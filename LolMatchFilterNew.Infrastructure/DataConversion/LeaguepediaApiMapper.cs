using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities;
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
using LolMatchFilterNew.Domain.Entities.Processed_LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.Processed_ProPlayerEntities;
using LolMatchFilterNew.Domain.Entities.Processed_TeamRenameEntities;
using LolMatchFilterNew.Domain.Entities.Import_TeamsTableEntities;

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


        public async Task<IEnumerable<Import_ScoreboardGamesEntity>> MapLeaguepediaDataToEntity(IEnumerable<JObject> leaguepediaData)
        {
            if (leaguepediaData == null || !leaguepediaData.Any())
            {
                _appLogger.Error($"Input data cannot be null or empty for {nameof(MapLeaguepediaDataToEntity)}.");
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
                            LeaguepediaGameIdAndTitle = _apiHelper.GetStringValue(matchData, "GameId"),
                            GameName = _apiHelper.GetStringValue(matchData, "Gamename"),
                            League = _apiHelper.GetStringValue(matchData, "League"),
                            DateTimeUTC = _apiHelper.ParseDateTime(matchData, "DateTime UTC"),
                            Tournament = _apiHelper.GetStringValue(matchData, "Tournament"),
                            Team1 = _apiHelper.GetStringValue(matchData, "Team1"),
                            Team2 = _apiHelper.GetStringValue(matchData, "Team2"),
                            Team1Players = string.Join(',', _apiHelper.GetValuesAsList(matchData, "Team1Players")),
                            Team2Players = string.Join(',', _apiHelper.GetValuesAsList(matchData, "Team2Players")),
                            Team1Picks = string.Join(',', _apiHelper.GetValuesAsList(matchData, "Team1Picks")),
                            Team2Picks = string.Join(',', _apiHelper.GetValuesAsList(matchData, "Team2Picks")),
                            WinTeam = _apiHelper.GetStringValue(matchData, "WinTeam"),
                            LossTeam = _apiHelper.GetStringValue(matchData, "LossTeam"),
                            Team1Kills = _apiHelper.GetInt32Value(matchData, "Team1Kills"),
                            Team2Kills = _apiHelper.GetInt32Value(matchData, "Team2Kills")

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

        public async Task<IEnumerable<Processed_LeagueTeamEntity>> MapLeaguepediaDataToLeagueTeamEntity(IEnumerable<JObject> leaguepediaData)
        {
            if (leaguepediaData == null || !leaguepediaData.Any())
            {
                _appLogger.Error($"Input data cannot be null or empty for {nameof(MapLeaguepediaDataToLeagueTeamEntity)}.");
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
                        string teamName = _apiHelper.GetStringValue(teamData, "TeamName");

                        if (string.IsNullOrWhiteSpace(teamName))
                        {
                            skippedCount++;
                            _appLogger.Warning($"Skipping record {processedCount} due to null or empty team name. Raw data: {teamData}");
                            continue;
                        }

                        var entity = new Processed_LeagueTeamEntity
                        {
                            TeamName = teamName,
                            NameShort = _apiHelper.GetStringValue(teamData, "ShortName"),
                            Region = _apiHelper.GetStringValue(teamData, "Region")
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
                            string teamName = _apiHelper.GetStringValue(titleData, "Name");
                            if (string.IsNullOrEmpty(teamName))
                            {
                                skippedCount++;
                                _appLogger.Warning($"Skipping record {processedCount} due to null or empty team name. Raw data: {titleData}");
                                continue;
                            }

                            var entity = new Processed_LeagueTeamEntity
                            {
                                TeamName = teamName,
                                NameShort = _apiHelper.GetStringValue(titleData, "Short"),
                                Region = _apiHelper.GetStringValue(titleData, "Region")
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


        public async Task<IEnumerable<Processed_TeamRenameEntity>> MapJTokenToTeamRenameEntity(IEnumerable<JObject> apiData)
        {
            if (apiData == null)
            {
                _appLogger.Error($"The apiData parameter cannot be null for {nameof(MapJTokenToTeamRenameEntity)}.");
                throw new ArgumentNullException(nameof(apiData), "The apiData parameter cannot be null.");
            }

            return await Task.Run(() =>
            {
                var results = new List<Processed_TeamRenameEntity>();
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
                            DateTime date = _apiHelper.ParseDateTime(titleData, "Date");

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

                            var entity = new Processed_TeamRenameEntity
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
        public async Task<IEnumerable<Import_TeamsTableEntity>> MapJTokenToLpediaTeamEntity(IEnumerable<JObject> apiData)
        {
            if (apiData == null)
            {
                _appLogger.Error($"The apiData parameter cannot be null for {nameof(MapJTokenToLpediaTeamEntity)}.");
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
                            string name = _apiHelper.GetStringValue(teamData, "Name");
                            string overviewPage = _apiHelper.GetStringValue(teamData, "OverviewPage");

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
                                Short = _apiHelper.GetStringValue(teamData, "Short"),
                                Location = _apiHelper.GetStringValue(teamData, "Location"),
                                TeamLocation = _apiHelper.GetStringValue(teamData, "TeamLocation"),
                                Region = _apiHelper.GetStringValue(teamData, "Region"),
                                OrganizationPage = _apiHelper.GetStringValue(teamData, "OrganizationPage"),
                                Image = _apiHelper.GetStringValue(teamData, "Image"),
                                Twitter = _apiHelper.GetStringValue(teamData, "Twitter"),
                                Youtube = _apiHelper.GetStringValue(teamData, "Youtube"),
                                Facebook = _apiHelper.GetStringValue(teamData, "Facebook"),
                                Instagram = _apiHelper.GetStringValue(teamData, "Instagram"),
                                Discord = _apiHelper.GetStringValue(teamData, "Discord"),
                                Snapchat = _apiHelper.GetStringValue(teamData, "Snapchat"),
                                Vk = _apiHelper.GetStringValue(teamData, "Vk"),
                                Subreddit = _apiHelper.GetStringValue(teamData, "Subreddit"),
                                Website = _apiHelper.GetStringValue(teamData, "Website"),
                                RosterPhoto = _apiHelper.GetStringValue(teamData, "RosterPhoto"),
                                IsDisbanded = _apiHelper.GetStringValue(teamData, "IsDisbanded").Equals("true", StringComparison.OrdinalIgnoreCase),
                                RenamedTo = _apiHelper.GetStringValue(teamData, "RenamedTo"),
                                IsLowercase = _apiHelper.GetStringValue(teamData, "IsLowercase").Equals("true", StringComparison.OrdinalIgnoreCase)
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


    }
}