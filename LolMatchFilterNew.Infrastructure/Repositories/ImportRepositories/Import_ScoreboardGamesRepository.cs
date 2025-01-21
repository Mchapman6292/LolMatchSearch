using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_ScoreboardGamesRepositories;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Infrastructure.DbContextService.LolMatchFilterDbContextFactory;
using System.Runtime.CompilerServices;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using Domain.DTOs.Western_MatchDTOs;
using Domain.Interfaces.ApplicationInterfaces.IDTOBuilders.IWesternMatchDTOFactories;


namespace Infrastructure.Repositories.ImportRepositories.Import_ScoreboardGamesRepositories
{
    public class Import_ScoreboardGamesRepository : GenericRepository<Import_ScoreboardGamesEntity>, IImport_ScoreboardGamesRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;
        private readonly IWesternMatchDTOFactory _westernMatchDTOFactory;

        public Import_ScoreboardGamesRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger, IWesternMatchDTOFactory westernMatchDtoFactory)
            : base(dbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
            _westernMatchDTOFactory = westernMatchDtoFactory;
        }

        public async Task<int> BulkAddScoreboardGames(IEnumerable<Import_ScoreboardGamesEntity> matchDetails)
        {
            int totalCount = matchDetails.Count();
            _appLogger.Info($"Starting bulk add of {totalCount} Leaguepedia match details.");
            LogTrackedScoreboardGames();

            try
            {
                int processedCount = 0;

                foreach (var matchDetail in matchDetails)
                {
                    if (matchDetail.DateTime_utc != null && matchDetail.DateTime_utc.Value.Kind != DateTimeKind.Utc)
                    {
                        matchDetail.DateTime_utc = DateTime.SpecifyKind(matchDetail.DateTime_utc.Value, DateTimeKind.Utc);
                    }
                    _matchFilterDbContext.Import_ScoreboardGames.Add(matchDetail);
                    processedCount++;

                    // Used to log progress at regular intervals(every 20% or 500 items)
                    if (processedCount % Math.Max(totalCount / 5, 500) == 0)
                    {
                        _appLogger.Info($"Processed {processedCount} of {totalCount} entities.");
                        LogTrackedScoreboardGames();
                    }
                }

                _appLogger.Info($"Saving changes for {processedCount} entities...");
                int addedCount = await _matchFilterDbContext.SaveChangesAsync();
                _appLogger.Info($"Successfully added {addedCount} new matches out of {totalCount} processed.");
                LogTrackedScoreboardGames();

                return addedCount;
            }
            catch (DbUpdateException ex)
            {
                _appLogger.Error($"Failed to bulk add matches. Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _appLogger.Error($"Inner exception: {ex.InnerException.Message}");
                }
                LogTrackedScoreboardGames();
                throw;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Unexpected error during bulk add: {ex.Message}");
                LogTrackedScoreboardGames();
                throw;
            }
        }


        public void LogTrackedScoreboardGames()
        {
            var trackedEntities = _matchFilterDbContext.ChangeTracker.Entries<Import_ScoreboardGamesEntity>()
                .Select(e => new
                {
                    Key = e.Property(p => p.GameId).CurrentValue,
                    e.State
                }).ToList();

            _appLogger.Info($"Number of tracked Import_ScoreboardGamesEntity: {trackedEntities.Count}");
        }




        public async Task<List<WesternMatchDTO>> GetEuNaMatchesAsync()
        {
            try
            {
                var query = from sg in _matchFilterDbContext.Import_ScoreboardGames
                            join team1Data in (
                                from tm in _matchFilterDbContext.Import_Teamname
                                join t in _matchFilterDbContext.Import_TeamsTable
                                on tm.Longname equals t.Name
                                select new
                                {
                                    TeamNameId = tm.TeamnameId,
                                    Longname = tm.Longname,
                                    Medium = tm.Medium,
                                    Short = tm.Short,
                                    Region = t.Region,
                                    Inputs = tm.Inputs
                                }
                            ) on sg.Team1 equals team1Data.Longname
                            join team2Data in (
                                from tm in _matchFilterDbContext.Import_Teamname
                                join t in _matchFilterDbContext.Import_TeamsTable
                                on tm.Longname equals t.Name
                                select new
                                {
                                    TeamNameId = tm.TeamnameId,
                                    Longname = tm.Longname,
                                    Medium = tm.Medium,
                                    Short = tm.Short,
                                    Region = t.Region,
                                    Inputs = tm.Inputs
                                }
                            ) on sg.Team2 equals team2Data.Longname
                            where team1Data.Region == "Americas" ||
                                  team1Data.Region == "EMEA" ||
                                  team1Data.Region == "North America" ||
                                  team2Data.Region == "Americas" ||
                                  team2Data.Region == "EMEA" ||
                                  team2Data.Region == "North America"
                            orderby sg.DateTime_utc descending
                            select _westernMatchDTOFactory.CreateWesternMatchDTO(
                                sg.GameId,
                                sg.MatchId,
                                sg.DateTime_utc,
                                sg.Tournament,
                                sg.Team1,
                                team1Data.TeamNameId,
                                sg.Team1Players,
                                sg.Team1Picks,
                                sg.Team2,
                                team2Data.TeamNameId,
                                sg.Team2Players,
                                sg.Team2Picks,
                                sg.WinTeam,
                                sg.LossTeam,
                                team1Data.Region,
                                team1Data.Longname,
                                team1Data.Medium,
                                team1Data.Short,
                                team1Data.Inputs,
                                team2Data.Region,
                                team2Data.Longname,
                                team2Data.Medium,
                                team2Data.Short,
                                team2Data.Inputs);

                return await query.Distinct().ToListAsync();
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error retrieving EU/NA matches: {ex.Message}", ex);
                throw;
            }
        }
    }
}
