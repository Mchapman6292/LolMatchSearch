using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Entities.LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.LpediaTeamEntities;
using LolMatchFilterNew.Domain.Entities.TeamRenamesEntities;
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
        Task<IEnumerable<LeaguepediaMatchDetailEntity>> MapLeaguepediaDataToEntity(IEnumerable<JObject> leaguepediaData);

        Task<IEnumerable<LeagueTeamEntity>> MapLeaguepediaDataToLeagueTeamEntity(IEnumerable<JObject> leaguepediaData);

        Task<IEnumerable<LeagueTeamEntity>> MapApiDataToLeagueTeamEntityForTeamShort(IEnumerable<JObject> apiData);

        Task<IEnumerable<TeamRenameEntity>> MapJTokenToTeamRenameEntity(IEnumerable<JObject> apiData);

        Task<IEnumerable<LpediaTeamEntity>> MapJTokenToLpediaTeamEntity(IEnumerable<JObject> apiData);
    }
}
