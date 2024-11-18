using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameToHistoryMappers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamHistoryLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using LolMatchFilterNew.Domain.Entities.TeamRenamesEntities;




namespace LolMatchFilterNew.Application.TeamHistoryService.TeamHistoryLogics
{
    public class TeamHistoryLogic : ITeamHistoryLogic
    {
        private readonly IAppLogger _appLogger;
        private readonly ITeamRenameRepository _teamRenameRepository;
        private readonly IGenericRepository<TeamNameHistoryEntity> _teamHistoryEntity;

        public TeamHistoryLogic(IAppLogger appLogger, ITeamRenameRepository teamRenameRepository, IGenericRepository<TeamNameHistoryEntity> teamHistoryEntity)
        {
            _appLogger = appLogger;
            _teamRenameRepository = teamRenameRepository;
            _teamHistoryEntity = teamHistoryEntity;
        }

        public  List<TeamNameHistoryEntity> GetAllCurrentTeamsWithHistory()
        {
            throw new NotImplementedException();
        }

        // This is causing Infinite loop when there are no previous teamNames
        public List<string> FindPreviousTeamNames(string currentName, IEnumerable<TeamRenameEntity> allRenames, Dictionary<string, List<string>> resultsWithMorethanOneOriginalName)
        {
            var historyList = new List<string>();
            string nameToSearch = currentName;

            {
                var foundOriginalName = allRenames
                    .Where(rename => rename.NewName.Equals(nameToSearch, StringComparison.OrdinalIgnoreCase))
                    .Select(rename => rename.OriginalName)
                    .ToList();

                if (foundOriginalName.Any())
                {
                    historyList.Add(foundOriginalName.First());
                    nameToSearch = foundOriginalName.First();
                }
                else
                {
                    historyList.Add("N/A");
                }
            }
            return historyList;
        }



        public async Task<List<TeamNameHistoryEntity>> GetAllPreviousTeamNamesForCurrentTeamName(List<string> currentNames)
        {
            _appLogger.Info($"Starting {nameof(GetAllPreviousTeamNamesForCurrentTeamName)} with {currentNames.Count} team names");
            _appLogger.Info($"Input team names: {string.Join(", ", currentNames)}");

            IEnumerable<TeamRenameEntity> allRenames = await _teamRenameRepository.GetAllTeamRenameValuesAsync();
            _appLogger.Info($"Retrieved {allRenames.Count()} total rename records from database");

            var teamHistoryEntities = new List<TeamNameHistoryEntity>();
            var resultsWithMorethanOneOriginalName = new Dictionary<string, List<string>>();

            foreach (var currentName in currentNames)
            {
                _appLogger.Info($"Processing history for team: {currentName}");

                var historyList = FindPreviousTeamNames(currentName, allRenames, resultsWithMorethanOneOriginalName);

                _appLogger.Info(historyList.Any()
                    ? $"Found {historyList.Count} previous names for {currentName}: {string.Join(", ", historyList)}"
                    : $"No previous names found for {currentName}");

                var teamEntity = new TeamNameHistoryEntity
                {
                    CurrentTeamName = currentName,
                    NameHistory = historyList.Any() ? string.Join(", ", historyList) : string.Empty,
                };
                teamHistoryEntities.Add(teamEntity);

                _appLogger.Info($"Created history entity for {currentName} with history: {teamEntity.NameHistory}");
            }

            LogMultipleMatches(resultsWithMorethanOneOriginalName);

            foreach (var teamEntity in teamHistoryEntities)
            {
                _appLogger.Info($"Final entity - CurrentTeamName: {teamEntity.CurrentTeamName}, NameHistory: {teamEntity.NameHistory}");
            }

            _appLogger.Info($"Completed {nameof(GetAllPreviousTeamNamesForCurrentTeamName)} - Processed {teamHistoryEntities.Count} teams");
            return teamHistoryEntities;
        }




        public void LogMultipleMatches(Dictionary<string, List<string>> resultsWithMorethanOneOriginalName)
        {
            if (resultsWithMorethanOneOriginalName.Count > 0)
            {
                foreach (var teamEntry in resultsWithMorethanOneOriginalName)
                {
                    _appLogger.Error(
                        $"Error: Multiple names found when searching team history. " +
                        $"Team Name: {teamEntry.Key}. " +
                        $"Multiple names found: {string.Join(", ", teamEntry.Value)}");
                }
                _appLogger.Error(
                    $"Total number of teams with multiple history entries found: {resultsWithMorethanOneOriginalName.Count}. " +
                    $"Teams affected: {string.Join(", ", resultsWithMorethanOneOriginalName.Keys)}");
            }
        }

     




    }
}
