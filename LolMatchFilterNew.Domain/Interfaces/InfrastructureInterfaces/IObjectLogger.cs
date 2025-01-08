using Domain.DTOs.Processed_YoutubeDataDTOs;
using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.Western_MatchDTOs;

namespace Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers
{
    public interface IObjectLogger
    {
        void LogWesternMatchDTO(WesternMatchDTO westernMatchDTO);
        void LogTeamnameDTO(TeamnameDTO teamnameDTO);

        void LogProcessedYoutubeDataDTO(Processed_YoutubeDataDTO youtubeDTO);
    }
}
