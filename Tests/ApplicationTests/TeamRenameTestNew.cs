using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.TeamRenamesEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamRenameTests;
using Xunit;
using LolMatchFilterNew.Application.TeamHistoryService.TeamHistoryLogics;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamHistoryLogic;
using LolMatchFilterNew.Infrastructure.Logging.AppLoggers;


namespace LolMatchFilterNew.Tests.TeamRenameTests
{
    public class TeamRenameTest : ITeamRenameTest
    {
        private readonly IAppLogger _applogger;
        private readonly ITeamHistoryLogic _teamHistoryLogic;
        private readonly List<TeamNameHistoryEntity> _expectedResults;

        public TeamRenameTest(IAppLogger appLogger, ITeamHistoryLogic teamHistoryLogic)
        {
            _applogger = appLogger;
            _teamHistoryLogic = teamHistoryLogic;
            _expectedResults = new List<TeamNameHistoryEntity>
       {
           new TeamNameHistoryEntity { CurrentTeamName = "MAD Lions KOI", NameHistory = "Follow eSports, MAD Lions, Splyce" },
           new TeamNameHistoryEntity { CurrentTeamName = "G2 Esports", NameHistory = "Gamers2, Team Nevo" },
           new TeamNameHistoryEntity { CurrentTeamName = "Fnatic", NameHistory = "myRevenge" },
           new TeamNameHistoryEntity { CurrentTeamName = "SK Gaming", NameHistory = "Dimegio Club" },
           new TeamNameHistoryEntity { CurrentTeamName = "Karmine Corp", NameHistory = "Kameto Corp" }
       };
        }

        [Fact]
        public async Task AllTeamsHistory_ShouldMatchExpected()
        {
            List<string> testCurrentNames = _expectedResults.Select(x => x.CurrentTeamName).ToList();
            List<TeamNameHistoryEntity> actualResults = await _teamHistoryLogic.GetAllPreviousTeamNamesForCurrentTeamName(testCurrentNames);

            Assert.Equal(_expectedResults.Count, actualResults.Count);
            foreach (var expected in _expectedResults)
            {
                var actual = actualResults.FirstOrDefault(x => x.CurrentTeamName == expected.CurrentTeamName);
                Assert.NotNull(actual);
                Assert.Equal(expected.NameHistory, actual.NameHistory);
            }
        }

        [Theory]
        [InlineData("MAD Lions KOI", "Follow eSports, MAD Lions, Splyce")]
        [InlineData("G2 Esports", "Gamers2, Team Nevo")]
        [InlineData("Fnatic", "myRevenge")]
        [InlineData("SK Gaming", "Dimegio Club")]
        [InlineData("Karmine Corp", "Kameto Corp")]
        public async Task SingleTeamHistory_ShouldMatchExpected(string teamName, string expectedHistory)
        {
            List<string> singleTeamName = new List<string> { teamName };
            List<TeamNameHistoryEntity> result = await _teamHistoryLogic.GetAllPreviousTeamNamesForCurrentTeamName(singleTeamName);

            Assert.Single(result);
            Assert.Equal(teamName, result[0].CurrentTeamName);
            Assert.Equal(expectedHistory, result[0].NameHistory);
        }
    }
}

