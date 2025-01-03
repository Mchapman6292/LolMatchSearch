﻿using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using Domain.DTOs.Western_MatchDTOs;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using Domain.DTOs.TeamnameDTOs;

namespace Infrastructure.SQLFunctions.StoredSqlFunctionCallers
{
    public class StoredSqlFunctionCaller : IStoredSqlFunctionCaller
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;


        public StoredSqlFunctionCaller(IAppLogger appLogger, IMatchFilterDbContext matchFilterDbContext)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = matchFilterDbContext;
        }



        public async Task<IEnumerable<WesternMatchDTO>> GetWesternMatches()
        {
            var matches = await _matchFilterDbContext.WesternMatchesSet
            .FromSqlRaw("SELECT * FROM get_western_matches()")
            .ToListAsync();

            foreach (var match in matches)
            {
                match.Team1_Inputs = ParseteamnameInputForWesternMatches(match.Team1_Inputs);
                match.Team2_Inputs = ParseteamnameInputForWesternMatches(match.Team2_Inputs);
            }

            return matches;
        }


        private List<string>? ParseteamnameInputForWesternMatches(List<string>? inputs)
        {
            if (inputs == null || !inputs.Any()) return null;

            return inputs.SelectMany(input => input
                            .Trim('{', '}', '"')
                            .Split(';', StringSplitOptions.RemoveEmptyEntries)
                            .Select(name => name.Trim()))
                            .ToList();
        }




        public async Task<IEnumerable<TeamnameDTO>> GetWesternTeamsWithRegions()
        {
            var matches = await _matchFilterDbContext.TeamnamesSet
           .FromSqlRaw("SELECT * FROM get_western_teams_with_regions()")
           .ToListAsync();

            foreach (var match in matches)
            {
                match.FormattedInputs = ParseteamnameInputForWesternMatches(match.FormattedInputs);
            }
            return matches;
        }


    }
}
