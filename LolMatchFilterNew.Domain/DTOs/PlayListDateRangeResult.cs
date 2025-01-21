using LolMatchFilterNew.Domain.DTOs.YoutubeVideoDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

namespace Domain.DTOs.PlayListDateRangeDTOs
{
    public class PlayListDateRangeResult
    {
        public string PlaylistName {  get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }

        public List<Import_YoutubeDataEntity> VideosWithinRange { get; set; }

        public List<Import_ScoreboardGamesEntity> GamesWithinRange = new List<Import_ScoreboardGamesEntity>();

        private const int DateRangeLeeway = 14;



        public void AddGamesWithinDateRange(List<Import_ScoreboardGamesEntity> scoreboardGames)
        {
            DateTime rangeStartDate = StartDate.AddDays(-DateRangeLeeway);
            DateTime rangeEndDate = EndDate.AddDays(DateRangeLeeway);

            var gamesBeforeStartDate = scoreboardGames.Where(game => game.DateTime_utc < rangeStartDate).ToList();
            var gamesAfterEndDate = scoreboardGames.Where(game => game.DateTime_utc > rangeEndDate).ToList();

            if (gamesBeforeStartDate.Any() || gamesAfterEndDate.Any())
            {
                throw new ArgumentException($"Games found outside the allowed date range for playlist {PlaylistName}. " +
                    $"Before start date: {gamesBeforeStartDate.Count}, After end date: {gamesAfterEndDate.Count}");
            }

            GamesWithinRange = scoreboardGames.OrderBy(game => game.DateTime_utc).ToList();
        }





    }
}
