using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRedirectEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamsTableEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IAPIControllers;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_ScoreboardGamesRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameToHistoryMappers;
using Newtonsoft.Json.Linq;



namespace LolMatchFilterNew.Application.Controllers


{
    public class APIControllers : IAPIControllers
    {
        private readonly IAppLogger _appLogger;
        private readonly IApiHelper _apiHelper;

        private readonly ILeaguepediaQueryService _leaguepediaQueryService;
        private readonly ILeaguepediaDataFetcher _leaguepediaDataFetcher;
        private readonly ILeaguepediaApiMapper _leaguepediaApiMapper;
        private readonly IYoutubeDataFetcher _youtubeDataFetcher;
        private readonly ITeamRenameToHistoryMapper _teamRenameToHistoryMapper;


        private readonly IImport_ScoreboardGamesRepository _Import_ScoreboardGames;
        private readonly IImport_YoutubeDataRepository _Import_YoutubeData;
        private readonly IImport_TeamRenameRepository _importTeamRenameRepository;


        private readonly IGenericRepository<Import_ScoreboardGamesEntity> _generic_Import_ScoreboardGamesEntity;
        private readonly IGenericRepository<Import_TeamRenameEntity> _generic_Import_TeamRenameEntity;
        private readonly IGenericRepository<Import_TeamsTableEntity> _generic_Import_TeamsTableRepository;
        private readonly IGenericRepository<Import_YoutubeDataEntity> _generic_Import_YoutubeDataEntity;
        private readonly IGenericRepository<Import_TeamRedirectEntity> _generic_Import_TeamRedirectEntity;


        private readonly IGenericRepository<Processed_LeagueTeamEntity> _generic_Processed_LeagueTeam;
        private readonly IGenericRepository<Processed_TeamNameHistoryEntity> _generic_Processed_TeamNameHistoryEntity;








        public APIControllers(

        IAppLogger appLogger,
        IApiHelper apiHelper,

        ILeaguepediaQueryService leaguepediaQueryService,
        ILeaguepediaDataFetcher leaguepediaDataFetcher,
        ILeaguepediaApiMapper leaguepediaApiMapper,
        IYoutubeDataFetcher youtubeDataFetcher,
        ITeamRenameToHistoryMapper teamRenameToHistoryMapper,

        IImport_TeamRenameRepository importTeamRenameRepository,
        IImport_ScoreboardGamesRepository importScoreboardGamesRepository,
        IImport_YoutubeDataRepository importYoutubeDataRepository,


        IGenericRepository<Import_TeamRenameEntity> genericImport_TeamRenameRepository,
        IGenericRepository<Import_TeamsTableEntity> genericImport_TeamsRepository,
        IGenericRepository<Import_YoutubeDataEntity> genericImport_YoutubeData,
        IGenericRepository<Import_TeamRedirectEntity> genericImport_TeamRedirectEntity,
        IGenericRepository<Import_ScoreboardGamesEntity> genericImport_ScoreboardGamesEntity,

        IGenericRepository<Processed_LeagueTeamEntity> genericProcessed_leagueTeamRepository,
        IGenericRepository<Processed_TeamNameHistoryEntity> genericProcessed_TeamHistoryRepository
        )


        {
            _appLogger = appLogger ?? throw new ArgumentNullException(nameof(appLogger));
            _apiHelper = apiHelper;

            _leaguepediaQueryService = leaguepediaQueryService;
            _leaguepediaDataFetcher = leaguepediaDataFetcher;
            _leaguepediaApiMapper = leaguepediaApiMapper;
            _youtubeDataFetcher = youtubeDataFetcher;
            _teamRenameToHistoryMapper = teamRenameToHistoryMapper;

            _Import_ScoreboardGames = importScoreboardGamesRepository;
            _Import_YoutubeData = importYoutubeDataRepository;
            _importTeamRenameRepository = importTeamRenameRepository;

            _generic_Import_TeamRenameEntity = genericImport_TeamRenameRepository;
            _generic_Import_TeamsTableRepository = genericImport_TeamsRepository;
            _generic_Import_YoutubeDataEntity = genericImport_YoutubeData;
            _generic_Import_TeamRedirectEntity = genericImport_TeamRedirectEntity;
            _generic_Import_ScoreboardGamesEntity = genericImport_ScoreboardGamesEntity;

            _generic_Processed_TeamNameHistoryEntity = genericProcessed_TeamHistoryRepository;
            _generic_Processed_LeagueTeam = genericProcessed_leagueTeamRepository;

        }



  

        public async Task FetchAndAddLeaguepediaDataForLeagueName(string leagueName)
        {
            int limit = 5;

            IEnumerable<JObject> apiData = await _leaguepediaDataFetcher.FetchAndExtractMatches(limit);

            IEnumerable<Import_ScoreboardGamesEntity> leagueEntities = await _leaguepediaApiMapper.MapToImport_ScoreboardGames(apiData);

            int addedEntries = await _Import_ScoreboardGames.BulkAddScoreboardGames(leagueEntities);

        }

        // Used BuildQueryStringScoreBoardGames
        public async Task ControllerAddScoreboardGames()
        {

            IEnumerable<JObject> apiData = await _leaguepediaDataFetcher.FetchAndExtractMatches();

            IEnumerable<Import_ScoreboardGamesEntity> scoreBoardGames = await _leaguepediaApiMapper.MapToImport_ScoreboardGames(apiData);

            await _generic_Import_ScoreboardGamesEntity.AddRangeWithTransactionAsync(scoreBoardGames);
        }


        public async Task ControllerAddTeamRedirects()
        {
            IEnumerable<JObject> apiData = await _leaguepediaDataFetcher.FetchAndExtractMatches();

            IEnumerable<Import_TeamRedirectEntity> redirectEntity = await _leaguepediaApiMapper.MapToImport_TeamRedirects(apiData);

            await _generic_Import_TeamRedirectEntity.AddRangeWithTransactionAsync(redirectEntity);
        }


        public async Task FetchAndAddTeamNamesForLeague(string leagueName)
        {
            int limit = 5;

            IEnumerable<JObject> apiData = await _leaguepediaDataFetcher.FetchAndExtractMatches( limit);

            var counts = _apiHelper.CountObjectsAndNullProperties(apiData);

            _appLogger.Info($"Object Analysis - Total Objects: {counts.TotalObjects}, " +
                   $"Null Objects: {counts.NullObjects}, " +
                   $"Null Properties: {counts.NullProperties}");


            IEnumerable<Processed_LeagueTeamEntity> leagueEntities = await _leaguepediaApiMapper.MapApiDataToLeagueTeamEntityForTeamShort(apiData);

            await _generic_Processed_LeagueTeam.AddRangeWithTransactionAsync(leagueEntities);
        }

        public async Task ControllerAddTeamRenames()
        {
            try
            {
                _appLogger.Info("Starting ControllerAddTeamRenames");

                if (_leaguepediaDataFetcher == null) throw new InvalidOperationException("_leaguepediaDataFetcher is null");
                if (_apiHelper == null) throw new InvalidOperationException("_apiHelper is null");
                if (_leaguepediaApiMapper == null) throw new InvalidOperationException("_leaguepediaApiMapper is null");
                if (_generic_Import_TeamRenameEntity == null) throw new InvalidOperationException("_generic_Import_TeamRenameEntity is null");

                var apiData = await _leaguepediaDataFetcher.FetchAndExtractMatches();
                _appLogger.Info($"API Data fetched: {apiData != null}");

                if (apiData == null)
                {
                    _appLogger.Error("FetchAndExtractMatches returned null");
                    throw new InvalidOperationException("API data fetch returned null");
                }

                var counts = _apiHelper.CountObjectsAndNullProperties(apiData);
                _appLogger.Info($"Object Analysis - Total Objects: {counts.TotalObjects}, " +
                               $"Null Objects: {counts.NullObjects}, " +
                               $"Null Properties: {counts.NullProperties}");

                var teamRenameEntities = await _leaguepediaApiMapper.MapToImport_TeamRename(apiData);
                _appLogger.Info($"Team Rename Entities mapped: {teamRenameEntities != null}");

                if (teamRenameEntities == null)
                {
                    _appLogger.Error("MapToImport_TeamRename returned null");
                    throw new InvalidOperationException("Entity mapping returned null");
                }

                var entityList = teamRenameEntities.ToList();
                if (!entityList.Any())
                {
                    _appLogger.Warning("No entities to save to database");
                    return;
                }

                foreach (var entity in entityList)
                {
                    if (entity == null)
                    {
                        _appLogger.Warning("Null entity found in teamRenameEntities");
                        continue;
                    }
                }

                var results = await _generic_Import_TeamRenameEntity.AddRangeWithTransactionAsync(entityList);
                _appLogger.Info($"Database operation results - Saved: {results.savedCount}, Failed: {results.failedCount}");
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error in ControllerAddTeamRenames: {ex.Message}");
                _appLogger.Error($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        
        // Used to add all records from ScoreboardGames for initial selection of columns, Has joined scoreboardGames to scoreboardPlayers
        public async Task ControllerAddScoreBoardGamesForAllGames()
        {
            IEnumerable<JObject> ExtractedSGames = await _leaguepediaDataFetcher.FetchAndExtractMatches();

            IEnumerable<Import_ScoreboardGamesEntity> MappedSGEntities = await _leaguepediaApiMapper.MapToImport_ScoreboardGames(ExtractedSGames);


            await _Import_ScoreboardGames.BulkAddScoreboardGames(MappedSGEntities);



        }


        public async Task ControllerAddTeamNameHistoryToDatabase()
        {
            List<Processed_TeamNameHistoryEntity> teamRenameEntities = await _teamRenameToHistoryMapper.MapTeamRenameToHistoryAsync();

           await _generic_Processed_TeamNameHistoryEntity.AddRangeWithTransactionAsync(teamRenameEntities);
        }

        public async Task ControllerAddTeamsTableToDatabase()
        {
            IEnumerable<JObject> teamEntities = await _leaguepediaDataFetcher.FetchAndExtractMatches();

            IEnumerable<Import_TeamsTableEntity> mappedEntites = await _leaguepediaApiMapper.MapToImport_Teams(teamEntities);

            await _generic_Import_TeamsTableRepository.AddRangeWithTransactionAsync(mappedEntites);
        }




        // Used to populate Processed_TeamNameHistory 15/12/2024
        public async Task ControllerAddProcessed_TeamNameHistory()
        {
            List<Processed_TeamNameHistoryEntity> teamHistories = await _teamRenameToHistoryMapper.MapTeamRenameToHistoryAsync();

            await _generic_Processed_TeamNameHistoryEntity.AddRangeWithTransactionAsync(teamHistories);
        }


        public async Task ControllerDeleteProcessed_TeamNameHistory()
        {
            await _generic_Processed_TeamNameHistoryEntity.RemoveAllEntitiesAsync();
        }

        public async Task DeleteAllTeamRedirects()
        {
            await _generic_Import_TeamRedirectEntity.RemoveAllEntitiesAsync();
        }


    }
}





