using Domain.DTOs.ImportTournamentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.IDTOBuilders.IImportTournamentDTOFactories
{
    public interface IImportTournamentDTOFactory
    {
        ImportTournamentDTO CreateTournamentDTO(string tournamentName, DateTime? dateStartUtc, DateTime? dateUtc, string? league, string? region, string? country, string? split, string? tournamentLevel, bool isQualifier, string? alternativeNames);

    }
}
