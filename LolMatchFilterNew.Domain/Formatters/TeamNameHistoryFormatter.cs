
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


    }
}