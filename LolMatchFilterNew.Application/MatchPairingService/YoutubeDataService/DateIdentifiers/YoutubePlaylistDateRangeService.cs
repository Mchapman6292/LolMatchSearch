using Domain.DTOs.PlayListDateRangeDTOs;
using Domain.Interfaces.ApplicationInterfaces.IDTOBuilders.IPlayListDateRangeDTOFactories;
using Domain.Interfaces.InfrastructureInterfaces.IImportRepositories.IImport_TournamentRepositories;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using Domain.Interfaces.ApplicationInterfaces.YoutubeDataService.DateIdentifiers.IYoutubePlaylistDateRangeServices;

namespace Application.MatchPairingService.YoutubeDataService.DateIdentifiers.YoutubePlaylistDateRangeServices
{
    public class YoutubePlaylistDateRangeService : IYoutubePlaylistDateRangeService
    {
        private readonly IAppLogger _appLogger;
        private readonly IObjectLogger _objectLogger;
        private readonly IImport_TournamentRepository _importTournamentRepository;
        private readonly IImport_YoutubeDataRepository _import_YoutubeDataRepository;
        private readonly IPlayListDateRangeDTOFactory _playListDateRangeDTOFactory;


        public List<PlayListDateRangeResult> TournamentDateRanges = new List<PlayListDateRangeResult>(); 


        public YoutubePlaylistDateRangeService(IAppLogger appLogger, IObjectLogger objectLogger, IImport_TournamentRepository importTournamentRepository, IImport_YoutubeDataRepository import_YoutubeDataRepository, IPlayListDateRangeDTOFactory playListDateRangeDTOFactory)
        {
            _appLogger = appLogger;
            _objectLogger = objectLogger;
            _importTournamentRepository = importTournamentRepository;
            _import_YoutubeDataRepository = import_YoutubeDataRepository;
            _playListDateRangeDTOFactory = playListDateRangeDTOFactory;
        }


        public async Task PopulateTournmanetDateRanges(List<Import_YoutubeDataEntity> youtubeEntities)
        {
            TournamentDateRanges = _playListDateRangeDTOFactory.CreateListOfPlaylistDateRangeDTOs(youtubeEntities);

            _appLogger.Info($"{nameof(TournamentDateRanges)} populated with count: {TournamentDateRanges.Count}.");
        }

        public List<PlayListDateRangeResult> ReturnTournamentDateRanges()
        {
            return TournamentDateRanges;
        }






       


    }
}
