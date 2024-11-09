using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IAPIControllers
{
    public interface IAPIControllers
    {
        Task FetchAndAddLeaguepediaDataForLeagueName(string league);

        Task FetchAndAddYoutubeVideos(List<string> playlistIds);

        Task FetchAndAddTeamNamesForLeague(string leagueName);

        Task FetchAllDataForTeamRenames();

        Task ControllerGetAllCurrentTeamNames();

        Task ControllerAddTeamNameHistoryToDatabase();

        Task ControllerAddLpediaTeamsToDatabase();
    }
}
