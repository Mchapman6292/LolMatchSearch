using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Activity = System.Diagnostics.Activity;

namespace LolMatchFilterNew.Domain.Interfaces.IYoutubeTitleMatcher
{
    public interface IYoutubeTitleMatcher
    {
        Task<List<string>> ExtractTeamNames(Activity activity, string youtubeTitle);
        Task<bool> CheckYoutubeTitleFormatForAbbreviatedTeams(Activity activity, string youtubeTitle);
    }
}
