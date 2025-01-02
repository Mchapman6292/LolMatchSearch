using Domain.DTOs.Western_MatchDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers
{
    public interface IObjectLogger
    {
        void LogWesternMatchDTO(WesternMatchDTO westernMatchDTO);
    }
}
