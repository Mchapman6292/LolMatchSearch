

using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameDTOBuilders;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameValidators;

namespace Application.MatchPairingService.YoutubeDataService.YoutubeTeamNameValidators
{
    public class YoutubeTeamNameValidator : IYoutubeTeamNameValidator
    {
        private readonly IAppLogger _appLogger;
        private readonly IObjectLogger _objectLogger;
        private readonly ITeamNameDTOBuilder _teamNameDTOBuilder;

        public YoutubeTeamNameValidator(IAppLogger appLogger, IObjectLogger objectLogger, ITeamNameDTOBuilder teamNameDTOBuilder)
        {
            _appLogger = appLogger;
            _objectLogger = objectLogger;
            _teamNameDTOBuilder = teamNameDTOBuilder;
        }

        

        public bool ValidateTeamName(string extractedWordFromYoutubeTitle, List<TeamNameDTO> importTeamNameAllNames)
        {
            if (MatchesTeamShortName(extractedWordFromYoutubeTitle, importTeamNameAllNames)) return true;
            if (MatchesTeamMediumName(extractedWordFromYoutubeTitle, importTeamNameAllNames)) return true;
            if (MatchesTeamInputs(extractedWordFromYoutubeTitle, importTeamNameAllNames)) return true;
            return false;
        }

        public bool MatchesTeamShortName(string teamName, List<TeamNameDTO> teamNamesAndAbbreviations)
        {
            return teamNamesAndAbbreviations.Any(t => string.Equals(t.ShortName, teamName, StringComparison.OrdinalIgnoreCase));
        }

        public bool MatchesTeamMediumName(string teamName, List<TeamNameDTO> teamNamesAndAbbreviations)
        {
            return teamNamesAndAbbreviations.Any(t => string.Equals(t.MediumName, teamName, StringComparison.OrdinalIgnoreCase));
        }

        public bool MatchesTeamInputs(string teamName, List<TeamNameDTO> teamNamesAndAbbreviations)
        {
            return teamNamesAndAbbreviations.Any(t => t.FormattedInputs != null &&
                t.FormattedInputs.Contains(teamName, StringComparer.OrdinalIgnoreCase));
        }




    }
}
