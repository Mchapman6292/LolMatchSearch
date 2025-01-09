using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using Domain.DTOs.TeamnameDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IImport_TeamNameServices;


namespace Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IImport_TeamNameServices
{
    public interface IImport_TeamNameService
    {
        Task PopulateImport_TeamNameAllNames();

        Task TESTLogTeamNameAbbreviations();
        List<TeamNameDTO> ReturnImport_TeamNameAllNames();


        void TESTCheckAllProcessedEuAndNaAgainstKnownAbbreviations(HashSet<string> distinctTeamNames, List<TeamNameDTO> teamNameDTOs);
    }
}
