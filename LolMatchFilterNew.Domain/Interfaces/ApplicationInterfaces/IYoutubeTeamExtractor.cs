using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
using LolMatchFilterNew.Domain.DTOs.MatchComparisonResults;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamExtractors
{
    public interface IYoutubeTeamExtractor
    {
        public bool MatchVsPatternAndUpdateMatchComparisonResultEntity(YoutubeVideoEntity youtubeVideo, MatchComparisonResult comparisonResult);
    }
}
