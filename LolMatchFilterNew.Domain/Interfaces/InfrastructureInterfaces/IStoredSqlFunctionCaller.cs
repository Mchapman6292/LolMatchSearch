﻿using Domain.DTOs.Western_MatchDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

namespace Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers
{
    public interface IStoredSqlFunctionCaller
    {
        Task<List<WesternMatchDTO>> GetWesternMatchesAsync();

        Task<List<Import_YoutubeDataEntity>> GetYoutubeDataEntitiesForWesternTeamsAsync();

        Task<List<Import_TeamnameEntity>> GetAllWesternTeamsAsync();

    }
}
