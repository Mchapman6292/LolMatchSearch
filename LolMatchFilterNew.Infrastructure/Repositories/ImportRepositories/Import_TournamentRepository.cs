using Domain.Interfaces.InfrastructureInterfaces.IImportRepositories.IImport_TournamentRepositories;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using Domain.Entities.Imported_Entities.Import_TournamentEntities;
using Domain.DTOs.ImportTournamentDTOs;
using Domain.Interfaces.ApplicationInterfaces.IDTOBuilders.IImportTournamentDTOFactories;

namespace Infrastructure.Repositories.ImportRepositories.Import_TournamentRepositories
{
    public class Import_TournamentRepository : GenericRepository<Import_TournamentEntity>, IImport_TournamentRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;
        private readonly IImportTournamentDTOFactory _importTournamentDtoFactory;

        public Import_TournamentRepository(IAppLogger appLogger, IMatchFilterDbContext matchFilterDbContext, IImportTournamentDTOFactory importTournamentDTOFactory)
            : base(matchFilterDbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = matchFilterDbContext;
            _importTournamentDtoFactory = importTournamentDTOFactory;
        }


        public async Task<List<ImportTournamentDTO>> GetTournamentDTOsAsync()
        {
            var tournaments = await _matchFilterDbContext.Import_Tournament
                .Select(t => _importTournamentDtoFactory.CreateTournamentDTO(
                    t.TournamentName,
                    t.DateStart,
                    t.Date,
                    t.DateStartFuzzy,
                    t.League,
                    t.Region,
                    t.Country,
                    t.Split,
                    t.TournamentLevel,
                    t.IsQualifier,
                    t.AlternativeNames
                ))
                .ToListAsync();

            return tournaments;
        }


        public async Task<List<ImportTournamentDTO>> GetEuNaTournamentDTOsAsync()
        {
            var tournaments = await _matchFilterDbContext.Import_Tournament
                .Where(t => (EF.Functions.ILike(t.TournamentName, "%NA LCS%") ||
                            EF.Functions.ILike(t.TournamentName, "%EU LCS%") ||
                            EF.Functions.ILike(t.TournamentName, "LEC%") ||
                            EF.Functions.ILike(t.TournamentName, "LCS%") ||
                            EF.Functions.ILike(t.TournamentName, "%LEC Spring%") ||
                            EF.Functions.ILike(t.TournamentName, "%LEC Summer%") ||
                            EF.Functions.ILike(t.TournamentName, "%LCS Spring%") ||
                            EF.Functions.ILike(t.TournamentName, "%LCS Summer%")) &&
                            !(EF.Functions.ILike(t.TournamentName, "%MSI%") ||
                              EF.Functions.ILike(t.TournamentName, "%Worlds%") ||
                              EF.Functions.ILike(t.TournamentName, "%World Championship%") ||
                              EF.Functions.ILike(t.TournamentName, "%International%") ||
                              EF.Functions.ILike(t.TournamentName, "%All-Star%") ||
                              EF.Functions.ILike(t.TournamentName, "%Rift Rivals%")))
                .OrderBy(t => t.DateStart)
                .Select(t => _importTournamentDtoFactory.CreateTournamentDTO(
                    t.TournamentName,
                    t.DateStart,
                    t.Date,
                    t.DateStartFuzzy,
                    t.League,
                    t.Region,
                    t.Country,
                    t.Split,
                    t.TournamentLevel,
                    t.IsQualifier,
                    t.AlternativeNames
                ))
                .ToListAsync();

            _appLogger.Info("Method {MethodName} returned {Count} entities", nameof(GetEuNaTournamentDTOsAsync), tournaments.Count);

            return tournaments;
        }
    }
}
