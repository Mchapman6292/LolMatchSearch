using Domain.Interfaces.InfrastructureInterfaces.IImport_TeamnameRepositories;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;

namespace Application.MatchPairingService.ScoreboardGameService.LeaguepediaBuilders
{
    public class LeaguepediaBuilder
    {
        private readonly IAppLogger _appLogger;
        private readonly IImport_TeamnameRepository _teamnameRepository;


        public LeaguepediaBuilder(IAppLogger appLogger, IImport_TeamnameRepository teamnameRepository)
        {
            _appLogger = appLogger;
            _teamnameRepository = teamnameRepository;
        }


        public List<string> ParseTeamnameInputsColumn(string teamnameInputs)
        {
            string cleaned = teamnameInputs.Trim('{', '}', '"');

            List<string> allNames = cleaned.Split(';', StringSplitOptions.RemoveEmptyEntries)
                                        .Select(name => name.Trim())
                                        .ToList();

            return allNames;
        }
    }
}
