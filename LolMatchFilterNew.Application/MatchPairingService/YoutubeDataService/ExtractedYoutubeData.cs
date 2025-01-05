using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MatchPairingService.YoutubeDataService.ExtractedYoutubeDatas
{
    public class ExtractedYoutubeData
    {
        private readonly IAppLogger _appLogger;
        private readonly IImport_YoutubeDataRepository _ImportYoutubeDataRepository;

        public string GameTitle { get; }

        public DateTime? PublishedAt_Utc { get; set; }

        public string? Team1 { get; }  
        public string? Team2 { get; } 

        public string? Tournament { get; }


        
    }
}
