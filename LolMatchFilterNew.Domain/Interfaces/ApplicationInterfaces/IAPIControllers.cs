using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IAPIControllers
{
    public interface IAPIControllers
    {

        Task DeleteAllTeamRedirects();
        Task FetchAndAddLeaguepediaDataForLeagueName(string league);

        Task ControllerAddScoreboardGames();

        Task ControllerAddTeamRedirects();


        Task FetchAndAddTeamNamesForLeague(string leagueName);

        Task FetchAllDataForTeamRenames();


        Task ControllerAddTeamNameHistoryToDatabase();

        Task ControllerAddTeamsTableToDatabase();

        Task ControllerMapAllCurrentTeamNamesToPreviousTeamNamesAsync();

        Task ControllerDeleteAllYoutubeVideoResultsEntries();
    }
}
