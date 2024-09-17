using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using Google.Apis.YouTube.v3;


namespace LolMatchFilterNew.Domain.UnUsedYoutubeClass
{
    public class UnUsedYoutubeClass
    {
        private readonly IAppLogger _appLogger;
        private readonly IActivityService _activityService;
        private readonly YouTubeService _youtubeService;




        public UnUsedYoutubeClass(IAppLogger appLogger, IActivityService activityService)
        {
            _appLogger = appLogger;
            _activityService = activityService;
        }



    }

  
}
