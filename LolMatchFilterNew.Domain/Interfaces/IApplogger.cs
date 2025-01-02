using Domain.DTOs.TeamnameDTO; 

namespace LolMatchFilterNew.Domain.Interfaces.IAppLoggers
{
    public interface IAppLogger
    {

        void Info(string message);
        void Info(string message, params object[] propertyValues);
        void Debug(string message);
        void Debug(string message, params object[] propertyValues);
        void Warning(string message);
        void Warning(string message, params object[] propertyValues);
        void Error(string message, Exception ex);
        void Error(string message, Exception ex, params object[] propertyValues);
        void Error(string message, params object[] propertyValues);
        void Fatal(string message);
        void Fatal(string message, Exception ex);



        Task InfoAsync(string message);
        Task InfoAsync(string message, params object[] propertyValues);
        Task DebugAsync(string message);
        Task DebugAsync(string message, params object[] propertyValues);
        Task WarningAsync(string message);
        Task WarningAsync(string message, params object[] propertyValues);
        Task ErrorAsync(string message, Exception ex);
        Task ErrorAsync(string message, Exception ex, params object[] propertyValues);
        Task ErrorAsync(string message, params object[] propertyValues);
        Task FatalAsync(string message);
        Task FatalAsync(string message, Exception ex);








    }
}