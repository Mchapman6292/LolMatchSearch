using LolMatchFilterNew.Domain.Entities.Processed_TeamNameHistoryEntities;
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
            var testData = new List<Processed_TeamNameHistoryEntity>
        {
            new Processed_TeamNameHistoryEntity { CurrentTeamName = "MAD Lions KOI", NameHistory = "Follow eSports, MAD Lions, Splyce" },
            new Processed_TeamNameHistoryEntity { CurrentTeamName = "G2 Esports", NameHistory = "Gamers2, Team Nevo" },
            new Processed_TeamNameHistoryEntity { CurrentTeamName = "Fnatic", NameHistory = "myRevenge" },
            new Processed_TeamNameHistoryEntity { CurrentTeamName = "SK Gaming", NameHistory = "Dimegio Club" },
            new Processed_TeamNameHistoryEntity { CurrentTeamName = "Karmine Corp", NameHistory = "Kameto Corp" },
            new Processed_TeamNameHistoryEntity { CurrentTeamName = "Rogue", NameHistory = "" }
        };

            foreach (var item in testData)
            {
                yield return new object[] { item };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
