﻿

using Domain.Enums.TeamNameTypes;

namespace LolMatchFilterNew.Domain.DTOs.YoutubeTitleTeamOccurrenceDTOs
{
    public class YoutubeTitleTeamOccurenceDTO
    {
        public string YoutubeTitle { get; set; } = string.Empty;
        public Dictionary<string, List<(TeamNameType, string)>> AllMatchingTeamNameIds { get; set; }

        public Dictionary<string, List<(TeamNameType, string)>> TeamIdsWithMostMatches { get; set; }

        public int LongNameCount { get; set; } = 0;

        public int MediumNameCount { get; set; } = 0;
        public int ShortNameCount { get; set; } = 0;
        public int MatchingInputsCount { get; set; } = 0;
        public List<string>? MatchingInputs { get; set; }

        public List<string>? Exclusions { get; set; }

        public string? MatchingTeam1LongName { get; set; }

        public string? MatchingTeam2LongName { get; set; }

    }
}







