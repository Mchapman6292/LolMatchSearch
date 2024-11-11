﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamHistoryLogic
{
    public interface ITeamHistoryLogic
    {
        Task<Dictionary<string, List<string>>> LinkAllCurrentTeamNamesToPreviousTeamNamesAsync();
    }
}
