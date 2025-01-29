using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Helpers.ApiHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRedirectEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamsTableEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames;
using Domain.DTOs.TeamnameDTOs;
using Domain.Entities.Imported_Entities.Import_TournamentEntities;
using Domain.Entities.Imported_Entities.Import_LeagueEntities;
namespace LolMatchFilterNew.Infrastructure.DataConversion.LeaguepediaApiMappers
{
    public class LeaguepediaApiMapper : ILeaguepediaApiMapper
    {
        private readonly IAppLogger _appLogger;
        private readonly IApiHelper _apiHelper;


        public LeaguepediaApiMapper(IAppLogger appLogger, IApiHelper apiHelper)
        {
            _appLogger = appLogger;
            _apiHelper = apiHelper;
        }


        public async Task<IEnumerable<Import_ScoreboardGamesEntity>> MapToImport_ScoreboardGames(IEnumerable<JObject> leaguepediaData)
        {
            if (leaguepediaData == null || !leaguepediaData.Any())
            {
                _appLogger.Error($"Input data cannot be null or empty for {nameof(MapToImport_ScoreboardGames)}.");

                foreach(var entry in leaguepediaData)
                {
                    Console.Write($" LeaguepediaData: {entry.ToString(Formatting.Indented)}");
                }

                throw new ArgumentNullException(nameof(leaguepediaData), "Input data cannot be null or empty.");
            }
            return await Task.Run(() =>
            {
                var results = new List<Import_ScoreboardGamesEntity>();
                int processedCount = 0;
                foreach (var matchData in leaguepediaData)
                {
                    processedCount++;
                    try
                    {

                        var entity = new Import_ScoreboardGamesEntity
                        {
                            GameId = _apiHelper.GetStringValue(matchData, "GameId"),
                            GameName = _apiHelper.GetStringValue(matchData, "Gamename"),        // Lower case in original data https://lol.fandom.com/wiki/Special:CargoTables/ScoreboardGames
                            MatchId = _apiHelper.GetNullableStringValue(matchData, "MatchId"),
                            DateTime_utc = _apiHelper.ParseNullableDateTime(matchData, "DateTime UTC"),
                            Tournament = _apiHelper.GetNullableStringValue(matchData, "Tournament"),
                            Team1 = _apiHelper.GetNullableStringValue(matchData, "Team1"),
                            Team2 = _apiHelper.GetNullableStringValue(matchData, "Team2"),
                            Team1Players = string.Join(',', _apiHelper.GetValuesAsList(matchData, "Team1Players")),
                            Team2Players = string.Join(',', _apiHelper.GetValuesAsList(matchData, "Team2Players")),
                            Team1Picks = string.Join(',', _apiHelper.GetValuesAsList(matchData, "Team1Picks")),
                            Team2Picks = string.Join(',', _apiHelper.GetValuesAsList(matchData, "Team2Picks")),
                            WinTeam = _apiHelper.GetNullableStringValue(matchData, "WinTeam"),
                            LossTeam = _apiHelper.GetNullableStringValue(matchData, "LossTeam"),
                            Team1Kills = _apiHelper.GetNullableInt32Value(matchData, "Team1Kills"),
                            Team2Kills = _apiHelper.GetNullableInt32Value(matchData, "Team2Kills")

                        };

                        results.Add(entity);
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Error processing match data {processedCount}: {ex.Message}");
                        _appLogger.Error($"Raw data: {matchData}");
                    }
                }

                _appLogger.Info($"Deserialized {results.Count} entities out of {processedCount} processed.");
                return results;
            });
        }




        public async Task<IEnumerable<Import_TeamRedirectEntity>> MapToImport_TeamRedirects(IEnumerable<JObject> TeamRedirectData)
        {
            if (TeamRedirectData == null || !TeamRedirectData.Any())
            {
                _appLogger.Error($"Input data cannot be null or empty for {nameof(MapToImport_TeamRedirects)}.");
                throw new ArgumentNullException(nameof(TeamRedirectData), "Input data cannot be null or empty.");
            }

            return await Task.Run(() =>
            {
                var results = new List<Import_TeamRedirectEntity>();
                int processedCount = 0;

                foreach (var teamData in TeamRedirectData)
                {
                    processedCount++;
                    try
                    {
                        var entity = new Import_TeamRedirectEntity
                        {
                            PageName = _apiHelper.GetNullableStringValue(teamData, "PageName"),
                            AllName = _apiHelper.GetNullableStringValue(teamData, "AllName"),
                            OtherName = _apiHelper.GetNullableStringValue(teamData, "OtherName"),
                            UniqueLine = _apiHelper.GetNullableStringValue(teamData, "UniqueLine")
                        };
                        results.Add(entity);
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Error processing team redirect data {processedCount}: {ex.Message}");
                        _appLogger.Error($"Raw data: {teamData}");
                    }
                }

                _appLogger.Info($"Deserialized {results.Count} team redirect entities out of {processedCount} processed.");
                return results;
            });
        }







        public async Task<IEnumerable<Processed_LeagueTeamEntity>> MapToProcessed_LeagueTeamEntity(IEnumerable<JObject> leaguepediaData)
        {
            if (leaguepediaData == null || !leaguepediaData.Any())
            {
                _appLogger.Error($"Input data cannot be null or empty for {nameof(MapToProcessed_LeagueTeamEntity)}.");
                throw new ArgumentNullException(nameof(leaguepediaData), "Input data cannot be null or empty.");
            }
            return await Task.Run(() =>
            {
                var results = new List<Processed_LeagueTeamEntity>();
                int processedCount = 0;
                int skippedCount = 0;
                foreach (var teamData in leaguepediaData)
                {
                    processedCount++;
                    try
                    {
                        string teamName = _apiHelper.GetNullableStringValue(teamData, "TeamName");

                        if (string.IsNullOrWhiteSpace(teamName))
                        {
                            skippedCount++;
                            _appLogger.Warning($"Skipping record {processedCount} due to null or empty team name. Raw data: {teamData}");
                            continue;
                        }

                        var entity = new Processed_LeagueTeamEntity
                        {
                            TeamName = teamName,
                            NameShort = _apiHelper.GetNullableStringValue(teamData, "ShortName"),
                            Region = _apiHelper.GetNullableStringValue(teamData, "Region")
                        };
                        results.Add(entity);
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Error processing team data {processedCount}: {ex.Message}");
                        _appLogger.Error($"Raw data: {teamData}");
                    }
                }
                _appLogger.Info($"Deserialized {results.Count} entities out of {processedCount} processed. Skipped {skippedCount} records with null/empty team names.");
                return results;
            });
        }




        public async Task<IEnumerable<Processed_LeagueTeamEntity>> MapApiDataToLeagueTeamEntityForTeamShort(IEnumerable<JObject> apiData)
        {
            if (apiData == null || !apiData.Any())
            {
                _appLogger.Error($"Input data cannot be null or empty for {nameof(MapApiDataToLeagueTeamEntityForTeamShort)}.");
                throw new ArgumentNullException(nameof(apiData), "Input data cannot be null or empty.");
            }

            return await Task.Run(() =>
            {
                var results = new List<Processed_LeagueTeamEntity>();
                int processedCount = 0;
                int skippedCount = 0;

                foreach (var teamData in apiData)
                {
                    processedCount++;
                    try
                    {
                        if (teamData["title"] is JObject titleData)
                        {
                            string teamName = _apiHelper.GetNullableStringValue(titleData, "Name");
                            if (string.IsNullOrEmpty(teamName))
                            {
                                skippedCount++;
                                _appLogger.Warning($"Skipping record {processedCount} due to null or empty team name. Raw data: {titleData}");
                                continue;
                            }

                            var entity = new Processed_LeagueTeamEntity
                            {
                                TeamName = teamName,
                                NameShort = _apiHelper.GetNullableStringValue(titleData, "ShortName"),
                                Region = _apiHelper.GetNullableStringValue(titleData, "Region")
                            };

                            results.Add(entity);
                        }
                        else
                        {
                            skippedCount++;
                            _appLogger.Warning($"Skipping record {processedCount} due to missing or invalid 'title' object. Raw data: {teamData}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Error processing team data {processedCount}: {ex.Message}");
                        _appLogger.Error($"Raw data: {teamData}");
                    }
                }

                _appLogger.Info($"Deserialized {results.Count} entities out of {processedCount} processed. Skipped {skippedCount} records with null/empty team names.");
                return results;
            });
        }


        public async Task<IEnumerable<Import_TeamRenameEntity>> MapToImport_TeamRename(IEnumerable<JObject> apiData)
        {
            if (apiData == null)
            {
                _appLogger.Error($"The apiData parameter cannot be null for {nameof(MapToImport_TeamRename)}.");
                throw new ArgumentNullException(nameof(apiData), "The apiData parameter cannot be null.");
            }

            return await Task.Run(() =>
            {
                var results = new List<Import_TeamRenameEntity>();
                int processedCount = 0;
                int skippedCount = 0;

                foreach (var teamData in apiData)
                {
                    try
                    {
                        if (teamData["title"] is JObject titleData)
                        {
                            string originalName = titleData["OriginalName"]?.ToString();
                            string newName = titleData["NewName"]?.ToString();
                            DateTime? date = _apiHelper.ParseNullableDateTime(titleData, "Date");

                            if (
                                string.IsNullOrWhiteSpace(originalName) ||
                                string.IsNullOrWhiteSpace(newName))
                            {
                                skippedCount++;
                                _appLogger.Warning($"Skipping record {processedCount} due to missing required fields. Raw data: {titleData}");
                                continue;
                            }


                            string? verb = !string.IsNullOrWhiteSpace(titleData["Verb"]?.ToString())
                                ? titleData["Verb"]?.ToString()
                                : null;

                            string? isSamePage = !string.IsNullOrWhiteSpace(titleData["IsSamePage"]?.ToString())
                                ? titleData["IsSamePage"]?.ToString()
                                : null;

                            string? newsId = !string.IsNullOrWhiteSpace(titleData["NewsId"]?.ToString())
                                ? titleData["NewsId"]?.ToString()
                                : null;

                            var entity = new Import_TeamRenameEntity
                            {
                                Date = date,
                                OriginalName = originalName.Trim(),
                                NewName = newName.Trim(),
                                Verb = verb,
                                IsSamePage = isSamePage,
                                NewsId = newsId
                            };

                            results.Add(entity);
                        }
                        else
                        {
                            skippedCount++;
                            _appLogger.Warning($"Skipping record {processedCount} because 'title' is not a JObject. Raw data: {teamData}");
                        }
                        processedCount++;
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Error processing team data {processedCount}: {ex.Message}");
                        _appLogger.Error($"Raw data: {teamData}");
                        skippedCount++;
                    }
                }

                _appLogger.Info($"Deserialized {results.Count} entities out of {processedCount} processed. Skipped {skippedCount} records.");
                return results;
            });
        }



        // For Teams Table in leagupedia
        public async Task<IEnumerable<Import_TeamsTableEntity>> MapToImport_Teams(IEnumerable<JObject> apiData)
        {
            if (apiData == null)
            {
                _appLogger.Error($"The apiData parameter cannot be null for {nameof(MapToImport_Teams)}.");
                throw new ArgumentNullException(nameof(apiData), "The apiData parameter cannot be null.");
            }

            return await Task.Run(() =>
            {
                var results = new List<Import_TeamsTableEntity>();
                int processedCount = 0;
                int skippedCount = 0;
                int fallbackNameCount = 0;

                foreach (var teamData in apiData)
                {
                    try
                    {
                        if (teamData["title"] is JObject titleData)
                        {
                            // Try to get name with fallback to OverviewPage
                            string name = _apiHelper.GetNullableStringValue(teamData, "Name");
                            string overviewPage = _apiHelper.GetNullableStringValue(teamData, "OverviewPage");

                            if (string.IsNullOrWhiteSpace(name))
                            {
                                if (string.IsNullOrWhiteSpace(overviewPage))
                                {
                                    skippedCount++;
                                    _appLogger.Warning($"Skipping record {processedCount} due to missing both Name and OverviewPage. Raw data: {titleData}");
                                    continue;
                                }

                                // Use OverviewPage as name, removing any URL characters
                                name = _apiHelper.NormalizeOverviewPageToName(overviewPage);
                                fallbackNameCount++;
                                _appLogger.Info($"Using normalized OverviewPage as Name: '{overviewPage}' -> '{name}'");
                            }

                            // Validate name length
                            if (name.Length > 255)
                            {
                                _appLogger.Warning($"Name exceeds maximum length (255): '{name}'. Truncating...");
                                name = name.Substring(0, 255);
                            }

                            var entity = new Import_TeamsTableEntity
                            {
                                Name = name,
                                OverviewPage = overviewPage,
                                Short = _apiHelper.GetNullableStringValue(teamData, "ShortName"),
                                Location = _apiHelper.GetNullableStringValue(teamData, "Location"),
                                TeamLocation = _apiHelper.GetNullableStringValue(teamData, "TeamLocation"),
                                Region = _apiHelper.GetNullableStringValue(teamData, "Region"),
                                OrganizationPage = _apiHelper.GetNullableStringValue(teamData, "OrganizationPage"),
                                Image = _apiHelper.GetNullableStringValue(teamData, "Image"),
                                Twitter = _apiHelper.GetNullableStringValue(teamData, "Twitter"),
                                Youtube = _apiHelper.GetNullableStringValue(teamData, "Youtube"),
                                Facebook = _apiHelper.GetNullableStringValue(teamData, "Facebook"),
                                Instagram = _apiHelper.GetNullableStringValue(teamData, "Instagram"),
                                Discord = _apiHelper.GetNullableStringValue(teamData, "Discord"),
                                Snapchat = _apiHelper.GetNullableStringValue(teamData, "Snapchat"),
                                Vk = _apiHelper.GetNullableStringValue(teamData, "Vk"),
                                Subreddit = _apiHelper.GetNullableStringValue(teamData, "Subreddit"),
                                Website = _apiHelper.GetNullableStringValue(teamData, "Website"),
                                RosterPhoto = _apiHelper.GetNullableStringValue(teamData, "RosterPhoto"),
                                IsDisbanded = _apiHelper.GetNullableStringValue(teamData, "IsDisbanded").Equals("true", StringComparison.OrdinalIgnoreCase),
                                RenamedTo = _apiHelper.GetNullableStringValue(teamData, "RenamedTo"),
                                IsLowercase = _apiHelper.GetNullableStringValue(teamData, "IsLowercase").Equals("true", StringComparison.OrdinalIgnoreCase)
                            };

                            results.Add(entity);
                        }
                        else
                        {
                            skippedCount++;
                            _appLogger.Warning($"Skipping record {processedCount} because 'title' is not a JObject. Raw data: {teamData}");
                        }
                        processedCount++;
                    }
                    catch (Exception ex)
                    {
                        skippedCount++;
                        _appLogger.Error($"Error processing team data at record {processedCount}: {ex.Message}");
                        _appLogger.Error($"Raw data: {teamData}");
                    }
                }

                var (totalObjects, nullObjects, nullProperties) = _apiHelper.CountObjectsAndNullProperties(results.Select(r => JObject.FromObject(r)));
                _appLogger.Info($"Deserialized {results.Count} entities out of {processedCount} processed. " +
                               $"Skipped {skippedCount} records. " +
                               $"Used fallback names for {fallbackNameCount} records. " +
                               $"Total objects: {totalObjects}, Null objects: {nullObjects}, Null properties: {nullProperties}");

                return results;
            });
        }





        public async Task<IEnumerable<Import_TeamnameEntity>> MapToImport_Teamname(IEnumerable<JObject> apiData)
        {
            if (apiData == null || !apiData.Any())
            {
                _appLogger.Error($"Input data cannot be null or empty for {nameof(MapToImport_Teamname)}.");
                throw new ArgumentNullException(nameof(apiData), "Input data cannot be null or empty.");
            }

            return await Task.Run(() =>
            {
                var results = new List<Import_TeamnameEntity>();
                int processedCount = 0;
                int skippedCount = 0;

                foreach (var teamData in apiData)
                {
                    processedCount++;
                    try
                    {
                        if (teamData["title"] is JObject titleData)
                        {
                            string teamnameId = _apiHelper.GetNullableStringValue(titleData, "TeamnameId");
                            if (string.IsNullOrEmpty(teamnameId))
                            {
                                skippedCount++;
                                _appLogger.Warning($"Skipping record {processedCount} due to null or empty TeamnameId. Raw data: {titleData}");
                                continue;
                            }

                            var entity = new Import_TeamnameEntity
                            {
                                TeamnameId = teamnameId,
                                Longname = _apiHelper.GetNullableStringValue(titleData, "Longname"),
                                Short = _apiHelper.GetNullableStringValue(titleData, "ShortName"),
                                Medium = _apiHelper.GetNullableStringValue(titleData, "MediumName"),
                                Inputs = _apiHelper.GetValuesAsList(titleData, "FormattedInputs")
                            };

                            results.Add(entity);
                        }
                        else
                        {
                            skippedCount++;
                            _appLogger.Warning($"Skipping record {processedCount} due to missing or invalid 'title' object. Raw data: {teamData}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Error processing team data {processedCount}: {ex.Message}");
                        _appLogger.Error($"Raw data: {teamData}");
                    }
                }

                _appLogger.Info($"Deserialized {results.Count} entities out of {processedCount} processed. Skipped {skippedCount} records with null/empty TeamnameId.");
                return results;
            });
        }




        public async Task<List<Import_TournamentEntity>> MapToImport_Tournaments(IEnumerable<JObject> tournamentData)
        {
            if (tournamentData == null || !tournamentData.Any())
            {
                _appLogger.Error($"Input data cannot be null or empty for {nameof(MapToImport_Tournaments)}.");
                throw new ArgumentNullException(nameof(tournamentData), "Input data cannot be null or empty.");
            }

            return await Task.Run(() =>
            {
                var results = new List<Import_TournamentEntity>();
                int processedCount = 0;

                foreach (var tournament in tournamentData)
                {
                    processedCount++;
                    try
                    {
                        var entity = new Import_TournamentEntity
                        {
                            TournamentName = _apiHelper.GetStringValue(tournament, "Name"),  
                            OverviewPage = _apiHelper.GetNullableStringValue(tournament, "OverviewPage"),
                            DateStart = _apiHelper.GetNullableDateTimeFromJobject(tournament, "DateStart"),
                            Date = _apiHelper.GetNullableDateTimeFromJobject(tournament, "Date"),
                            League = _apiHelper.GetNullableStringValue(tournament, "League"),
                            Region = _apiHelper.GetNullableStringValue(tournament, "Region"),
                            Country = _apiHelper.GetNullableStringValue(tournament, "Country"),
                            ClosestTimezone = _apiHelper.GetNullableStringValue(tournament, "ClosestTimezone"),
                            EventType = _apiHelper.GetNullableStringValue(tournament, "EventType"),
                            StandardName = _apiHelper.GetNullableStringValue(tournament, "StandardName"),
                            Split = _apiHelper.GetNullableStringValue(tournament, "Split"),
                            SplitNumber = _apiHelper.GetNullableInt32Value(tournament, "SplitNumber"),
                            SplitMainPage = _apiHelper.GetNullableStringValue(tournament, "SplitMainPage"),
                            TournamentLevel = _apiHelper.GetNullableStringValue(tournament, "TournamentLevel"),
                            IsQualifier = _apiHelper.GetNullableInt32Value(tournament, "IsQualifier") == 1,
                            IsPlayoffs = _apiHelper.GetNullableInt32Value(tournament, "IsPlayoffs") == 1,
                            IsOfficial = _apiHelper.GetNullableInt32Value(tournament, "IsOfficial") == 1,
                            Year = _apiHelper.GetNullableStringValue(tournament, "Year"),
                            AlternativeNames = string.Join(',', _apiHelper.GetValuesAsList(tournament, "AlternativeNames")),
                            Tags = string.Join(',', _apiHelper.GetValuesAsList(tournament, "Tags"))
                        };

             

                        results.Add(entity);
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Error processing tournament data {processedCount}: {ex.Message}");
                        _appLogger.Error($"Raw data: {tournament}");
                    }
                }

                _appLogger.Info($"Deserialized {results.Count} tournament entities out of {processedCount} processed.");
                return results;
            });
        }










        public async Task<List<Import_LeagueEntity>> MapToImport_Leagues(IEnumerable<JObject> leagueData)
        {
            if (leagueData == null || !leagueData.Any())
            {
                _appLogger.Error($"Input data cannot be null or empty for {nameof(MapToImport_Leagues)}.");
                throw new ArgumentNullException(nameof(leagueData), "Input data cannot be null or empty.");
            }

            return await Task.Run(() =>
            {
                var results = new List<Import_LeagueEntity>();
                int processedCount = 0;

                foreach (var league in leagueData)
                {
                    processedCount++;
                    try
                    {
                        var entity = new Import_LeagueEntity
                        {
                            LeagueName = _apiHelper.GetStringValue(league, "League"),            
                            LeagueShortName = _apiHelper.GetNullableStringValue(league, "LeagueShort"),  
                            Region = _apiHelper.GetNullableStringValue(league, "Region"),       
                            Level = _apiHelper.GetNullableStringValue(league, "Level"),        
                            IsOfficial = _apiHelper.GetNullableStringValue(league, "IsOfficial") 
                        };

                        results.Add(entity);
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Error processing league data {processedCount}: {ex.Message}");
                        _appLogger.Error($"Raw data: {league}");
                    }
                }

                _appLogger.Info($"Deserialized {results.Count} league entities out of {processedCount} processed.");
                return results;
            });
        }



    }
}