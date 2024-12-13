using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ITeamNameHistoryFormatters;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;




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
                        var history = team.NameHistory ?? new List<string>();

                        return history;
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