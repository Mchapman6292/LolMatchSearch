﻿using Google.Apis.Auth.OAuth2;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Entities.LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.ProPlayerEntities;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.ILolMatchFilterDbContext
{
    public interface ILolMatchFilterDbContext
    {
        DbSet<LeaguepediaMatchDetailEntity> LeaguepediaMatchDetails { get; set; }
        DbSet<LeagueTeamEntity> Teams { get; set; }
        DbSet<ProPlayerEntity> ProPlayers { get; set; }
        DbSet<YoutubeVideoEntity> YoutubeVideoResults { get; set; }

        // Returns an int to indicate the number of entities changed. 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}