using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ILolMatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.DbContextFactory;
using Microsoft.EntityFrameworkCore;

namespace LolMatchFilterNew.Infrastructure.Repositories.LeaguepediaMatchDetailRepository
{
    public class LeaguepediaMatchDetailRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly ILolMatchFilterDbContext _MatchFilterDbContext;

        public LeaguepediaMatchDetailRepository(IAppLogger appLogger, ILolMatchFilterDbContext MatchFilterDbContext)
        {
            _appLogger = appLogger;
            _MatchFilterDbContext = MatchFilterDbContext;
        }
    }
}
