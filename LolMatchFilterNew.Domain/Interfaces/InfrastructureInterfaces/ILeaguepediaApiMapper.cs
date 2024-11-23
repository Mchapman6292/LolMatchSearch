using LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Processed_LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.Import_TeamsTableEntities;
using LolMatchFilterNew.Domain.Entities.Processed_TeamRenameEntities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers
{
    public interface ILeaguepediaApiMapper
    {
        Task<IEnumerable<Import_ScoreboardGamesEntity>> MapLeaguepediaDataToEntity(IEnumerable<JObject> leaguepediaData);

        Task<IEnumerable<Processed_LeagueTeamEntity>> MapLeaguepediaDataToLeagueTeamEntity(IEnumerable<JObject> leaguepediaData);

        Task<IEnumerable<Processed_LeagueTeamEntity>> MapApiDataToLeagueTeamEntityForTeamShort(IEnumerable<JObject> apiData);

        Task<IEnumerable<Processed_TeamRenameEntity>> MapJTokenToTeamRenameEntity(IEnumerable<JObject> apiData);

        Task<IEnumerable<Import_TeamsTableEntity>> MapJTokenToLpediaTeamEntity(IEnumerable<JObject> apiData);
    }
}
