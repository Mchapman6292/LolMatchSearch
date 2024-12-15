using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;

namespace API.Controllers.Controller_APITestClasses
{
    public class Controller_APITestClass : ControllerBase
    {
        private readonly IAppLogger _appLogger;

    }
}
