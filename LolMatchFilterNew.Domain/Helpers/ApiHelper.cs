using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Xceed.Words.NET;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using Activity = System.Diagnostics.Activity;
using Microsoft.Extensions.Configuration;

namespace LolMatchFilterNew.Domain.Helpers.ApiHelper
{
    public class ApiHelper : IApiHelper
    {
        private readonly string _saveDirectory;
        private static int _fileCounter = 0;
        private readonly IAppLogger _appLogger;
        private static readonly string SaveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LolMatchReports");




        public ApiHelper(IAppLogger appLogger, IConfiguration configuration)
        {
            _appLogger = appLogger;
            _saveDirectory = configuration["SaveDirectory"]
                ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LolMatchReports");
        }

        public async Task<string> WriteToDocxDocumentAsync(Activity activity, string title, List<string> content = null)
        {
            _appLogger.Info($"Starting {nameof(WriteToDocxDocumentAsync)},  Content items: {content?.Count ?? 0}, TraceId: {activity.TraceId}.");

            try
            {
                Directory.CreateDirectory(SaveDirectory);

                string fileName;
                string fullPath;

                do
                {
                    fileName = $"CapsAhriMatches_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}_{Interlocked.Increment(ref _fileCounter)}.docx";
                    fullPath = Path.Combine(_saveDirectory, fileName);
                } while (File.Exists(fullPath));

                _appLogger.Info($"Full file path for {nameof(WriteToDocxDocumentAsync)}: {fullPath}, ParentId: {activity.ParentId}, TraceId: {activity.TraceId}.");

                using (var document = DocX.Create(fullPath))
                {
                    document.InsertParagraph(title).Bold();

                    if (content != null)
                    {
                        foreach (var item in content)
                        {
                            document.InsertParagraph(item);
                        }
                    }
                    else
                    {
                        _appLogger.Info($"No content to add to document ParentId: {activity.ParentId}, TraceId: {activity.TraceId}.");
                    }
                    document.Save();
                }
                await Task.Delay(100);
                _appLogger.Info($"File saved to: {fullPath}, Directory: {Path.GetDirectoryName(fullPath)}, Filename: {Path.GetFileName(fullPath)}, ParentId:{activity.ParentId}, TraceId: {activity.TraceId}.");

                return fullPath;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error in WriteToDocxDocumentAsync: {ex.Message}");
                _appLogger.Error($"Stack Trace: {ex.StackTrace}");
                throw;

            }
        }
    }
}

