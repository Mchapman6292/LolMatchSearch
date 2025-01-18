
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
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames;
using Domain.Entities.Imported_Entities.Import_TournamentEntities;

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers
{
    public interface ILeaguepediaApiMapper
    {
        Task<IEnumerable<Import_ScoreboardGamesEntity>> MapToImport_ScoreboardGames(IEnumerable<JObject> leaguepediaData);

        Task<IEnumerable<Import_TeamRedirectEntity>> MapToImport_TeamRedirects(IEnumerable<JObject> leaguepediaData);

        Task<IEnumerable<Processed_LeagueTeamEntity>> MapToProcessed_LeagueTeamEntity(IEnumerable<JObject> leaguepediaData);

        Task<IEnumerable<Processed_LeagueTeamEntity>> MapApiDataToLeagueTeamEntityForTeamShort(IEnumerable<JObject> apiData);

        Task<IEnumerable<Import_TeamRenameEntity>> MapToImport_TeamRename(IEnumerable<JObject> apiData);

        Task<IEnumerable<Import_TeamsTableEntity>> MapToImport_Teams(IEnumerable<JObject> apiData);

        Task<IEnumerable<Import_TeamnameEntity>> MapToImport_Teamname(IEnumerable<JObject> apiData);

        Task<IEnumerable<Import_TournamentEntity>> MapToImport_Tournaments(IEnumerable<JObject> tournamentData);
    }
}
