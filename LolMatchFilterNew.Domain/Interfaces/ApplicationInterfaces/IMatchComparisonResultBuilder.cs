using LolMatchFilterNew.Domain.DTOs.MatchComparisonResultDTOs;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchComparisonResultBuilders
{
    public interface IMatchComparisonResultBuilder
    {
        IMatchComparisonResultBuilder WithMatchIdentification(string gameId);
        IMatchComparisonResultBuilder WithMatchDate(DateTime? matchDate);
        IMatchComparisonResultBuilder WithLeaguepediaTeams(string team1, string team2);
        IMatchComparisonResultBuilder WithYoutubeInfo(string videoId, string title, DateTime? publishDate);
        IMatchComparisonResultBuilder WithYoutubeTeams(string youtubeVideoId, string extractedTeam1, string extractedTeam2);
        IMatchComparisonResultBuilder WithExtractedTeamInfo(string extractedTeamInfo);
        MatchComparisonResultDTO Build();
    }
}
