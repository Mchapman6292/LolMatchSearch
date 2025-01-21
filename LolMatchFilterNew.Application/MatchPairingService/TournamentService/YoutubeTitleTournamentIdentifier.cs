using Domain.Interfaces.InfrastructureInterfaces.IImportRepositories.IImport_TournamentRepositories;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;


namespace Application.MatchPairingService.TournamentService.YoutubeTitleTournamentIdentifiers
{
    public class YoutubeTitleTournamentIdentifier
    {
        private readonly IAppLogger _appLogger;
        private readonly IObjectLogger _objectLogger;
        private readonly IImport_TournamentRepository _importTournamentRepository;
        private readonly IImport_YoutubeDataRepository _import_YoutubeDataRepository;



        public YoutubeTitleTournamentIdentifier(IAppLogger appLogger, IObjectLogger objectLogger, IImport_TournamentRepository importTournamentRepository, IImport_YoutubeDataRepository import_YoutubeDataRepository)
        {
            _appLogger = appLogger;
            _objectLogger = objectLogger;
            _importTournamentRepository = importTournamentRepository;
            _import_YoutubeDataRepository = import_YoutubeDataRepository;
        }


    }
}
