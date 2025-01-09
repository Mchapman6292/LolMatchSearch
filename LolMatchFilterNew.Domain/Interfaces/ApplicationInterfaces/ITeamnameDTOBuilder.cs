using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.Western_MatchDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.ITeamNameDTOBuilders
{
    public interface ITeamNameDTOBuilder
    {

        TeamNameDTO BuildTeamnameDTO(string teamNameId, string? longname, string? mediumName, string? shortname, List<string>? inputs);


        Task<List<TeamNameDTO>> BuildTeamNameDTOListFromGetWesternMatchesAsync(List<WesternMatchDTO> westernMatches);
    }
}
