namespace LolMatchFilterNew.Domain.DTOs.MatchComparisonResultDTOs
{
    public class MatchComparisonResultDTO
    {
   

        public string YoutubeVideoId { get; set; }

        public string YoutubeTitle { get; set; }
        public string GameID { get; set; }
        public DateTime? SGMatchDate { get; set; }
        public DateTime? YoutubePublishDate { get; set; }
        public string Team1TeamID { get; set; }
        public string Team1LongName { get; set; }   

        public string Team2TeamID { get; set; }
        public string Team2LongName { get; set; }



        // Result properties
        public bool? DoesMatch { get; set; }


        public List<string> Errors { get; set; } = new();
        public List<string> Warnings { get; set; } = new();
        public List<string> InfoMessages { get; set; } = new();
    }
}
