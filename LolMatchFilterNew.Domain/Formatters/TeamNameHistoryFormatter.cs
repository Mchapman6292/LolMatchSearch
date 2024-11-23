using LolMatchFilterNew.Domain.Entities.Processed_TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ITeamNameHistoryFormatters;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Formatters.TeamNameHistoryFormatters
{
    public  class TeamNameHistoryFormatter : ITeamNameHistoryFormatter
    {
        private readonly IAppLogger _appLogger;

        public TeamNameHistoryFormatter(IAppLogger appLogger)
        {
            _appLogger = appLogger;
        }

        // Used mainly for Testing TeamHistory. 

        public  Dictionary<string, List<string>> FormatTeamHistoryToDict(List<Processed_TeamNameHistoryEntity> teamHistories)
        {
            return teamHistories
                .Where(team => team != null && !string.IsNullOrEmpty(team.CurrentTeamName))
                .ToDictionary(
                    team => team.CurrentTeamName,
                    team =>
                    {
                        if (string.IsNullOrEmpty(team.NameHistory))
                            return new List<string>();

                        var result = new List<string>();
                        var parts = team.NameHistory.Split(new[] { ',', ';' });

                        for (int i = 0; i < parts.Length; i++)
                        {
                            var name = parts[i].Trim();
                            if (name == "Also Known As")
                                continue;

                            if (!string.IsNullOrWhiteSpace(name))
                                result.Add(name);
                        }

                        _appLogger.Info($"Team: {team.CurrentTeamName}");
                        _appLogger.Info($"Original History: {team.NameHistory}");
                        _appLogger.Info($"Processed History: {string.Join(", ", result)}");

                        return result;
                    },
                    StringComparer.OrdinalIgnoreCase
                );
        }

        public  Dictionary<string, string> StandardizeDelimiters(List<Processed_TeamNameHistoryEntity> teamHistories)
        {
            var formattedDict = FormatTeamHistoryToDict(teamHistories);

            return formattedDict.ToDictionary(
                kvp => kvp.Key,
                kvp => string.Join(", ", kvp.Value),
                StringComparer.OrdinalIgnoreCase
            );
        }
    }
}