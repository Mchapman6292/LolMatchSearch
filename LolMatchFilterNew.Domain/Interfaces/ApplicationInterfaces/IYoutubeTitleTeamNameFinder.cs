using Domain.DTOs.YoutubeTitleTeamNameOccurrenceCountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders
{
    public interface IYoutubeTitleTeamNameFinder
    {

        void UpdateDTOWithTeamMatchesFromTitle(YoutubeTitleTeamNameOccurrenceCountDTO youtubeTitleMatchDTO);

    }
}
