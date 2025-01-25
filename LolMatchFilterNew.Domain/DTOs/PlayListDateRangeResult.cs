using Domain.DTOs.Western_MatchDTOs;
using LolMatchFilterNew.Domain.DTOs.YoutubeVideoDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;

namespace Domain.DTOs.PlayListDateRangeDTOs
{
    public class PlayListDateRangeResult
    {
        private readonly IAppLogger _appLogger;

        public string PlaylistName {  get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }

        public List<Import_YoutubeDataEntity> VideosWithinRange { get; set; }

        public List<WesternMatchDTO> GamesWithinRange = new List<WesternMatchDTO>();

        private const int DateRangeLeeway = 14;


        public PlayListDateRangeResult(IAppLogger appLogger)
        {
            _appLogger = appLogger;
        }

        
        public void AddGamesWithinDateRange(List<WesternMatchDTO> scoreboardGames)
        {
            _appLogger.Info("Initial startDate:{StartDate},endDate: {EndDate}.", StartDate, EndDate);


            DateTime rangeStartDate = StartDate.AddDays(-DateRangeLeeway);
            DateTime rangeEndDate = EndDate.AddDays(DateRangeLeeway);

            _appLogger.Info("Dates after leeway startDate: {startDate}, endDate: {EndDate}.", rangeStartDate, rangeEndDate);

            var gamesBeforeStartDate = scoreboardGames.Where(game => game.DateTime_Utc < rangeStartDate).ToList();
            var gamesAfterEndDate = scoreboardGames.Where(game => game.DateTime_Utc > rangeEndDate).ToList();

            if (gamesBeforeStartDate.Any() || gamesAfterEndDate.Any())
            {
                throw new ArgumentException($"Games found outside the allowed date range for playlist {PlaylistName}. " +
                    $"Before start date: {gamesBeforeStartDate.Count}, After end date: {gamesAfterEndDate.Count}");
            }

            GamesWithinRange = scoreboardGames.OrderBy(game => game.DateTime_Utc).ToList();


        }





    }
}
