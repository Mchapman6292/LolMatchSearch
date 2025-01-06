using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.Western_MatchDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.ITeamnameDTOBuilders
{
    public interface ITeamnameDTOBuilder
    {
        List<TeamnameDTO> GetTeamNamesAndAbbreviations();

        Task PopulateTeamNamesAndAbbreviations();

        TeamnameDTO BuildTeamnameDTO(string teamNameId, string? longname, string? mediumName, string? shortname, List<string>? inputs);


        Task TESTLogTeamNameAbbreviations();

        Task<List<TeamnameDTO>> BuildTeamnameDTOFromGetWesternMatches(List<WesternMatchDTO> westernMatches);
    }
}
