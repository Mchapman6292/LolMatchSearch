using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.Western_MatchDTOs;
using LolMatchFilterNew.Domain.DTOs.MatchComparisonResultDTOs;
using LolMatchFilterNew.Domain.DTOs.YoutubeTitleTeamOccurrenceDTOs;
using Domain.DTOs.PlayListDateRangeDTOs;

namespace Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers
{
    public interface IObjectLogger
    {
        void LogWesternMatchDTO(WesternMatchDTO westernMatchDTO);
        void LogTeamnameDTO(TeamNameDTO teamnameDTO);

        void LogProcessedYoutubeDataDTO(YoutubeDataWithTeamsDTO youtubeDTO);

        void LogMatchComparison(MatchComparisonResultDTO match);

        void LogListForCONTROLLERValidateWesternMatches(List<string> noMatchesList);

        void LogFinalizedYoutubeTitleTeamNameOccurrenceCountDTO(List<YoutubeTitleTeamOccurenceDTO> matchResults);

        void LogYoutubeTeamNameOccurenceWithOnlyOneMatch(List<YoutubeTitleTeamOccurenceDTO> matchResults);

 

        void LogPlaylistDateRanges(List<PlayListDateRangeResult> playlistDTOs);

        void LogGameIdsInUpdatedPlaylistDateRangeResult(List<PlayListDateRangeResult> playlistDTOs);

    }
}
