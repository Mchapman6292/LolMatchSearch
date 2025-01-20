using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LolMatchFilterNew.Tests.TestServiceFactories;
using LolMatchFilterNew.Tests.TestLoggers;

namespace LolMatchFilterNew.Tests.ApplicationTests.YoutubeTests.YoutubeTitleExtractorTests
{
    public class YoutubeTitleExtractorTest : IClassFixture<TestServiceFactory>
    {
        private readonly TestLogger _testLogger;
        public static IEnumerable<object[]> YoutubeTitleTestData => new List<object[]>
        {
            new object[]
            {
                  // Pattern matches: "TEAM1 vs TEAM2 Highlights ALL GAMES | TOURNAMENT YEAR STAGE | FULLTEAM1 vs FULLTEAM2"
                 // Example: "T1 vs BLG Highlights ALL GAMES | Worlds 2024 GRAND FINAL | T1 vs Bilibili Gaming"
                "^[\\w\\s-]+\\s+vs\\s+[\\w\\s-]+\\s+Highlights\\s+ALL\\s+GAMES\\s+\\|\\s+[\\w\\s-]+\\s+\\d{4}\\s+[\\w\\s-]+\\s+\\|\\s+[\\w\\s-]+\\s+vs\\s+[\\w\\s-]+$"
            }

        };

    }
}
