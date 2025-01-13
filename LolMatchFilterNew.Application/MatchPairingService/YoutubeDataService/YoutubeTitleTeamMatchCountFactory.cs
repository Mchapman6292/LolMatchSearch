using Domain.DTOs.YoutubeTitleTeamNameOccurrenceCountDTOs;
using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamMatchCountFactories;


namespace Application.MatchPairingService.YoutubeDataService.YoutubeTitleTeamMatchCountFactories
{
    public class YoutubeTitleTeamMatchCountFactory : IYoutubeTitleTeamMatchCountFactory
    {


        public YoutubeTitleTeamNameOccurrenceCountDTO CreateNewYoutubeTitleOccurenceDTO(TeamNameDTO? teamNameDTO, string youtubeTitle, int longNameMatch = 0, int mediumNameMatch = 0, int shortNameMatch = 0, List<string> matchingInputs = null)
        {
            return new YoutubeTitleTeamNameOccurrenceCountDTO
            {
                TeamNameDto = teamNameDTO,
                LongNameCount = longNameMatch,
                YoutubeTitle = youtubeTitle,
                LongNameMatches = new List<string>(),
                MediumNameCount = mediumNameMatch,
                MediumNameMatches = new List<string>(),
                ShortNameCount = shortNameMatch,
                ShortNameMatches = new List<string>(),
                MatchingInputs = new List<string>()
            };
        }

        public void UpdateTeamNameDtoForMatch(YoutubeTitleTeamNameOccurrenceCountDTO dto, TeamNameDTO? newTeamDto)
        {
            dto.TeamNameDto = newTeamDto;
        }

        public void UpdateYoutubeTitle(YoutubeTitleTeamNameOccurrenceCountDTO dto, string title)
        {
            dto.YoutubeTitle = title;
        }







    }
}

