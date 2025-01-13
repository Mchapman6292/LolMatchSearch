using Domain.DTOs.YoutubeTitleTeamNameOccurrenceCountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders
{
    public interface IYoutubeTitleTeamNameFinder
    {
        void IncrementAllCountsForMatchesInTitle(string youtubeTitle, List<YoutubeTitleTeamNameOccurrenceCountDTO> youtubeTitleTeamMatchCounts);

        void IncrementShortNameMatchesInTitle(string youtubeTitle, List<YoutubeTitleTeamNameOccurrenceCountDTO> listOfTitleMatches);

        void IncrementMediumNameMatches(string youtubeTitle, List<YoutubeTitleTeamNameOccurrenceCountDTO> listOfTitleMatches);

        void IncrementLongNameMatches(string youtubeTitle, List<YoutubeTitleTeamNameOccurrenceCountDTO> listOfTitleMatches);

        void AddFormattedInputMatches(string youtubeTitle, List<YoutubeTitleTeamNameOccurrenceCountDTO> listOfTitleMatches);
    }
}
