using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
namespace LolMatchFilterNew.Infrastructure.ExcelServices
{
    public class ExcelService
    {
        private readonly IAppLogger _appLogger;


        public ExcelService(IAppLogger appLogger)
        {
            _appLogger = appLogger;
        }



    }
}
