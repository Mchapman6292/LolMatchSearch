using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameRepositories;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamHistoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using Microsoft.EntityFrameworkCore;

namespace LolMatchFilterNew.Infrastructure.Repositories.TeamHistoryRepositories
{


    public class TeamHistoryRepository : GenericRepository<TeamNameHistoryEntity>, ITeamHistoryRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public TeamHistoryRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger)
            : base(dbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
        }

        public async Task<TeamNameHistoryEntity> GetByCurrentTeamNameAsync(string teamName)
        {
            try
            {
                return  await _matchFilterDbContext.TeamNameHistory
                    .FirstOrDefaultAsync(t => t.CurrentTeamName.ToLower() == teamName.ToLower());
   
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error retrieving team history for {teamName}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<TeamNameHistoryEntity>> GetAllTeamHistoriesAsync()
        {
            try
            {
                return await _matchFilterDbContext.TeamNameHistory
                    .OrderBy(t => t.CurrentTeamName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _appLogger.Error("Error retrieving all team histories", ex);
                throw;
            }
        }

        public async Task<bool> AddTeamHistoryAsync(TeamNameHistoryEntity teamHistory)
        {
            try
            {
                await _matchFilterDbContext.TeamNameHistory.AddAsync(teamHistory);
                var result = await _matchFilterDbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error adding team history for {teamHistory.CurrentTeamName}", ex);
                throw;
            }
        }

        public async Task<bool> UpdateTeamHistoryAsync(TeamNameHistoryEntity teamHistory)
        {
            try
            {
                var existing = await GetByCurrentTeamNameAsync(teamHistory.CurrentTeamName);
                if (existing == null) return false;

                _matchFilterDbContext.TeamNameHistory.Update(teamHistory);
                var result = await _matchFilterDbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error updating team history for {teamHistory.CurrentTeamName}", ex);
                throw;
            }
        }

        public async Task<bool> DeleteTeamHistoryAsync(string teamName)
        {
            try
            {
                var entity = await GetByCurrentTeamNameAsync(teamName);
                if (entity == null) return false;

                _matchFilterDbContext.TeamNameHistory.Remove(entity);
                var result = await _matchFilterDbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error deleting team history for {teamName}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<TeamNameHistoryEntity>> GetTeamsByRegionAsync(string region)
        {
            try
            {
                return await _matchFilterDbContext.TeamNameHistory
                    .Where(t => t.Region.ToLower() == region.ToLower())
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error retrieving teams for region {region}", ex);
                throw;
            }
        }

        public async Task<bool> TeamExistsAsync(string teamName)
        {
            try
            {
                return await _matchFilterDbContext.TeamNameHistory
                    .AnyAsync(t => t.CurrentTeamName.ToLower() == teamName.ToLower() ||
                                 t.NameHistory.ToLower().Contains(teamName.ToLower()));
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error checking existence of team {teamName}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetAllPreviousNamesAsync(string currentTeamName)
        {
            try
            {
                var team = await GetByCurrentTeamNameAsync(currentTeamName);
                if (team?.NameHistory == null) return new List<string>();

                return team.NameHistory.Split(", ").ToList();
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error retrieving previous names for {currentTeamName}", ex);
                throw;
            }
        }

        public async Task<TeamNameHistoryEntity> GetByPreviousNameAsync(string previousName)
        {
            try
            {
                return await _matchFilterDbContext.TeamNameHistory
                    .FirstOrDefaultAsync(t => t.NameHistory.ToLower().Contains(previousName.ToLower()));
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error retrieving team by previous name {previousName}", ex);
                throw;
            }
        }

        public async Task<bool> UpdateTeamNameHistoryAsync(string currentTeamName, string newHistoricalName)
        {
            try
            {
                var team = await GetByCurrentTeamNameAsync(currentTeamName);
                if (team == null) return false;

                var currentHistory = string.IsNullOrEmpty(team.NameHistory)
                    ? new List<string>()
                    : team.NameHistory.Split(", ").ToList();

                if (!currentHistory.Contains(newHistoricalName))
                {
                    currentHistory.Add(newHistoricalName);
                    team.NameHistory = string.Join(", ", currentHistory);
                    var result = await _matchFilterDbContext.SaveChangesAsync();
                    return result > 0;
                }

                return true;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error updating name history for {currentTeamName}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<TeamNameHistoryEntity>> GetTeamsWithNameChangeHistoryAsync()
        {
            try
            {
                return await _matchFilterDbContext.TeamNameHistory
                    .Where(t => !string.IsNullOrEmpty(t.NameHistory))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _appLogger.Error("Error retrieving teams with name change history", ex);
                throw;
            }
        }

        public async Task<Dictionary<string, List<string>>> GetAllTeamNameMappingsAsync()
        {
            try
            {
                var allTeams = await GetAllTeamHistoriesAsync();
                return allTeams.ToDictionary(
                    t => t.CurrentTeamName,
                    t => string.IsNullOrEmpty(t.NameHistory)
                        ? new List<string>()
                        : t.NameHistory.Split(", ").ToList()
                );
            }
            catch (Exception ex)
            {
                _appLogger.Error("Error creating team name mappings", ex);
                throw;
            }
        }
    }
}