using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.TeamRenamesEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LolMatchFilterNew.Application.TeamHistoryService.TeamHistoryLogics;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamHistoryLogic;
using LolMatchFilterNew.Tests.TestLoggers;
using System.Collections;


namespace LolMatchFilterNew.Tests.ApplicationTests.TeamRenameTests
{
    public class TeamRenameTest
    {
        private readonly TestLogger _testLogger;
        private readonly ITeamHistoryLogic _teamHistoryLogic;
        private readonly List<TeamNameHistoryEntity> _expectedResults;




        public TeamRenameTest(TestLogger testLogger , ITeamHistoryLogic teamHistoryLogic)
        {
            _testLogger = factory.GetTestLogger();
            _teamHistoryLogic = factory.GetTeamHistoryLogic();
        }

        public static IEnumerable<object[]> TeamData => new List<object[]>
        {
            new object[] { new TeamNameHistoryEntity { CurrentTeamName = "MAD Lions KOI", NameHistory = "Follow eSports, MAD Lions, Splyce" } },
            new object[] { new TeamNameHistoryEntity { CurrentTeamName = "G2 Esports", NameHistory = "Gamers2, Team Nevo" } },
            new object[] { new TeamNameHistoryEntity { CurrentTeamName = "Fnatic", NameHistory = "myRevenge" } },
            new object[] { new TeamNameHistoryEntity { CurrentTeamName = "SK Gaming", NameHistory = "Dimegio Club" } },
            new object[] { new TeamNameHistoryEntity { CurrentTeamName = "Karmine Corp", NameHistory = "Kameto Corp" } },
            new object[] { new TeamNameHistoryEntity { CurrentTeamName = "Rogue", NameHistory = "" } }
        };


        public static List<string> CovertTeamHistoryToList(string teamNameHistory)
        {
            List<string> teamHistory = new List<string>();
            if (teamNameHistory == string.Empty)
            {
                teamHistory.Add("");
            }
            teamHistory.AddRange(teamNameHistory.Split(','));
            return teamHistory;
        }

        [Theory]
        [MemberData(nameof(TeamData))]
        public async Task AllTeamsHistory_ShouldMatchExpected(TeamNameHistoryEntity expectedEntity)
        {
            List<string> testCurrentNames = new List<string> { expectedEntity.CurrentTeamName };

            List<TeamNameHistoryEntity> actualResults = await _teamHistoryLogic.GetAllPreviousTeamNamesForCurrentTeamName(testCurrentNames);

            Assert.Single(actualResults); 
            var actual = actualResults.First();
            Assert.NotNull(actual);

            _testLogger.Info($"Expected NameHistory: {expectedEntity.NameHistory}." +
                             $" Actual NameHistory: {actual.NameHistory}.");

            Assert.Equal(expectedEntity.NameHistory, actual.NameHistory);
        }

    }
}

