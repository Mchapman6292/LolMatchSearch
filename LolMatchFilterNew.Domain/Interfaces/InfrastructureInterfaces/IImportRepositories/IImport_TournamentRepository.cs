using Domain.DTOs.ImportTournamentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InfrastructureInterfaces.IImportRepositories.IImport_TournamentRepositories
{
    public interface IImport_TournamentRepository
    {
        Task<List<ImportTournamentDTO>> GetTournamentDTOsAsync();

        Task<List<ImportTournamentDTO>> GetEuNaTournamentDTOsAsync();
    }
}
