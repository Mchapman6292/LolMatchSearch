using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;
using LolMatchFilterNew.Domain.Interfaces.IHttpJsonServices;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Serilog;
using System.Diagnostics;
using System.Text.Json;

namespace LolMatchFilterNew.infrastructure.HttpJsonServices
{
    public class HttpJsonService : IHttpJsonService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAppLogger _appLogger;


        public HttpJsonService(IHttpClientFactory HTTPClientFactory, IAppLogger appLogger)
        {
            _httpClientFactory = HTTPClientFactory;
            _appLogger = appLogger;
        }


        public async Task<JObject> FetchLeaguepediaJsonDataAsync(Activity activity, string url)
        {
            _appLogger.Info($"{nameof(FetchLeaguepediaJsonDataAsync)} Started.");
            try
            {
                using HttpClient client = _httpClientFactory.CreateClient();
                using HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();


                string responseBody = await response.Content.ReadAsStringAsync();
               

                var result = JObject.Parse(responseBody);

                return result;
            }
            catch (HttpRequestException ex)
            {
                _appLogger.Error($"HTTP request failed for URL: {url}, TraceId: {activity.TraceId}.", ex);
                throw;

            }
            catch (JsonException ex)
            {
                _appLogger.Error($"JSON parsing failed for URL: {url}, TraceId: {activity.TraceId}.", ex);
                throw;
            }
        }


    }
}
