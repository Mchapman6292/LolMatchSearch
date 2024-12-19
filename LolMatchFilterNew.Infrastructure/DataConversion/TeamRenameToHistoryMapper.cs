using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameToHistoryMappers;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;

namespace LolMatchFilterNew.Infrastructure.DataConversion.TeamRenameToHistoryMappers
{
    /// <summary>
    /// 
    /// COMPLETE METHOD CALLING CHAIN:
    /// 1. Entry Point: MapTeamRenameToHistoryAsync()
    ///    │
    ///    ├─> Calls AddOriginalNameToNewNameAsync() (or TESTAddOriginalNameToNewNameAsync for testing)
    ///    │   │
    ///    │   ├─> Calls Repository.GetAllTeamRenameValuesAsync()
    ///    │   │   - Fetches all team rename records from database
    ///    │   │
    ///    │   ├─> Calls Repository.GetCurrentTeamNamesAsync() 
    ///    │   │   - Gets list of current team names (or TEST version for 10 teams)
    ///    │   │
    ///    │   └─> For each current team name:
    ///    │       - Processes name history using Queue/HashSet approach
    ///    │       - Returns Dictionary<CurrentName, List<PreviousNames>>
    ///    │
    ///    └─> Converts results to List<Processed_TeamNameHistoryEntity>

    public class TeamRenameToHistoryMapper : ITeamRenameToHistoryMapper
    {
        private readonly IAppLogger _appLogger;
        private readonly IImport_TeamRenameRepository _teamRenameRepository;
        private readonly IGenericRepository<Processed_TeamNameHistoryEntity> _teamHistoryGenericRepository;


        public TeamRenameToHistoryMapper(IAppLogger appLogger, IImport_TeamRenameRepository teamRenameRepository, IGenericRepository<Processed_TeamNameHistoryEntity> teamHistoryGenericRepository)
        {
            _appLogger = appLogger;
            _teamRenameRepository = teamRenameRepository;
            _teamHistoryGenericRepository = teamHistoryGenericRepository;
        }



        public async Task<List<Processed_TeamNameHistoryEntity>> MapTeamRenameToHistoryAsync()
        {
            var teamHistories = await AddOriginalNameToNewNameAsync();

            var historyEntities = teamHistories.Select(kvp => new Processed_TeamNameHistoryEntity
            {
                CurrentTeamName = kvp.Key,

                // Stores previous TeamNames as comma separated string in database. 
                NameHistory = kvp.Value
            }).ToList();



            _appLogger.Info($"Mapped {historyEntities.Count} team histories successfully");
            return historyEntities;
            }





        public async Task<Dictionary<string, List<string>>> AddOriginalNameToNewNameAsync()
        {
            Dictionary<string, List<string>> teamNameHistory = new();
            var allTeamRenames = await _teamRenameRepository.GetAllTeamRenameValuesAsync();
            var currentNames = await _teamRenameRepository.GetCurrentTeamNamesAsync();

            foreach (var currentName in currentNames)
            { 
                var previousNames = new HashSet<string>(); 
                var processedNames = new HashSet<string> { currentName }; 
                var namesToProcess = new Queue<string>();
                namesToProcess.Enqueue(currentName);

                while (namesToProcess.Count > 0)
                {
                    var nameToCheck = namesToProcess.Dequeue();

                    var allPreviousNames = allTeamRenames
                        .Where(x => x.NewName.Equals(nameToCheck, StringComparison.OrdinalIgnoreCase))
                        .Select(x => x.OriginalName)
                        .ToList();

                    foreach (var previousName in allPreviousNames)
                    {
                        if (processedNames.Add(previousName))  
                        {
                            previousNames.Add(previousName);
                            namesToProcess.Enqueue(previousName);
                        }
                    }
                }

                teamNameHistory[currentName] = previousNames.ToList();
            }

            _appLogger.Info($"Processed {teamNameHistory.Count} teams with name histories");
            return teamNameHistory;
        }








        public async Task<Dictionary<string, List<string>>> TESTAddOriginalNameToNewNameAsync()
        {
            Dictionary<string, List<string>> teamNameHistory = new();
            var allTeamRenames = await _teamRenameRepository.GetAllTeamRenameValuesAsync();
            var currentNames = await _teamRenameRepository.TESTGet10CurrentTeamNamesAsync();

            foreach (var currentName in currentNames)
            {
                var previousNames = new HashSet<string>(); 
                var processedNames = new HashSet<string> { currentName };
                var namesToProcess = new Queue<string>();
                namesToProcess.Enqueue(currentName);

                while (namesToProcess.Count > 0)
                {
                    var nameToCheck = namesToProcess.Dequeue();


                    var allPreviousNames = allTeamRenames
                        .Where(x => x.NewName.Equals(nameToCheck, StringComparison.OrdinalIgnoreCase))
                        .Select(x => x.OriginalName)
                        .ToList();

                    foreach (var previousName in allPreviousNames)
                    {
       
                        if (processedNames.Add(previousName)) 
                        {
                            previousNames.Add(previousName);
                            namesToProcess.Enqueue(previousName);
                        }
                    }
                }

                teamNameHistory[currentName] = previousNames.ToList();
            }

            _appLogger.LogTeamNameHistory(teamNameHistory);
            return teamNameHistory;
        
        }



    }
}
