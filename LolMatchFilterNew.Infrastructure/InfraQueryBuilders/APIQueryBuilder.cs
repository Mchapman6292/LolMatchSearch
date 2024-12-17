using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.QueryBuilders.APIQueryBuilders
{
 
    public interface IAPIQueryBuilder
    {
        APIQueryBuilder WithTeam(string teamName);
    }
    



    public class APIQueryBuilder :IAPIQueryBuilder
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;
        private IQueryable<Processed_TeamNameHistoryEntity> _query;


        public APIQueryBuilder( IMatchFilterDbContext matchFilterDbContext)
        {
            _matchFilterDbContext = matchFilterDbContext;
  
        }


        public APIQueryBuilder WithTeam(string teamName) 
        {
            if(!string.IsNullOrEmpty(teamName))
            {
                _query = _query.Where(t =>
                                t.CurrentTeamName == teamName ||
                                t.NameHistory.Contains(teamName));
                                
            }
            return this;
        }


        public IQueryable<List<string>> SelectNameHistory()
        {
            return _query.Select(t => t.NameHistory);
        }



        public IQueryable<Processed_TeamNameHistoryEntity> Build()
        {
            return _query;
        }










    }
}
