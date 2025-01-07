using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.ITeamNameValidators
{
    public interface ITeamNameValidator
    {
        int GetCountOfValidTeams(string extractedTeam1, string extractedTeam2);
        bool ValidateTeamName(string teamName);
        bool MatchesTeamShortName(string teamName);
        bool MatchesTeamMediumName(string teamName);
        bool MatchesTeamInputs(string teamName);

    }
}
