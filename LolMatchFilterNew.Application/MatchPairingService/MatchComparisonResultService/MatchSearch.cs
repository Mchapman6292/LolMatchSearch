using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_ScoreboardGamesRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using System.Drawing;
using System.Text.RegularExpressions;
using LolMatchFilterNew.Domain.DTOs.YoutubeVideoDTOs;
using Xceed.Document.NET;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Helpers.ApiHelper;
using LolMatchFilterNew.Infrastructure.Logging.AppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchSearches;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;


namespace Application.MatchPairingService.MatchComparisonResultService.MatchSearches
{
    public class MatchSearch : IMatchSearch
    {
        private readonly IAppLogger _appLogger;
        private readonly IImport_ScoreboardGamesRepository _leaguepediaMatchDetailRepository;
        private readonly IApiHelper _apiHelper;


        // Looks for names seperated by |


        public MatchSearch(IAppLogger appLogger, IImport_ScoreboardGamesRepository leaguepediaMatchDetailRepository, IApiHelper apiHelper)
        {
            _appLogger = appLogger;
            _leaguepediaMatchDetailRepository = leaguepediaMatchDetailRepository;
            _apiHelper = apiHelper;
        }






        public async Task CheckYoutubeTitleForVsTeams(Import_YoutubeDataEntity youtubeVideoEntity)
        {

        }




    }

}


