using Domain.DTOs.TeamnameDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.ITeamnameDTOBuilders
{
    public interface ITeamnameDTOBuilder
    {
        Task PopulateTeamNamesAndAbbreviations();

        TeamnameDTO BuildTeamnameDTO(string? longname, string? shortname, string? mediumName, List<string>? inputs);

        List<TeamnameDTO> GetTeamNamesAndAbbreviations();

        Task TESTLogTeamNameAbbreviations();
    }
}
