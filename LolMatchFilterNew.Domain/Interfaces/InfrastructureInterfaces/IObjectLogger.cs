using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.Western_MatchDTOs;

namespace Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers
{
    public interface IObjectLogger
    {
        void LogWesternMatchDTO(WesternMatchDTO westernMatchDTO);
        void LogTeamnameDTO(TeamNameDTO teamnameDTO);

        void LogProcessedYoutubeDataDTO(YoutubeDataWithTeamsDTO youtubeDTO);
    }
}
