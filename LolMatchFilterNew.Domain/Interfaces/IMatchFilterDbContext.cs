﻿using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRedirectEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamsTableEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_ProPlayerEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities;

namespace LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext
{
    public interface IMatchFilterDbContext
    {
        DbSet<Import_YoutubeDataEntity> Import_YoutubeData { get; set; }
        DbSet<Processed_ProPlayerEntity> Processed_ProPlayer { get; set; }
        DbSet<Import_ScoreboardGamesEntity> Import_ScoreboardGames { get; set; }
        DbSet<Processed_LeagueTeamEntity> Processed_LeagueTeam { get; set; }

        DbSet<Import_TeamRenameEntity> Import_TeamRename { get; set; }

        DbSet<Import_TeamRedirectEntity> Import_TeamRedirect { get; set; }

        DbSet<Processed_TeamNameHistoryEntity> Processed_TeamNameHistory { get; set; }
        DbSet<Processed_YoutubeDataEntity> YoutubeMatchExtracts { get; set; }
        DbSet<Import_TeamsTableEntity> Import_TeamsTable { get; set; }

        ChangeTracker ChangeTracker { get; }

        // Returns an int to indicate the number of entities changed. 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
