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


namespace LolMatchFilterNew.Application.Tests.TeamRenameTests
{
    public class TeamRenameTests : ITeamRenameTest
    {
        private readonly IAppLogger _logger;
        private readonly List<TeamRenameEntity> _testRenameData;
        private readonly List<TeamNameHistoryEntity> _expectedResults;

        public TeamRenameTests()
        {
            _testRenameData = new List<TeamRenameEntity>
       {
           new TeamRenameEntity { NewName = "MAD Lions KOI", OriginalName = "MAD Lions" },
           new TeamRenameEntity { NewName = "MAD Lions", OriginalName = "Splyce" },
           new TeamRenameEntity { NewName = "Splyce", OriginalName = "Follow eSports" },
           new TeamRenameEntity { NewName = "G2 Esports", OriginalName = "Gamers2" },
           new TeamRenameEntity { NewName = "Gamers2", OriginalName = "Team Nevo" },
           new TeamRenameEntity { NewName = "Fnatic", OriginalName = "myRevenge" },
           new TeamRenameEntity { NewName = "SK Gaming", OriginalName = "Dimegio Club" },
           new TeamRenameEntity { NewName = "Karmine Corp", OriginalName = "Kameto Corp" }
       };

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
        public void SingleTeamHistory_ShouldMatchExpected()
        {
            var teamName = "MAD Lions KOI";
            var expected = "Follow eSports, MAD Lions, Splyce";
            var history = BuildTeamHistory(teamName, _testRenameData);
            Assert.Equal(expected, history);
        }

        [Theory]
        [InlineData("MAD Lions KOI", "Follow eSports, MAD Lions, Splyce")]
        [InlineData("G2 Esports", "Gamers2, Team Nevo")]
        [InlineData("Fnatic", "myRevenge")]
        public void TeamHistory_MultipleTeams_ShouldMatchExpected(string teamName, string expectedHistory)
        {
            var history = BuildTeamHistory(teamName, _testRenameData);
            Assert.Equal(expectedHistory, history);
        }

        [Fact]
        public void TeamWithNoHistory_ShouldReturnNull()
        {
            var teamName = "New Team";
            var history = BuildTeamHistory(teamName, _testRenameData);
            Assert.Null(history);
        }

        private string BuildTeamHistory(string currentName, List<TeamRenameEntity> renameData)
        {
            var historyList = new List<string>();
            string nameToSearch = currentName;

            while (true)
            {
                var foundOriginalName = renameData
                    .Where(rename => rename.NewName.Equals(nameToSearch, StringComparison.OrdinalIgnoreCase))
                    .Select(rename => rename.OriginalName)
                    .ToList();

                if (foundOriginalName.Any())
                {
                    historyList.Add(foundOriginalName.First());
                    nameToSearch = foundOriginalName.First();
                }
                else
                    break;
            }

            return historyList.Any() ? string.Join(", ", historyList) : null;
        }
    }
}


