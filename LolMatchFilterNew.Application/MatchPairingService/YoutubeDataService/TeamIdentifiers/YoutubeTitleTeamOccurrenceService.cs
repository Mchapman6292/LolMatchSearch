using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IImport_TeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders;
using Domain.Interfaces.ApplicationInterfaces.YoutubeDataService.TeamIdentifiers.IYoutubeTitleTeamOccurrenceServices;
using LolMatchFilterNew.Domain.DTOs.YoutubeTitleTeamOccurrenceDTOs;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using System.Text.RegularExpressions;
using Domain.Enums.TeamNameTypes;
using LolMatchFilterNew.Infrastructure.Logging.AppLoggers;


namespace Application.MatchPairingService.YoutubeDataService.TeamIdentifiers.YoutubeTitleTeamOccurenceServices
{
    public class YoutubeTitleTeamOccurrenceService : IYoutubeTitleTeamOccurenceService
    {
        private static readonly Regex G2_Game2_FalsePositive = new Regex(@"[Gg]2$", RegexOptions.Compiled);
        private readonly IAppLogger _appLogger;
        private readonly IYoutubeTitleTeamNameFinder _youtubeTitleTeamNameFinder;
        private readonly IImport_TeamNameService _importTeamNameService;



                // EXCLUSION LOGIC MUST CHANGE


        public YoutubeTitleTeamOccurenceDTO TallyTeamNameOccurrences(YoutubeTitleTeamOccurenceDTO occurrenceDTO)
        {
            string normalizedTitle = occurrenceDTO.YoutubeTitle.ToLower();

            // Check if title ends with G2
            bool endsWithG2 = G2_Game2_FalsePositive.IsMatch(normalizedTitle);

            if(endsWithG2)
            {
                _appLogger.Warning($"Title flagged for G2(Game2) false positive match for {occurrenceDTO.YoutubeTitle}");
            }


            // If it does, create a temporary version without G2 for matching
            string titleForMatching = endsWithG2
                ? normalizedTitle.Substring(0, normalizedTitle.Length - 2).Trim()
                : normalizedTitle;

            _appLogger.Info($"TallyTeamNameOccurrences started with DTO instance: {occurrenceDTO.InstanceId}");


            foreach (var teamNameDto in _importTeamNameService.ReturnImport_TeamNameAllNames())
            {
                var matchesForTeam = new List<(TeamNameType, string)>();

                if (teamNameDto.ShortName != null && _youtubeTitleTeamNameFinder.CheckNameMatch(teamNameDto.ShortName, normalizedTitle))
                {
                    IncrementCount(occurrenceDTO, "Short", 1);
                    matchesForTeam.Add((TeamNameType.Short,teamNameDto.ShortName));
                    _appLogger.Info($"Added ShortName {teamNameDto.ShortName}");
                }

                if (teamNameDto.MediumName != null && _youtubeTitleTeamNameFinder.CheckNameMatch(teamNameDto.MediumName, normalizedTitle))
                {
                    IncrementCount(occurrenceDTO, "Medium", 1);
                    matchesForTeam.Add((TeamNameType.Medium,teamNameDto.MediumName));
                    _appLogger.Info($"Added Medium: {teamNameDto.MediumName}");
                }

                if (teamNameDto.LongName != null && _youtubeTitleTeamNameFinder.CheckNameMatch(teamNameDto.LongName, normalizedTitle))
                {
                    IncrementCount(occurrenceDTO, "Long", 1);
                    matchesForTeam.Add((TeamNameType.Long,teamNameDto.LongName));
                    _appLogger.Info($"Added LongName {teamNameDto.LongName}");
                }


                if (teamNameDto.FormattedInputs != null)
                {
                    foreach (var input in teamNameDto.FormattedInputs)
                    {
                        if (_youtubeTitleTeamNameFinder.CheckNameMatch(input, normalizedTitle))
                        {
                            IncrementCount(occurrenceDTO, "Inputs", 1);
                            matchesForTeam.Add((TeamNameType.Input,input));
                        }
                    }
                }

                if (matchesForTeam.Any())
                {
                    UpdateMatchingTeamNameIds(occurrenceDTO, teamNameDto.TeamNameId, matchesForTeam);
                }
            }
            _appLogger.Info($"TallyTeamNameOccurrences ending with DTO instance: {occurrenceDTO.InstanceId}, Dictionary count: {occurrenceDTO.AllMatchingTeamNameIds.Count}");
            return occurrenceDTO;

      
        }



        public YoutubeTitleTeamOccurrenceService(IAppLogger appLogger, IYoutubeTitleTeamNameFinder youtubeTitleTeamNameFinder, IImport_TeamNameService importTeamNameService)
        {
            _appLogger = appLogger;
            _youtubeTitleTeamNameFinder = youtubeTitleTeamNameFinder;
            _importTeamNameService = importTeamNameService;
        }



        public void UpdateYoutubeTitle(YoutubeTitleTeamOccurenceDTO matchResultDTO, string title)
        {
            matchResultDTO.YoutubeTitle = title;
        }

        // 

        public void UpdateMatchingTeamNameIds(YoutubeTitleTeamOccurenceDTO occurrenceDTO, string teamNameId, List<(TeamNameType, string)> matches)
        {
            _appLogger.Info($"UpdateMatchingTeamNameIds called with DTO instance: {occurrenceDTO.InstanceId}");



            occurrenceDTO.AllMatchingTeamNameIds.Add(teamNameId, matches);
        }


        public void IncrementCount(YoutubeTitleTeamOccurenceDTO OccurrenceDTO, string countType, int increment)
        {
            if (countType != "Short" && countType != "Medium" && countType != "Long" && countType != "Inputs")
            {
                throw new ArgumentException($"Invalid count type: {countType}. Allowed values are: Short, Medium, Long, Inputs.");
            }

            switch (countType)
            {
                case "Short":
                    OccurrenceDTO.ShortNameCount += increment;
                    break;
                case "Medium":
                    OccurrenceDTO.MediumNameCount += increment;
                    break;
                case "Long":
                    OccurrenceDTO.LongNameCount += increment;
                    break;
                case "Inputs":
                    OccurrenceDTO.MatchingInputsCount += increment;
                    break;
            }
        }



        public Dictionary<string, List<(TeamNameType,string)>> GetTeamIdsWithHighestOccurences(YoutubeTitleTeamOccurenceDTO matchDTO)
        {
            // Dict to hold counts of all potential matches. 
            Dictionary<string, int> teamIdsWithCountsOfMatches = new Dictionary<string, int>();

            // Create result dictionary to store matches with top counts
            Dictionary<string, List<(TeamNameType, string)>> result = new Dictionary<string, List<(TeamNameType, string)>>();

            int numberOfTeamsAdded = 0;


            foreach (var teamidMatch in matchDTO.AllMatchingTeamNameIds)
            {
                string teamId = teamidMatch.Key;
                int idCount = teamidMatch.Value.Count;
                teamIdsWithCountsOfMatches.Add(teamId, idCount);
            }

            // Retrieve top two counts.
            List<KeyValuePair<string, int>> topTwoCounts = teamIdsWithCountsOfMatches
                .OrderByDescending(x => x.Value)
                .Take(2)
                .ToList();


            // Get the actual count values from top two entries
            int firstCount = topTwoCounts[0].Value;
            int secondCount = topTwoCounts.Count > 1 ? topTwoCounts[1].Value : firstCount;


            // Add all teams that match either of the top two counts
            foreach (var teamMatch in matchDTO.AllMatchingTeamNameIds)
            {
                int currentCount = teamMatch.Value.Count;
                if (currentCount == firstCount || currentCount == secondCount)
                {
                    result.Add(teamMatch.Key, teamMatch.Value);
                    numberOfTeamsAdded++;
                }
            }
            _appLogger.Info($"Number of teams found with matching name counts: {numberOfTeamsAdded}.");
            return result;
        }

        public void PopulateTeamIdsWithMostMatches(YoutubeTitleTeamOccurenceDTO occurrenceDTO, Dictionary<string, List<(TeamNameType,string)>> teamIdWithMatches)
        {
            if(occurrenceDTO.AllMatchingTeamNameIds.Keys.Count <= 2)
            {
                occurrenceDTO.TeamIdsWithMostMatches = occurrenceDTO.AllMatchingTeamNameIds;
            }

            var result =  occurrenceDTO.AllMatchingTeamNameIds
                    .Where(kvp => kvp.Value.Any(tuple => tuple.Item1 == TeamNameType.Long))
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            occurrenceDTO.TeamIdsWithMostMatches = result;
        }



        public void TESTPopulateTeamIdsWithMostMatches(YoutubeTitleTeamOccurenceDTO occurrenceDTO,
    Dictionary<string, List<(TeamNameType, string)>> teamIdWithMatches)
        {
            occurrenceDTO.TeamIdsWithMostMatches = teamIdWithMatches;
        }







    }
}