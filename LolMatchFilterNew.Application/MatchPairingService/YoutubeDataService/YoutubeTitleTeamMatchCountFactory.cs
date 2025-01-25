using LolMatchFilterNew.Domain.DTOs.YoutubeTitleTeamOccurrenceDTOs;
using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamMatchCountFactories;


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
                MatchingTeamNameIds = new Dictionary<string, List<string>>(),
                MediumNameCount = mediumNameCount,
                ShortNameCount = shortNameCount,
                MatchingInputsCount = matchingInputsCount,
                MatchingInputs = new List<string>()
            };
        }

 





    }
}

