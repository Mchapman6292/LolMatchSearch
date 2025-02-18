﻿using Domain.Interfaces.InfrastructureInterfaces.IImport_TeamnameRepositories;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameDTOBuilders;
using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using Domain.DTOs.Western_MatchDTOs;
using System.Linq;

namespace Application.MatchPairingService.ScoreboardGameService.TeamnameDTOBuilders
{
    public class TeamNameDTOBuilder  :ITeamNameDTOBuilder
    {
        private readonly IAppLogger _appLogger;
        private readonly IImport_TeamnameRepository _teamnameRepository;
        private readonly IObjectLogger _objectLogger;
        private readonly IStoredSqlFunctionCaller _storedSqlFunctionCaller;
        private List<TeamNameDTO> TeamNamesAndAbbreviations {  get; set; } 


        public TeamNameDTOBuilder(IAppLogger appLogger, IImport_TeamnameRepository teamnameRepository, IObjectLogger objectLogger, IStoredSqlFunctionCaller storedSqlFunctionCaller)
        {
            _appLogger = appLogger;
            _teamnameRepository = teamnameRepository;
            TeamNamesAndAbbreviations = new List<TeamNameDTO>();    
            _objectLogger = objectLogger;
            _storedSqlFunctionCaller = storedSqlFunctionCaller;
        }




        public TeamNameDTO BuildTeamNameDTO(string teamNameId, string? longName, string? mediumName, string? shortName, List<string> inputs) 
        {
            return new TeamNameDTO
            {
                TeamNameId = teamNameId,
                LongName = longName,
                MediumName = mediumName,
                ShortName = shortName,
                FormattedInputs = inputs != null ? ParseTeamnameInputsColumn(inputs) : null
            };    
        }



        // Parses list of teamname inputs from format {"name1;name2"} to ["name1", "name2"]
        private List<string> ParseTeamnameInputsColumn(List<string>? inputs)
        {
            if (inputs == null || !inputs.Any()) return new List<string>();

            return inputs.SelectMany(input => input
                            .Trim('{', '}', '"')
                            .Split(';', StringSplitOptions.RemoveEmptyEntries)
                            .Select(name => name.Trim()))
                            .ToList();
        }








        public async Task<List<TeamNameDTO>> BuildTeamNameDTOListFromGetWesternMatchesAsync(List<WesternMatchDTO> westernMatches)
        {

            List<TeamNameDTO> teamnames = new List<TeamNameDTO>();

            int count = 0;

            foreach(var match in westernMatches)
            {
                bool team1Exisits = teamnames.Any(t => t.TeamNameId == match.Team1Team_Id);

                if(!team1Exisits)
                {
                    teamnames.Add(BuildTeamNameDTO(match.Team1Team_Id, match.Team1_Longname, match.Team1_Medium, match.Team1_Short, match.Team1_Inputs));
                }

                bool team2Exists = teamnames.Any(t => t.TeamNameId == match.Team2Team_Id);

                if(!team2Exists)
                {
                    teamnames.Add(BuildTeamNameDTO(match.Team2Team_Id, match.Team2_Longname, match.Team2_Medium, match.Team2_Short, match.Team2_Inputs));
                }
                count++;
            }
            TeamNamesAndAbbreviations = teamnames;
            _appLogger.Info($"{nameof(BuildTeamNameDTO)} complete, TeamNameDTO count: {teamnames.Count}.");
            return teamnames;
        }





    }
}
