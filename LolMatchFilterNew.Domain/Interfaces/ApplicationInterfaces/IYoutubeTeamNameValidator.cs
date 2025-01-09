using Domain.DTOs.TeamnameDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameValidators
{
    public interface IYoutubeTeamNameValidator
    {

        bool ValidateTeamName(string teamName, List<TeamNameDTO> teamNamesAndAbbreviations);
        bool MatchesTeamShortName(string teamName, List<TeamNameDTO> teamNamesAndAbbreviations);
        bool MatchesTeamMediumName(string teamName, List<TeamNameDTO> teamNamesAndAbbreviations);
        bool MatchesTeamInputs(string teamName, List<TeamNameDTO> teamNamesAndAbbreviations);
        
    }
}
