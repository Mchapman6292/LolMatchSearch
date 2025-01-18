using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IAPIControllers
{
    public interface IAPIControllers
    {

        Task DeleteAllTournaments();
        Task FetchAndAddLeaguepediaDataForLeagueName(string league);

        Task ControllerAddScoreboardGames();

        Task ControllerAddTeamRedirects();


        Task FetchAndAddTeamNamesForLeague(string leagueName);

        Task ControllerAddTeamRenames();



        Task ControllerAddTeamsTableToDatabase();

        Task ControllerAddTournamentToDatabase();


        Task ControllerAddTeamnameToDatabase();



    }
}
