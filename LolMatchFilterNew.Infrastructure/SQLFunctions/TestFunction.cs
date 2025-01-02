using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using Domain.DTOs.Western_MatchDTOs;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.InfrastructureInterfaces.ITestFunctions;

namespace Infrastructure.SQLFunctions.TestFunctions
{
    public class TestFunction : ITestFunction
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;


        public TestFunction(IAppLogger appLogger, IMatchFilterDbContext matchFilterDbContext)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = matchFilterDbContext;
        }



        public async Task<IEnumerable<WesternMatchDTO>> GetWesternMatches()
        {
            var matches = await _matchFilterDbContext.WesternMatches
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


    }
}
