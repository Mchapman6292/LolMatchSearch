using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using Domain.DTOs.Western_MatchDTOs;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using Domain.DTOs.TeamnameDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames;

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



        public async Task<List<WesternMatchDTO>> GetWesternMatchesAsync()
        {
            var matches = await _matchFilterDbContext.WesternMatchesSet
            .FromSqlRaw("select * from region_functions.get_western_matches()")
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

        public async Task<List<Import_YoutubeDataEntity>> GetYoutubeDataEntitiesForWesternTeamsAsync()
        {
            return await _matchFilterDbContext.Import_YoutubeData
                            .FromSqlRaw("select * from youtube_functions.get_euna_videos_by_playlist_title();")
                            .ToListAsync();
        }


        public async Task<List<Import_TeamnameEntity>> GetAllWesternTeamsAsync()
        {
            return await _matchFilterDbContext.Import_Teamname
                        .FromSqlRaw("select * from region_functions.get_western_teams()")
                        .ToListAsync();
        }













    }
}
