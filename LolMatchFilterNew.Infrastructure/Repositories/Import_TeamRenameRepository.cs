
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities;

namespace LolMatchFilterNew.Infrastructure.Repositories.TeamRenameRepositories
{
    public class Import_TeamRenameRepository : GenericRepository<Import_TeamRenameRepository>, IImport_TeamRenameRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public Import_TeamRenameRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger)
          : base(dbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
        }

        // Retrieves all current team names by retrieving NewNames which do not appear in OriginalName
        public async Task<List<string>> GetCurrentTeamNamesAsync()
        {
            var teamRenames = await _matchFilterDbContext.Import_TeamRename.ToListAsync();

            var originalNames = teamRenames.Select(x => x.OriginalName).ToHashSet();

            return teamRenames
                .Select(x => x.NewName)
                .Where(newName => !originalNames.Contains(newName))
                .Distinct()
                .ToList();
        }

        // Retrieves all results from TeamReanmes which has kept the format of the data from Leaguepedia. 
        public async Task<List<Import_TeamRenameEntity>> GetAllTeamRenameValuesAsync()
        {
            return await _matchFilterDbContext.Import_TeamRename.ToListAsync();
        }


        // Iteratively  adds OriginalNames to NewNames until no more entries found for each OriginalName
        public async Task<Dictionary<string, List<string>>> AddOriginalNameToNewNameAsync()
        {
            Dictionary<string, List<string>> teamNameHistory = new();
            var allTeamRenames = await _matchFilterDbContext.Import_TeamRename.ToListAsync();
            var currentNames = await GetCurrentTeamNamesAsync();

            foreach (var currentName in currentNames)
            {
                var previousNames = new List<string>();
                var namesToProcess = new Queue<string>();
                namesToProcess.Enqueue(currentName);

                // Track processed names to avoid infinite loop
                var processedNames = new HashSet<string>();

                while (namesToProcess.Count > 0)
                {
                    var nameToCheck = namesToProcess.Dequeue();

                    if (processedNames.Contains(nameToCheck))
                        continue;


                    var previousIterations = allTeamRenames
                        .Where(x => x.NewName == nameToCheck)
                        .Select(x => x.OriginalName);

                    foreach (var previousName in previousIterations)
                    {
                        if (!processedNames.Contains(previousName))
                        {
                            previousNames.Add(previousName);
                            namesToProcess.Enqueue(previousName);
                        }
                    }

                    processedNames.Add(nameToCheck);
                }
                teamNameHistory[currentName] = previousNames;
            }
            return teamNameHistory;
        }

    }
}
