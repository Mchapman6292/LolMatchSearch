using Newtonsoft.Json.Linq;
using System.Web;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaApis;
using LolMatchFilterNew.Domain.Interfaces.IHttpJsonServices;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryService;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaAPILimiter;
using Activity = System.Diagnostics.Activity;

namespace LolMatchFilterNew.Domain.Apis.LeaguepediaApis
{
    // API limits : 500 results per query
    // From help page "Please add a small delay between queries (1-2 seconds). If the server gets stressed as a result of a huge number of queries, you may be rate-limited for some time."

    public class LeaguepediaApi : ILeaguepediaApi
    {
        private readonly IAppLogger _appLogger;
        private readonly IHttpJsonService _httpJsonService;
        private readonly IActivityService _activityService;
        private readonly IApiHelper _apiHelper;
        private readonly ILeaguepediaQueryService _leaguepediaQueryService;
        private readonly ILeaguepediaAPILimiter _leaguepediaApiLimiter;


        private static readonly HttpClient client = new HttpClient();
        private static readonly string SaveDirectory = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
    "LolMatchReports");

        public LeaguepediaApi(IHttpJsonService httpJsonService, IAppLogger appLogger, IActivityService activityService, IApiHelper apiHelper, ILeaguepediaQueryService leaguepediaQueryService, ILeaguepediaAPILimiter leaguepediaAPILimiter)
        {
            _httpJsonService = httpJsonService;
            _appLogger = appLogger;
            _activityService = activityService;
            _apiHelper = apiHelper;
            _leaguepediaQueryService = leaguepediaQueryService;
            _leaguepediaApiLimiter = leaguepediaAPILimiter;
        }


        public async Task GetAllGamesForSeason(string tournament, string split, int year)
        {
            _appLogger.Info($"STarting {nameof(GetAllGamesForSeason)}.");

            try
            {
                await _leaguepediaApiLimiter.WaitForNextRequestAsync();


            }


        }
  
           

       


    }
}
