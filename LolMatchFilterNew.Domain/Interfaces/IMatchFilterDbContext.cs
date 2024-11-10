using Google.Apis.Auth.OAuth2;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Entities.LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.LpediaTeamEntities;
using LolMatchFilterNew.Domain.Entities.ProPlayerEntities;
using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.TeamRenamesEntities;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
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
        DbSet<LeaguepediaMatchDetailEntity> LeaguepediaMatchDetails { get; set; }
        DbSet<LeagueTeamEntity> Teams { get; set; }
        DbSet<ProPlayerEntity> ProPlayers { get; set; }
        DbSet<YoutubeVideoEntity> YoutubeVideoResults { get; set; }

        DbSet<TeamRenameEntity> TeamRenames { get; set; }
        DbSet<TeamNameHistoryEntity> TeamNameHistory { get; set; }
        DbSet<LpediaTeamEntity> LOLTeams { get; set; }

        ChangeTracker ChangeTracker { get; }

        // Returns an int to indicate the number of entities changed. 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
