using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.YoutubeDataWithTeamsDTOs;
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
        Task<List<YoutubeDataWithTeamsDTO>> TESTGetAllProcessedForEuAndNaTeams();

        Task TESTCheckExtractedTeamsAsync(List<Import_YoutubeDataEntity> import_YoutubeDataEntities, HashSet<string> distinctTeamNames);

        void CONTROLLERValidateWesternMatches(HashSet<string> distinctTeamNames, List<TeamNameDTO> import_TeamNameAllNames);

        void TESTCreateTenYoutubeTitleOccurence(List<Import_YoutubeDataEntity> youtubeEntities);
        

        }
}
