using Serilog;
using Serilog.Context;
using System;
using System.Diagnostics;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;

namespace LolMatchFilterNew.Infrastructure.Logging.AppLoggers
{
    public class AppLogger : IAppLogger
    {
        private readonly Serilog.ILogger _logger;

        public AppLogger()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(@"C:\LolMatchFilterNewLogs\LolMatchFilterNewLogs.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 1)
                .CreateLogger();
        }



        // Method overloading, allows multiple methods with same name but different parameter lists. 
        public void Info(string message) => _logger.Information(message); // General information regarding app operations, e.g user login.
        public void Info(string message, params object[] propertyValues) => _logger.Information(message, propertyValues);

        public void Debug(string message) => _logger.Debug(message); // Detailed info used for debugging, e.g loaded X variables from database in 200ms/
        public void Debug(string message, params object[] propertyValues) => _logger.Debug(message, propertyValues);
        public void Warning(string message) => _logger.Warning(message); // Unexpected events that might lead to problems in future e.g Disk space running low.
        public void Warning(string message, params object[] propertyValues) => _logger.Warning(message, propertyValues);
        public void Error(string message, Exception ex) => _logger.Error(ex, message); // Errors/exceptions that cannot be handled/interupt execution of current operation, e.g Disk space running low.
        public void Error(string message, Exception ex, params object[] propertyValues) => _logger.Error(ex, message, propertyValues);
        public void Error(string message, params object[] propertyValues) => _logger.Error(message, propertyValues); //Cases with no exceptions for general logging.
        public void Fatal(string message) => _logger.Fatal(message); // Critical issues that cause app to stop running/operate in degraded state, should be used sparingly
        public void Fatal(string message, Exception ex) => _logger.Fatal(ex, message);




        public Task InfoAsync(string message) => Task.Run(() => Info(message));
        public Task InfoAsync(string message, params object[] propertyValues) => Task.Run(() => Info(message, propertyValues));
        public Task DebugAsync(string message) => Task.Run(() => Debug(message));
        public Task DebugAsync(string message, params object[] propertyValues) => Task.Run(() => Debug(message, propertyValues));
        public Task WarningAsync(string message) => Task.Run(() => Warning(message));
        public Task WarningAsync(string message, params object[] propertyValues) => Task.Run(() => Warning(message, propertyValues));
        public Task ErrorAsync(string message, Exception ex) => Task.Run(() => Error(message, ex));
        public Task ErrorAsync(string message, Exception ex, params object[] propertyValues) => Task.Run(() => Error(message, ex, propertyValues));
        public Task ErrorAsync(string message, params object[] propertyValues) => Task.Run(() => Error(message, propertyValues));
        public Task FatalAsync(string message) => Task.Run(() => Fatal(message));
        public Task FatalAsync(string message, Exception ex) => Task.Run(() => Fatal(message,ex));



    }
}