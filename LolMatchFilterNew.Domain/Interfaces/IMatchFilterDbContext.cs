using Google.Apis.Auth.OAuth2;
using LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Processed_LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.Import_TeamsTableEntities;
using LolMatchFilterNew.Domain.Entities.Processed_ProPlayerEntities;
using LolMatchFilterNew.Domain.Entities.Processed_TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.Processed_TeamRenameEntities;
using LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext
{
    public interface IMatchFilterDbContext
    {
        DbSet<Import_ScoreboardGamesEntity> LeaguepediaMatchDetails { get; set; }
        DbSet<Processed_LeagueTeamEntity> Teams { get; set; }
        DbSet<Processed_ProPlayerEntity> ProPlayers { get; set; }
        DbSet<Import_YoutubeDataEntity> YoutubeVideoResults { get; set; }

        DbSet<Import_TeamRenameEntity> TeamRenames { get; set; }
        DbSet<Processed_TeamNameHistoryEntity> TeamNameHistory { get; set; }
        DbSet<Import_TeamsTableEntity> LOLTeams { get; set; }

        ChangeTracker ChangeTracker { get; }

        // Returns an int to indicate the number of entities changed. 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
