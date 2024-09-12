using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Microsoft.Extensions.Logging;

namespace LolMatchFilterNew.Infrastructure.Logging.ActivityService
{
    public class ActivityService : IActivityService
    {
        private readonly IAppLogger _appLogger;
        private readonly ActivitySource _activitySource;

        public ActivityService(IAppLogger appLogger, ActivitySource activitySource)
        {
            _appLogger = appLogger ?? throw new ArgumentNullException(nameof(appLogger));
            _activitySource = activitySource ?? throw new ArgumentNullException(nameof(activitySource));

            if (!_activitySource.HasListeners())
            {
                _appLogger.Warning("ActivitySource has no listeners. Activities may not be created.");
            }
        }

        public async Task<Activity> StartActivityAsync(string name)
        {
            var activity = _activitySource.StartActivity(name);
            if (activity == null)
            {
                await _appLogger.WarningAsync($"Failed to create activity: {name}. ActivitySource.HasListeners: {_activitySource.HasListeners()}");
            }
            await LogActivityStartAsync(activity, name);
            return activity;
        }

        public async Task<Activity> StartChildActivityAsync(Activity parent, string name)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent), "Parent activity cannot be null when starting a child activity.");
            }

            var childActivity = _activitySource.StartActivity(name, ActivityKind.Internal, parent.Context);
            if (childActivity == null)
            {
                _appLogger.Warning($"Failed to start child activity: {name} under parent {parent.DisplayName}");
                return null; 
            }

            await LogChildActivityStartAsync(childActivity, parent, name);
            return childActivity;
        }

        public async Task StopActivityAsync(Activity activity)
        {
            if (activity != null)
            {
                activity.Stop();
                await _appLogger.InfoAsync($"Stopped activity: {activity.DisplayName}. TraceId: {activity.TraceId}, SpanId: {activity.SpanId}");
            }
            else
            {
                await _appLogger.InfoAsync("Attempted to stop a null activity");
            }
        }

        private async Task LogActivityStartAsync(Activity? activity, string name)
        {
            if (activity != null)
            {
                await _appLogger.InfoAsync($"Started activity: {name}. TraceId: {activity.TraceId}, SpanId: {activity.SpanId}");
            }
            else
            {
                await _appLogger.InfoAsync($"Failed to start activity: {name}");
            }
        }

        private async Task LogChildActivityStartAsync(Activity? childActivity, Activity? parent, string name)
        {
            if (childActivity != null && parent != null)
            {
                await _appLogger.InfoAsync($"Started child activity: {name} under parent {parent.DisplayName}. TraceId: {childActivity.TraceId}, SpanId: {childActivity.SpanId}");
            }
            else
            {
                await _appLogger.InfoAsync($"Failed to start child activity: {name} under parent {parent?.DisplayName ?? "null"}");
            }
        }
    }
}