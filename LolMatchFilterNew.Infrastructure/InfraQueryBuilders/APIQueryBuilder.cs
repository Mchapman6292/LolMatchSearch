using Domain.DTOs.TeamNameHistoryDTOs;
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
        private IQueryable<TeamNameHistoryDTO> _query;


        public APIQueryBuilder( IMatchFilterDbContext matchFilterDbContext)
        {
            _matchFilterDbContext = matchFilterDbContext;
  
        }


        public APIQueryBuilder WithTeam(string teamName) 
        {
            throw new NotImplementedException();
        }


        public IQueryable<List<string>> SelectNameHistory()
        {
            throw new NotImplementedException();
        }



        public IQueryable<TeamNameHistoryDTO> Build()
        {
            return _query;
        }










    }
}
