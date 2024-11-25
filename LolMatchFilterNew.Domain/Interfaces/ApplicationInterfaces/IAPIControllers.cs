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


        Task FetchAndAddTeamNamesForLeague(string leagueName);

        Task FetchAllDataForTeamRenames();


        Task ControllerAddTeamNameHistoryToDatabase();

        Task ControllerAddTeamsTableToDatabase();

        Task ControllerMapAllCurrentTeamNamesToPreviousTeamNamesAsync();

        Task ControllerDeleteAllYoutubeVideoResultsEntries();
    }
}
