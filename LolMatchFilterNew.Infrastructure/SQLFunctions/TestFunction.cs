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
            return await _matchFilterDbContext.WesternMatches
            .FromSqlRaw("SELECT * FROM get_western_matches()")
            .ToListAsync();
        }


    }
}
