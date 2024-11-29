
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamsTableEntities;

using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRedirectEntities;

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers
{
    public interface ILeaguepediaApiMapper
    {
        Task<IEnumerable<Import_ScoreboardGamesEntity>> MapSGamesJobjectToEntity(IEnumerable<JObject> leaguepediaData);

        Task<IEnumerable<Import_TeamRedirectEntity>> MapTeamRedirectsToEntity(IEnumerable<JObject> leaguepediaData);

        Task<IEnumerable<Processed_LeagueTeamEntity>> MapLeaguepediaDataToLeagueTeamEntity(IEnumerable<JObject> leaguepediaData);

        Task<IEnumerable<Processed_LeagueTeamEntity>> MapApiDataToLeagueTeamEntityForTeamShort(IEnumerable<JObject> apiData);

        Task<IEnumerable<Import_TeamRenameEntity>> MapJTokenToTeamRenameEntity(IEnumerable<JObject> apiData);

        Task<IEnumerable<Import_TeamsTableEntity>> MapJTokenToLpediaTeamEntity(IEnumerable<JObject> apiData);
    }
}
