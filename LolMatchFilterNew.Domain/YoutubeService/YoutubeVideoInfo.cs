using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using Google.Apis.YouTube.v3;


namespace LolMatchFilterNew.Domain.YoutubeVideoInfo
{
    public class YoutubeVideoInfo
    {
        private readonly IAppLogger _appLogger;
        private readonly IActivityService _activityService;
        private readonly YouTubeService _youtubeService;




        public YoutubeVideoInfo(IAppLogger appLogger, IActivityService activityService)
        {
            _appLogger = appLogger;
            _activityService = activityService;
        }



    }

  
}
