﻿using Domain.DTOs.Processed_YoutubeDataDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchServiceControllers
{
    public interface IMatchComparisonController
    {

        Task<List<Processed_YoutubeDataDTO>> TESTGetAllProcessedForEuAndNaTeams();


        Task<Processed_YoutubeDataDTO> ExtractAndBuildProcessedDTO(Import_YoutubeDataEntity youtubeData);

        Task TESTCheckAllProcessedEuAndNaAgainstKnownAbbreviations(List<Processed_YoutubeDataDTO> processedYoutubeDataList);

    }
}
