using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.Western_MatchDTOs;
using LolMatchFilterNew.Domain.DTOs.MatchComparisonResultDTOs;
using Domain.DTOs.YoutubeTitleTeamNameOccurrenceCountDTOs;

namespace Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers
{
    public interface IObjectLogger
    {
        void LogWesternMatchDTO(WesternMatchDTO westernMatchDTO);
        void LogTeamnameDTO(TeamNameDTO teamnameDTO);

        void LogProcessedYoutubeDataDTO(YoutubeDataWithTeamsDTO youtubeDTO);

        void LogMatchComparison(MatchComparisonResultDTO match);

        void LogListForCONTROLLERValidateWesternMatches(List<string> noMatchesList);

        void LogFinalizedYoutubeTitleTeamNameOccurrenceCountDTO(List<YoutubeTitleTeamNameOccurrenceCountDTO> matchResults);

        void LogYoutubeTeamNameOccurenceWithOnlyOneMatch(List<YoutubeTitleTeamNameOccurrenceCountDTO> matchResults);

    }
}
