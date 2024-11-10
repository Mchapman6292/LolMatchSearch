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
namespace LolMatchFilterNew.Application.Tests.TeamRenameTests
{
    public class TeamRenameTest : ITeamRenameTest
    {
        private readonly IAppLogger _appLogger;

        public TeamRenameTest(IAppLogger appLogger)
        {
            _appLogger = appLogger;
        }

        // Query that returns all Mad Lions KOI name history
        /// <summary>
        /// WITH "TeamChanges" AS (
        ///     SELECT tr."OriginalName", tr."NewName", tr."Date", t."RenamedTo", t."Region", t."Short"
        ///     FROM "TeamRenames" tr
        ///     FULL OUTER JOIN "LOLTeams" t ON tr."NewName" = t."Name"
        ///     WHERE tr."OriginalName" IN ('Follow eSports', 'Splyce', 'MAD Lions', 'MAD Lions KOI')
        ///        OR tr."NewName" IN ('Follow eSports', 'Splyce', 'MAD Lions', 'MAD Lions KOI')
        ///        OR t."Name" IN ('Follow eSports', 'Splyce', 'MAD Lions', 'MAD Lions KOI')
        ///        OR t."RenamedTo" IN ('Follow eSports', 'Splyce', 'MAD Lions', 'MAD Lions KOI')
        /// ),
        /// "CurrentTeamInfo" AS (
        ///     SELECT DISTINCT 
        ///         "NewName" as "CurrentTeamName",
        ///         "Short" as "CurrentNameShort",
        ///         "Region"
        ///     FROM "TeamChanges"
        ///     WHERE "NewName" NOT IN (
        ///         SELECT DISTINCT "OriginalName"
        ///         FROM "TeamChanges"
        ///         WHERE "OriginalName" IS NOT NULL
        ///     )
        ///     AND "NewName" IS NOT NULL
        /// )
        /// SELECT 
        ///     cti."CurrentTeamName",
        ///     cti."CurrentNameShort",
        ///     STRING_AGG(DISTINCT tc."OriginalName", ', ' ORDER BY tc."OriginalName") as "NameHistory",
        ///     cti."Region"
        /// FROM "CurrentTeamInfo" cti
        /// CROSS JOIN "TeamChanges" tc
        /// WHERE tc."OriginalName" IS NOT NULL
        /// GROUP BY cti."CurrentTeamName", cti."CurrentNameShort", cti."Region";
        /// </summary>
        /// <returns></returns>




        public List<TeamNameHistoryEntity> GetTeams()
        {
            var teams = new List<TeamNameHistoryEntity>
            {
                new TeamNameHistoryEntity
                {
                    CurrentTeamName = "MAD Lions KOI",
                    CurrentNameShort = "MDK",
                    NameHistory = "Follow eSports, MAD Lions, Splyce, Team Dignitas EU",
                    Region = "EMEA"
                },
                new TeamNameHistoryEntity
                {
                    CurrentTeamName = "G2 Esports",
                    CurrentNameShort = "G2",
                    NameHistory = "Gamers2, Team Nevo",
                    Region = "EMEA"
                },
                new TeamNameHistoryEntity
                {
                    CurrentTeamName = "Fnatic",
                    CurrentNameShort = "FNC",
                    NameHistory = "myRevenge",
                    Region = "EMEA"
                },
                new TeamNameHistoryEntity
                {
                    CurrentTeamName = "SK Gaming",
                    CurrentNameShort = "SK",
                    NameHistory = "Dimegio Club",
                    Region = "EMEA"
                },
                new TeamNameHistoryEntity
                {
                    CurrentTeamName = "Karmine Corp",
                    CurrentNameShort = "KC",
                    NameHistory = "Kameto Corp",
                    Region = "EMEA"
                }
            };

            return teams;
        }
    }
}

    

