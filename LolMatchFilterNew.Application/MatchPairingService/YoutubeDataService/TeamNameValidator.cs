

using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameDTOBuilders;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameValidators;

namespace Application.MatchPairingService.YoutubeDataService.TeamNameValidators
{
    public class TeamNameValidator : ITeamNameValidator
    {
        private readonly IAppLogger _appLogger;
        private readonly IObjectLogger _objectLogger;
        private readonly ITeamNameDTOBuilder _teamNameDTOBuilder;
        private readonly List<TeamnameDTO> _TeamNamesAndAbbreviations;
        



        public TeamNameValidator(IAppLogger appLogger, IObjectLogger objectLogger, ITeamNameDTOBuilder teamNameDTOBuilder)
        {
            _appLogger = appLogger;
            _objectLogger = objectLogger;
            _teamNameDTOBuilder = teamNameDTOBuilder;
            _TeamNamesAndAbbreviations = _teamNameDTOBuilder.GetTeamNamesAndAbbreviations();

        }


        public int GetCountOfValidTeams(string extractedTeam1, string extractedTeam2)
        {
            bool team1Valid = ValidateTeamName(extractedTeam1);
            bool team2Valid = ValidateTeamName(extractedTeam2);

            return  (team1Valid ? 1 : 0) + (team2Valid ? 1 : 0); 
    
        }

        public bool ValidateTeamName(string teamName)
        {
            if (MatchesTeamShortName(teamName)) return true;
            if (MatchesTeamMediumName(teamName)) return true;
            if(MatchesTeamInputs(teamName)) return true;

            return false;
        }




        public bool MatchesTeamShortName(string teamName)
        {
            return _TeamNamesAndAbbreviations.Any(t => string.Equals(t.Short, teamName, StringComparison.OrdinalIgnoreCase));
        }

        public bool MatchesTeamMediumName(string teamName)
        {
            return _TeamNamesAndAbbreviations.Any(t => string.Equals(t.Medium, teamName, StringComparison.OrdinalIgnoreCase));
        }

        public bool MatchesTeamInputs(string teamName)
        {
            return _TeamNamesAndAbbreviations.Any(t => t.FormattedInputs != null && t.FormattedInputs.Contains(teamName, StringComparer.OrdinalIgnoreCase));
        }

    }
}
