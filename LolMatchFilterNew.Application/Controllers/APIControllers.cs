using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IAPIControllers;
using Newtonsoft.Json.Linq;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_ScoreboardGamesRepositories;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameToHistoryMappers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamHistoryLogic;


using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRedirectEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamsTableEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_ProPlayerEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_YoutubeDataEntities;
using LolMatchFilterNew.Infrastructure.Repositories.TeamRenameRepositories;



namespace LolMatchFilterNew.Application.Controllers


{
    public class APIControllers : IAPIControllers
    {
        private readonly IAppLogger _appLogger;
        private readonly ILeaguepediaQueryService _leaguepediaQueryService;
        private readonly ILeaguepediaDataFetcher _leaguepediaDataFetcher;
        private readonly ILeaguepediaApiMapper _leaguepediaApiMapper;
        private readonly IImport_ScoreboardGamesRepository _Import_ScoreboardGamesRepository;
        private readonly IYoutubeDataFetcher _youtubeDataFetcher;
        private readonly IImport_YoutubeDataRepository _youtubeVideoRepository;


        private readonly IGenericRepository<Import_ScoreboardGamesEntity> _generic_Import_ScoreboardGamesEntity;
        private readonly IGenericRepository<Processed_LeagueTeamEntity> _generic_Processed_LeagueTeamRepository;
        private readonly IGenericRepository<Import_TeamRenameEntity> _generic_Import_TeamRenameEntity;
        private readonly IGenericRepository<Processed_TeamNameHistoryEntity> _generic_Processed_TeamNameHistoryEntity;
        private readonly IGenericRepository<Import_TeamsTableEntity> _generic_Import_TeamsTableEntity;
        private readonly IGenericRepository<Import_YoutubeDataEntity> _generic_Import_YoutubeDataEntity;
        private readonly IGenericRepository<Import_TeamRedirectEntity> _generic_Import_TeamRedirectEntity;



        private readonly IImport_TeamRenameRepository _importTeamRenameRepository;


        private readonly IApiHelper _apiHelper;
        private readonly ITeamRenameToHistoryMapper _teamRenameToHistoryMapper;
        private readonly ITeamHistoryLogic _teamHistoryLogic;


        public APIControllers(IImport_TeamRenameRepository importTeamRenameRepository, IAppLogger appLogger, ILeaguepediaQueryService leaguepediaQueryService, ILeaguepediaDataFetcher leaguepediaDataFetcher, ILeaguepediaApiMapper leaguepediaApiMapper, IImport_ScoreboardGamesRepository leaguepediaMatchDetailRepository, IYoutubeDataFetcher youtubeDataFetcher, IImport_YoutubeDataRepository youtubeVideoRepository, IGenericRepository<Processed_LeagueTeamEntity> leagueTeamRepository,IGenericRepository<Import_TeamRenameEntity> genericTeamRenameRepository, IApiHelper apiHelper, ITeamRenameToHistoryMapper teamRenameToHistoryMapper, IGenericRepository<Processed_TeamNameHistoryEntity> genericTeamHistoryRepository, IGenericRepository<Import_TeamsTableEntity> genericLpediaTeamRepository, ITeamHistoryLogic teamHistoryLogic, IGenericRepository<Import_YoutubeDataEntity> genericYoutubeVideoResultsRepository, IGenericRepository<Import_TeamRedirectEntity> genericImport_TeamRedirectEntity, IGenericRepository<Import_ScoreboardGamesEntity> genericImport_ScoreboardGamesEntity)
        {
            _appLogger = appLogger;
            _leaguepediaQueryService = leaguepediaQueryService;
            _leaguepediaDataFetcher = leaguepediaDataFetcher;
            _leaguepediaApiMapper = leaguepediaApiMapper;
            _Import_ScoreboardGamesRepository = leaguepediaMatchDetailRepository;
            _youtubeDataFetcher = youtubeDataFetcher;
            _youtubeVideoRepository = youtubeVideoRepository;
            _generic_Processed_LeagueTeamRepository = leagueTeamRepository;
            _generic_Import_TeamRenameEntity = genericTeamRenameRepository;
            _apiHelper = apiHelper;
            _teamRenameToHistoryMapper = teamRenameToHistoryMapper;
            _generic_Processed_TeamNameHistoryEntity = genericTeamHistoryRepository;
            _generic_Import_TeamsTableEntity = genericLpediaTeamRepository;
            _generic_Import_YoutubeDataEntity = genericYoutubeVideoResultsRepository;
            _teamHistoryLogic = teamHistoryLogic;
            _generic_Import_TeamRedirectEntity = genericImport_TeamRedirectEntity;
            _generic_Import_ScoreboardGamesEntity = genericImport_ScoreboardGamesEntity;

        }



        public async Task DeleteAllTeamRedirects()
        {
            await _generic_Import_TeamRedirectEntity.RemoveAllEntitiesAsync();
        }

        public async Task FetchAndAddLeaguepediaDataForLeagueName(string leagueName)
        {
            int limit = 5;

            IEnumerable<JObject> apiData = await _leaguepediaDataFetcher.FetchAndExtractMatches(limit);

            IEnumerable<Import_ScoreboardGamesEntity> leagueEntities = await _leaguepediaApiMapper.MapToImport_ScoreboardGames(apiData);

            int addedEntries = await _Import_ScoreboardGamesRepository.BulkAddScoreboardGames(leagueEntities);

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

            await _generic_Processed_LeagueTeamRepository.AddRangeWithTransactionAsync(leagueEntities);
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


            await _Import_ScoreboardGamesRepository.BulkAddScoreboardGames(MappedSGEntities);



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

            await _generic_Import_TeamsTableEntity.AddRangeWithTransactionAsync(mappedEntites);
        }

        public async Task ControllerMapAllCurrentTeamNamesToPreviousTeamNamesAsync()
        {

            List<string> teamNames = await _importTeamRenameRepository.GetCurrentTeamNamesAsync();

            List<Processed_TeamNameHistoryEntity> teamHistories = await _teamHistoryLogic.GetAllPreviousTeamNamesForCurrentTeamName(teamNames);

            await _generic_Processed_TeamNameHistoryEntity.AddRangeWithTransactionAsync(teamHistories);
        }

        public async Task ControllerDeleteAllYoutubeVideoResultsEntries()
        {
            await _generic_Import_YoutubeDataEntity.RemoveAllEntitiesAsync();
        }


    }
}





