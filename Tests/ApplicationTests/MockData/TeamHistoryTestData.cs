using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Tests.ApplicationTests.MockData.TeamHistoryTestData
{
    public class TeamHistoryTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var testData = new List<TeamNameHistoryEntity>
        {
            new TeamNameHistoryEntity { CurrentTeamName = "MAD Lions KOI", NameHistory = "Follow eSports, MAD Lions, Splyce" },
            new TeamNameHistoryEntity { CurrentTeamName = "G2 Esports", NameHistory = "Gamers2, Team Nevo" },
            new TeamNameHistoryEntity { CurrentTeamName = "Fnatic", NameHistory = "myRevenge" },
            new TeamNameHistoryEntity { CurrentTeamName = "SK Gaming", NameHistory = "Dimegio Club" },
            new TeamNameHistoryEntity { CurrentTeamName = "Karmine Corp", NameHistory = "Kameto Corp" },
            new TeamNameHistoryEntity { CurrentTeamName = "Rogue", NameHistory = "" }
        };

            foreach (var item in testData)
            {
                yield return new object[] { item };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
