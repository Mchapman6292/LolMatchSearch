using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamExtractors
{
    public interface IYoutubeTeamExtractor
    {
        List<string> ExtractTeamNamesAroundVsKeyword(string youtubeTitle);

        Task<List<string>> ExtractEndTeamStringForMultipleAsync(List<Import_YoutubeDataEntity> youtubeVideos);

        Task<string> ExtractEndTeamStringAsync(Import_YoutubeDataEntity youtubeVideo);
    }
}
