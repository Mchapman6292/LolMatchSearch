using LolMatchFilterNew.Domain.Entities.Processed_TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.Processed_TeamRenameEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ITeamNameHistoryFormatters;
using LolMatchFilterNew.Domain.Formatters.TeamNameHistoryFormatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LolMatchFilterNew.Application.TeamHistoryService.TeamHistoryLogics;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamHistoryLogic;
using LolMatchFilterNew.Tests.TestLoggers;
using LolMatchFilterNew.Tests.TestServiceFactories;
using System.Collections;


namespace LolMatchFilterNew.Tests.ApplicationTests.TeamRenameTests
{
    public class TeamRenameTest : IClassFixture<TestServiceFactory>
    {
        private readonly TestLogger _testLogger;
        private readonly ITeamHistoryLogic _teamHistoryLogic;
        private readonly ITeamNameHistoryFormatter _teamNameHistoryFormatter;


        public TeamRenameTest(TestServiceFactory factory)
        {
            _testLogger = factory.GetTestLogger();
            _teamHistoryLogic = factory.GetTeamHistoryLogic();
            _teamNameHistoryFormatter = factory.GetTeamNameHistoryFormatter();
        }
        // Enumerable<object[]> structure required for xUnit's MemberData attribute - each object[] represents a test case with parameters
        public static IEnumerable<object[]> TeamData => new List<object[]>
        {
            // Standard Format comma separated teamNames
            new object[] { new Dictionary<string, List<string>> { ["MAD Lions KOI"] = new List<string> { "Follow eSports", "MAD Lions", "Splyce", "Team Dignitas EU", "Team FurIR" } } },
            new object[] { new Dictionary<string, List<string>> { ["G2 Esports"] = new List<string> { "Gamers2", "Team Nevo" } } },
            new object[] { new Dictionary<string, List<string>> { ["SK Gaming"] = new List<string> { "Dimegio Club" } } },
            new object[] { new Dictionary<string, List<string>> { ["Karmine Corp"] = new List<string> { "Kameto Corp" } } },
    
            // No Name History 
            new object[] { new Dictionary<string, List<string>> { ["Rogue"] = new List<string>() } },

            // edge case 
            new object[] { new Dictionary<string, List<string>> { ["Fnatic"] = new List<string> { "myRevenge","Enemy" } } }
        };



        [Theory]
        [MemberData(nameof(TeamData))]
        public async Task AllTeamsHistory_ShouldMatchExpected(Dictionary<string, List<string>> expectedTeams)
        {
            try
            {
                var currentTeamName = expectedTeams.Keys.First();
                var expectedHistory = expectedTeams[currentTeamName];

                _testLogger.Info($"Starting test for team: {currentTeamName}");


                List<Processed_TeamNameHistoryEntity> actualResults =
                    await _teamHistoryLogic.GetAllPreviousTeamNamesForCurrentTeamName(
                        new List<string> { currentTeamName });

                foreach( var actualResult in actualResults ) 
                {
                    _testLogger.Info($"CurrentTeamName: {actualResult.CurrentTeamName}, TeamNameHistory: {actualResult.NameHistory}.");
                }

                // Verify we got exactly one result
                Assert.Single(actualResults);
                var actual = actualResults.First();
                Assert.NotNull(actual);

                // Convert the actual result to our standardized dictionary format for comparison
                var actualDict = _teamNameHistoryFormatter.FormatTeamHistoryToDict(actualResults);

                // Log both histories for debugging
                _testLogger.Info($"Expected NameHistory: {string.Join(", ", expectedHistory)}");
                _testLogger.Info($"Actual NameHistory: {string.Join(", ", actualDict[currentTeamName])}");

                // Compare the lists of team names, ignoring order and case
                // OrderBy ensures order-independent comparison, StringComparer.OrdinalIgnoreCase handles case sensitivity
                Assert.Equal(
                    expectedHistory.OrderBy(x => x),
                    actualDict[currentTeamName].OrderBy(x => x),
                    StringComparer.OrdinalIgnoreCase
                );
            }
            catch (Exception ex)
            {
                _testLogger.Error($"Test failed for team {expectedTeams.Keys.First()}", ex);
                throw;
            }
        }
    }
}


