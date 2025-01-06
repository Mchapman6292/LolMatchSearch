using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.ITeamNameValidators
{
    public interface ITeamNameValidator
    {
        bool ValidateTeamName(string teamName);
        bool VerifyTeamShortName(string teamName);
        bool VerifyTeamMediumName(string teamName);
        bool VerifyFormattedInputs(string teamName);

    }
}
