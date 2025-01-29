using Domain.Enums.TeamNameTypes;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamMatchCountFactories;
using LolMatchFilterNew.Domain.DTOs.YoutubeTitleTeamOccurrenceDTOs;


namespace Application.MatchPairingService.YoutubeDataService.YoutubeTitleTeamMatchCountFactories
{
    public class YoutubeTitleTeamMatchCountFactory : IYoutubeTitleTeamMatchCountFactory
    {


        public YoutubeTitleTeamOccurenceDTO CreateNewYoutubeTitleOccurenceDTO(string youtubeTitle, int longNameCount = 0, int mediumNameCount = 0, int shortNameCount = 0,int matchingInputsCount = 0 ,List<string> matchingInputs = null)
        {
            return new YoutubeTitleTeamOccurenceDTO
            {

                YoutubeTitle = youtubeTitle,
                LongNameCount = longNameCount,
                AllMatchingTeamNameIds = new Dictionary<string, List<(TeamNameType, string)>>(),
                TeamIdsWithMostMatches = new Dictionary<string, List<(TeamNameType, string)>>(),
                MediumNameCount = mediumNameCount,
                ShortNameCount = shortNameCount,
                MatchingInputsCount = matchingInputsCount,
                MatchingInputs = new List<string>()
            };
        }

 





    }
}

