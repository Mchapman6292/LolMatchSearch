using Domain.Interfaces.InfrastructureInterfaces.Repositories.MultipleTableRepositories.ICrossTableRepositories;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using Domain.DTOs.InfrastructureDTO.TeamsInLeagueDTOS;

namespace Infrastructure.Repositories.MultipleTableRepositories.CrossTableRepositories
{
    public class CrossTableRepository : GenericRepository<CrossTableRepository>, ICrossTableRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public CrossTableRepository(
            IMatchFilterDbContext dbContext,
            IAppLogger appLogger)
            : base(dbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
        }




        public async Task<List<TeamsInLeagueDTO>> GetTeamLeagueHistoryAsync(IEnumerable<string> targetLeagues)
        {
            var tournaments = await _matchFilterDbContext.Import_Tournament
                .Where(t => targetLeagues.Contains(t.League) && t.Year != null)
                .Select(t => new { t.TournamentName, t.League, t.Year })
                .ToListAsync();

            var games = await _matchFilterDbContext.Import_ScoreboardGames
                .Where(sg => tournaments.Select(t => t.TournamentName).Contains(sg.Tournament)
                       && sg.Team1 != null && sg.Team2 != null)
                .Select(sg => new
                {
                    sg.Team1,
                    sg.Team2,
                    Tournament = sg.Tournament,
                })
                .ToListAsync();

            var results = games
                .Join(tournaments,
                    g => g.Tournament,
                    t => t.TournamentName,
                    (g, t) => new
                    {
                        Game = g,
                        Tournament = t
                    })
                .SelectMany(x => new[]
                {
           new { TeamName = x.Game.Team1, League = x.Tournament.League, Year = x.Tournament.Year },
           new { TeamName = x.Game.Team2, League = x.Tournament.League, Year = x.Tournament.Year }
                })
                .Where(x => !x.TeamName.Contains("TBD", StringComparison.OrdinalIgnoreCase))
                .GroupBy(x => new { x.TeamName, x.Year })
                .Select(g => new TeamsInLeagueDTO
                {
                    TeamName = g.Key.TeamName,
                    Year = g.Key.Year,
                    Leagues = string.Join(", ", g.Select(x => x.League).Distinct().OrderBy(x => x)),
                    NumberOfLeagues = g.Select(x => x.League).Distinct().Count()
                })
                .OrderByDescending(x => x.Year)
                .ThenBy(x => x.Leagues)
                .ToList();

            return results;
        }




    }
}

