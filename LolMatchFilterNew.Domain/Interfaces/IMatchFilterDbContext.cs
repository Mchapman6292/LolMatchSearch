using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRedirectEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamsTableEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_ProPlayerEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames;
using Domain.DTOs.Western_MatchDTOs;
using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.YoutubeDataWithTeamsDTOs;

namespace LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext
{
    public interface IMatchFilterDbContext
    {
        DbSet<Import_YoutubeDataEntity> Import_YoutubeData { get; set; }
        DbSet<Import_TeamsTableEntity> Import_TeamsTable { get; set; }
        DbSet<Import_ScoreboardGamesEntity> Import_ScoreboardGames { get; set; }
        DbSet<Import_TeamRenameEntity> Import_TeamRename { get; set; }
        DbSet<Import_TeamRedirectEntity> Import_TeamRedirect { get; set; }
        DbSet<Import_TeamnameEntity> Import_Teamname { get; set; }


        DbSet<Processed_ProPlayerEntity> Processed_ProPlayer { get; set; }
        DbSet<Processed_LeagueTeamEntity> Processed_LeagueTeam { get; set; }
        DbSet<Processed_YoutubeDataEntity> YoutubeMatchExtracts { get; set; }


        DbSet<WesternMatchDTO> WesternMatchesSet { get; set; }
        DbSet<TeamNameDTO> TeamnamesSet { get; set; }

        
        DbSet<YoutubeDataWithTeamsDTO> YoutubeSet { get; set; }

        ChangeTracker ChangeTracker { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


    }
}
