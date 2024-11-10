using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaDataFetcher;
using Microsoft.Extensions.DependencyInjection;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IAPIControllers;
using LolMatchFilterNew.Application.QueryBuilders.LeaguepediaQueryService;
using LolMatchFilterNew.Domain.Apis.LeaguepediaDataFetcher;
using LolMatchFilterNew.Infrastructure.DataConversion.LeaguepediaApiMappers;
using Newtonsoft.Json.Linq;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeVideoRepository;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaMatchDetailRepository;
using LolMatchFilterNew.Domain.Entities.LeagueTeamEntities;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Entities.TeamRenamesEntities;
using System.Drawing.Printing;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameRepositories;
using LolMatchFilterNew.Infrastructure.DataConversion.TeamRenameToHistoryMappers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameToHistoryMappers;
using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using System.Runtime.CompilerServices;
using LolMatchFilterNew.Domain.Entities.LpediaTeamEntities;
using System.Runtime.Serialization;

namespace LolMatchFilterNew.Application.Controllers
{
    public class APIControllers : IAPIControllers
    {
        private readonly IAppLogger _appLogger;
        private readonly ILeaguepediaQueryService _leaguepediaQueryService;
        private readonly ILeaguepediaDataFetcher _leaguepediaDataFetcher;
        private readonly ILeaguepediaApiMapper _leaguepediaApiMapper;
        private readonly ILeaguepediaMatchDetailRepository _leaguepediaMatchDetailRepository;
        private readonly IYoutubeDataFetcher _youtubeDataFetcher;
        private readonly IYoutubeVideoRepository _youtubeVideoRepository;
        private readonly IGenericRepository<LeagueTeamEntity> _leagueTeamRepository;
        private readonly IGenericRepository<TeamRenameEntity> _genericTeamRenameRepository;
        private readonly IGenericRepository<TeamNameHistoryEntity> _genericTeamHistoryRepository;
        private readonly IGenericRepository<LpediaTeamEntity> _genericLpediaTeamRepository;
        private readonly ITeamRenameRepository _teamRenameRepository;
        private readonly IApiHelper _apiHelper;
        private readonly ITeamRenameToHistoryMapper _teamRenameToHistoryMapper;


        public APIControllers(IAppLogger appLogger, ILeaguepediaQueryService leaguepediaQueryService, ILeaguepediaDataFetcher leaguepediaDataFetcher, ILeaguepediaApiMapper leaguepediaApiMapper, ILeaguepediaMatchDetailRepository leaguepediaMatchDetailRepository, IYoutubeDataFetcher youtubeDataFetcher, IYoutubeVideoRepository youtubeVideoRepository, IGenericRepository<LeagueTeamEntity> leagueTeamRepository,IGenericRepository<TeamRenameEntity> genericTeamRenameRepository, IApiHelper apiHelper, ITeamRenameRepository teamRenameRepsitory, ITeamRenameToHistoryMapper teamRenameToHistoryMapper, IGenericRepository<TeamNameHistoryEntity> genericTeamHistoryRepository, IGenericRepository<LpediaTeamEntity> genericLpediaTeamRepository)
        {
            _appLogger = appLogger;
            _leaguepediaQueryService = leaguepediaQueryService;
            _leaguepediaDataFetcher = leaguepediaDataFetcher;
            _leaguepediaApiMapper = leaguepediaApiMapper;
            _leaguepediaMatchDetailRepository = leaguepediaMatchDetailRepository;
            _youtubeDataFetcher = youtubeDataFetcher;
            _youtubeVideoRepository = youtubeVideoRepository;
            _leagueTeamRepository = leagueTeamRepository;
            _genericTeamRenameRepository = genericTeamRenameRepository;
            _apiHelper = apiHelper;
            _teamRenameRepository = teamRenameRepsitory;
            _teamRenameToHistoryMapper = teamRenameToHistoryMapper;
            _genericTeamHistoryRepository = genericTeamHistoryRepository;
            _genericLpediaTeamRepository = genericLpediaTeamRepository;
        }


        public async Task FetchAndAddLeaguepediaDataForLeagueName(string leagueName)
        {
            int limit = 5;

            IEnumerable<JObject> apiData = await _leaguepediaDataFetcher.FetchAndExtractMatches(leagueName, limit);

            IEnumerable<LeaguepediaMatchDetailEntity> leagueEntities = await _leaguepediaApiMapper.MapLeaguepediaDataToEntity(apiData);

            int addedEntries = await _leaguepediaMatchDetailRepository.BulkAddLeaguepediaMatchDetails(leagueEntities);

        }

        public async Task FetchAndAddYoutubeVideos(List<string> playlistIds)
        {
            IList<YoutubeVideoEntity> retrievedEntities = await _youtubeDataFetcher.RetrieveAndMapAllPlaylistVideosToEntities(playlistIds);

            await _youtubeVideoRepository.BulkaddYoutubeDetails(retrievedEntities);
        }

        public async Task FetchAndAddTeamNamesForLeague(string leagueName)
        {
            int limit = 5;

            IEnumerable<JObject> apiData = await _leaguepediaDataFetcher.FetchAndExtractMatches(leagueName, limit);

            var counts = _apiHelper.CountObjectsAndNullProperties(apiData);

            _appLogger.Info($"Object Analysis - Total Objects: {counts.TotalObjects}, " +
                   $"Null Objects: {counts.NullObjects}, " +
                   $"Null Properties: {counts.NullProperties}");


            IEnumerable<LeagueTeamEntity> leagueEntities = await _leaguepediaApiMapper.MapApiDataToLeagueTeamEntityForTeamShort(apiData);

            await _leagueTeamRepository.AddRangeWithTransactionAsync(leagueEntities);
        }

        public async Task FetchAllDataForTeamRenames()
        {
            try
            {
                _appLogger.Info("Starting FetchAllDataForTeamRenames");

                if (_leaguepediaDataFetcher == null) throw new InvalidOperationException("_leaguepediaDataFetcher is null");
                if (_apiHelper == null) throw new InvalidOperationException("_apiHelper is null");
                if (_leaguepediaApiMapper == null) throw new InvalidOperationException("_leaguepediaApiMapper is null");
                if (_genericTeamRenameRepository == null) throw new InvalidOperationException("_genericTeamRenameRepository is null");

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

                var teamRenameEntities = await _leaguepediaApiMapper.MapJTokenToTeamRenameEntity(apiData);
                _appLogger.Info($"Team Rename Entities mapped: {teamRenameEntities != null}");

                if (teamRenameEntities == null)
                {
                    _appLogger.Error("MapJTokenToTeamRenameEntity returned null");
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

                var results = await _genericTeamRenameRepository.AddRangeWithTransactionAsync(entityList);
                _appLogger.Info($"Database operation results - Saved: {results.savedCount}, Failed: {results.failedCount}");
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error in FetchAllDataForTeamRenames: {ex.Message}");
                _appLogger.Error($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }


        public async Task ControllerGetAllCurrentTeamNames()
        {
            List<string> renames = await _teamRenameRepository.GetCurrentTeamNamesAsync();

            int total = renames.Count;

            _appLogger.Info($"Number of current team names: {total}");

            await _apiHelper.WriteListDictToWordDocAsync( renames );
        }

        public async Task ControllerAddTeamNameHistoryToDatabase()
        {
            List<TeamNameHistoryEntity> teamRenameEntities = await _teamRenameToHistoryMapper.MapTeamRenameToHistoryAsync();

           await _genericTeamHistoryRepository.AddRangeWithTransactionAsync(teamRenameEntities);
        }

        public async Task ControllerAddLpediaTeamsToDatabase()
        {
            IEnumerable<JObject> teamEntities = await _leaguepediaDataFetcher.FetchAndExtractMatches();

            IEnumerable<LpediaTeamEntity> mappedEntites = await _leaguepediaApiMapper.MapJTokenToLpediaTeamEntity(teamEntities);

            await _genericLpediaTeamRepository.AddRangeWithTransactionAsync(mappedEntites);
        }
    }
}





