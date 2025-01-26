using LolMatchFilterNew.Domain.DTOs.YoutubeTitleTeamOccurrenceDTOs; 
using Domain.DTOs.PlayListDateRangeDTOs;
using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.ApplicationInterfaces.IDTOBuilders.PlayListDateRangeServices;
using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IImport_TeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameDTOBuilders;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeDataWithTeamsDTOBuilders;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamMatchCountFactories;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders;
using Domain.Interfaces.InfrastructureInterfaces.IImport_TeamnameRepositories;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using LolMatchFilterNew.Domain.DTOs.YoutubeTitleTeamOccurrenceDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchComparisonResultBuilders;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchServiceControllers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_ScoreboardGamesRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using Domain.Interfaces.ApplicationInterfaces.YoutubeDataService.TeamIdentifiers.IYoutubeTitleTeamOccurrenceServices;
using Domain.Enums.TeamNameTypes;


namespace Application.MatchPairingService.MatchComparisonResultService.MatchComparisonControllers;

public class MatchComparisonController : IMatchComparisonController
{
    private readonly IAppLogger _appLogger;
    private readonly IObjectLogger _objectLogger;
    private readonly IMatchComparisonResultBuilder _matchBuilder;
    private readonly ITeamNameDTOBuilder _teamNameDTOBuilder;
    private readonly IStoredSqlFunctionCaller _storedSqlFunctionCaller;
    private readonly IImport_YoutubeDataRepository _import_YoutubeDataRepository;
    private readonly IImport_TeamnameRepository _import_TeamNameRepository;
    private readonly IYoutubeDataWithTeamsDTOBuilder _processed_YoutubeDataDTOBuilder;
    private readonly IPlayListDateRangeService _playListDateRangeService;
    private readonly IImport_ScoreboardGamesRepository _import_ScoreboardGamesRepository;
    private readonly IYoutubeTitleTeamOccurenceService _youtubeTitleTeamOccurenceService;


    private readonly IYoutubeTeamNameService _youtubeTeamNameService;
    private readonly IImport_TeamNameService _importTeamNameService;


    private readonly IYoutubeTitleTeamNameFinder _youtubeTitleTeamNameFinder;
    private readonly IYoutubeTitleTeamMatchCountFactory _youtubeTitleTeamMatchCountFactory;




    public MatchComparisonController(

        IAppLogger appLogger,
        IObjectLogger objectLogger,
        IMatchComparisonResultBuilder matchComparisonResultBuilder,
        ITeamNameDTOBuilder teamnameDTOBuilder,
        IImport_YoutubeDataRepository import_YoutubeDataRepository,
        IStoredSqlFunctionCaller storedSqlFunctionCaller,
        IYoutubeDataWithTeamsDTOBuilder proccessed_YoutubeDataDTOBuilder,
        IPlayListDateRangeService playListDateRangeService,
        IImport_ScoreboardGamesRepository import_ScoreboardGamesRepository,

        IImport_TeamNameService import_TeamnNameService,
        IYoutubeTeamNameService youtubeTeamNameService,
        IImport_TeamnameRepository import_TeamnameRepository,

        IYoutubeTitleTeamNameFinder youtubeTitleTeamNameFinder,
        IYoutubeTitleTeamMatchCountFactory youtubeTitleTeamMatchCountFactory,
        IYoutubeTitleTeamOccurenceService youtubeTitleTeamOccurenceService
        

    )
    {
        _appLogger = appLogger;
        _objectLogger = objectLogger;
        _matchBuilder = matchComparisonResultBuilder;

        _teamNameDTOBuilder = teamnameDTOBuilder;
        _import_YoutubeDataRepository = import_YoutubeDataRepository;
        _import_TeamNameRepository = import_TeamnameRepository;
        _storedSqlFunctionCaller = storedSqlFunctionCaller;
        _processed_YoutubeDataDTOBuilder = proccessed_YoutubeDataDTOBuilder;
        _playListDateRangeService = playListDateRangeService;
        _import_ScoreboardGamesRepository = import_ScoreboardGamesRepository;

        _youtubeTeamNameService = youtubeTeamNameService;
        _youtubeTitleTeamNameFinder = youtubeTitleTeamNameFinder;
        _youtubeTitleTeamMatchCountFactory = youtubeTitleTeamMatchCountFactory;
        _importTeamNameService = import_TeamnNameService;
        _youtubeTitleTeamOccurenceService = youtubeTitleTeamOccurenceService;


    }


    // 


    public async Task TESTFindTeamNameMatchesInYoutubeTitleAsync()
    {
        List<Import_TeamnameEntity> teamnNameEntities = await _storedSqlFunctionCaller.GetAllWesternTeamsAsync();
        List<Import_YoutubeDataEntity> importYoutubeEntities = await _storedSqlFunctionCaller.GetYoutubeDataEntitiesForWesternTeamsAsync();

        // Earlier youtube video = 20/06/2014, - 2 week leeway.
        var startDate = new DateTime(2014, 6, 6);

        var filteredYoutubeEntities = importYoutubeEntities
            .Where(x => x.PublishedAt_utc > startDate)
            .ToList();

        List<TeamNameDTO> teamNameDtos = _importTeamNameService.BuildTeamNameDTOFromImport_TeamNameEntites(teamnNameEntities);

        _appLogger.Info($"TeamNameDTO list build with count: {teamNameDtos.Count}.");

        var youtubeEntitiesSample = filteredYoutubeEntities
            .OrderBy(x => Guid.NewGuid())
            .Take(100)
            .ToList();



        _importTeamNameService.PopulateImport_TeamNameAllNames(teamNameDtos);
        _youtubeTeamNameService.PopulateYoutubeTitleTeamMatchCountList(youtubeEntitiesSample);


        // This needs to update the list held in YoutubeTeamService instead of local variables.

        List<YoutubeTitleTeamOccurenceDTO> teamNameOccurences = _youtubeTeamNameService.ReturnYoutubeTitleTeamMatchCounts();

        List<YoutubeTitleTeamOccurenceDTO> updatedOccurences = new List<YoutubeTitleTeamOccurenceDTO>();



        foreach(var youtubeOccurenceDto in teamNameOccurences)
        {
            _youtubeTitleTeamOccurenceService.TallyTeamNameOccurrences(youtubeOccurenceDto);
            Dictionary<string, List<(TeamNameType, string)>> teamIdWithMostMatches = _youtubeTitleTeamOccurenceService.GetTeamIdsWithHighestOccurences(youtubeOccurenceDto);
            _youtubeTitleTeamOccurenceService.PopulateTeamIdsWithMostMatches(youtubeOccurenceDto, teamIdWithMostMatches);

            updatedOccurences.Add(youtubeOccurenceDto);

        }
       _objectLogger.LogTopMatchesInOccurrenceDTOs(updatedOccurences);



    }




    public async Task TESTGetPlaylistDateRanges()
    {
        List<Import_YoutubeDataEntity> youtubeEntities = await _import_YoutubeDataRepository.GetEuNaVideosByPlaylistAsync();

        List<Import_YoutubeDataEntity> tenFirst = youtubeEntities.OrderByDescending(e => e.PublishedAt_utc).Take(10).ToList();

        HashSet<string> playlistTitles = new HashSet<string>();   

        foreach(var entity  in tenFirst)
        {
            playlistTitles.Add(entity.PlaylistTitle);
        }

        foreach(var title in playlistTitles)
        {
            _appLogger.Info($"PlaylistTitle: {title}");
        }


        _playListDateRangeService.PopulateGamesWithinPlaylistDates(youtubeEntities);

        await _playListDateRangeService.UpdateGamesWithinYoutubePlaylistDatesWithMatchesAsync();

        List<PlayListDateRangeResult> playlistResults = _playListDateRangeService.ReturngamesWithinYoutubePlaylistDates();

        _objectLogger.LogGameIdsInUpdatedPlaylistDateRangeResult(playlistResults);
    }














}





































