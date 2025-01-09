﻿using Domain.DTOs.Processed_YoutubeDataDTOs;
using Domain.DTOs.TeamnameDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IScoreboardGamesTeamNameServices
{
    public interface IScoreboardGamesTeamNameService
    {
        Task PopulateTeamNamesAndAbbreviations();

        Task TESTLogTeamNameAbbreviations();
        List<TeamnameDTO> GetTeamNamesAndAbbreviations();

        HashSet<string> GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO(List<Processed_YoutubeDataDTO> processed_YoutubeDataDTOs);

        void TESTCheckAllProcessedEuAndNaAgainstKnownAbbreviations(HashSet<string> distinctTeamNames, List<TeamnameDTO> teamNameDTOs);
    }
}
