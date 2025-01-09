using Domain.DTOs.Processed_YoutubeDataDTOs;
using Domain.DTOs.TeamnameDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.ITeamNameServices
{
    public interface ITeamNameService
    {
        void PopulateTeamVariations(IEnumerable<TeamnameDTO> teamNames);

        HashSet<string> GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO(List<Processed_YoutubeDataDTO> processed_YoutubeDataDTOs);

        void TESTCheckAllProcessedEuAndNaAgainstKnownAbbreviations(HashSet<string> distinctTeamNames, List<TeamnameDTO> teamNameDTOs);
    }
}
