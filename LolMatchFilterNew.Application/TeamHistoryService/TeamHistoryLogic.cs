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

        public List<string> FindPreviousTeamNames(string currentName, IEnumerable<TeamRenameEntity> allRenames, Dictionary<string, List<string>> resultsWithMorethanOneOriginalName)
        {
            var historyList = new List<string>();
            string nameToSearch = currentName;

            while (true)
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
                    break;
                }
            }
            return historyList;
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

        public async Task<List<TeamNameHistoryEntity>> GetAllPreviousTeamNamesForCurrentTeamName(List<string> currentNames)
        {
            IEnumerable<TeamRenameEntity> allRenames = await _teamRenameRepository.GetAllTeamRenameValuesAsync();
            var teamHistoryEntities = new List<TeamNameHistoryEntity>();
            var resultsWithMorethanOneOriginalName = new Dictionary<string, List<string>>();

            foreach (var currentName in currentNames)
            {
                var historyList = FindPreviousTeamNames(currentName, allRenames, resultsWithMorethanOneOriginalName);

                var teamEntity = new TeamNameHistoryEntity
                {
                    CurrentTeamName = currentName,
                    NameHistory = historyList.Any() ? string.Join(", ", historyList) : string.Empty,
                };

                teamHistoryEntities.Add(teamEntity);
            }
            LogMultipleMatches(resultsWithMorethanOneOriginalName);

            return teamHistoryEntities;
        }





    }
}
