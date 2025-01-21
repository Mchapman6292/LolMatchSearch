using Domain.DTOs.Western_MatchDTOs;

namespace Domain.Interfaces.ApplicationInterfaces.IDTOBuilders.IWesternMatchDTOFactories
{
    public interface IWesternMatchDTOFactory
    {
        WesternMatchDTO CreateWesternMatchDTO(
            string gameId,
            string matchId,
            DateTime? dateTimeUtc,
            string? tournament,
            string? team1,
            string? team1TeamId,
            string? team1Players,
            string? team1Picks,
            string? team2,
            string? team2TeamId,
            string? team2Players,
            string? team2Picks,
            string? winTeam,
            string? lossTeam,
            string? team1Region,
            string? team1Longname,
            string? team1Medium,
            string? team1Short,
            List<string>? team1Inputs,
            string? team2Region,
            string? team2Longname,
            string? team2Medium,
            string? team2Short,
            List<string>? team2Inputs);
    }
}
