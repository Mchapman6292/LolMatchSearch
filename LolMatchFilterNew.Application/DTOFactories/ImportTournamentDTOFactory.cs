using Domain.DTOs.ImportTournamentDTOs;
using Domain.Interfaces.ApplicationInterfaces.IDTOBuilders.IImportTournamentDTOFactories;

namespace Application.DTOBuilders.ImportTournamentDTOFactories
{
    public class ImportTournamentDTOFactory : IImportTournamentDTOFactory
    {
        public ImportTournamentDTO CreateTournamentDTO(
            string tournamentName,
            DateTime? dateStartUtc,
            DateTime? dateUtc,
            string? league,
            string? region,
            string? country,
            string? split,
            string? tournamentLevel,
            bool isQualifier,
            string? alternativeNames)
        {
            return new ImportTournamentDTO
            {
                TournamentName = tournamentName,
                DateStartUtc = dateStartUtc,
                DateUtc = dateUtc,
                League = league,
                Region = region,
                Country = country,
                Split = split,
                TournamentLevel = tournamentLevel,
                IsQualifier = isQualifier,
                AlternativeNames = alternativeNames
            };
        }
    }
}