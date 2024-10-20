using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaAPILimiter;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Infrastructure.ApiLimiters.LeaguepediaAPILimiter
{
    // This is to ensure that any calls made observe a delay of 2 seconds to avoid rate limits from Leaguepedia.
    public class LeaguepediaAPILimiter : ILeaguepediaAPILimiter
    {
        private static readonly TimeSpan TimeBetweenRequests = TimeSpan.FromSeconds(4);
        private DateTime _lastRequestTime;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly IAppLogger _appLogger;

        public LeaguepediaAPILimiter(IAppLogger appLogger)
        {
            _lastRequestTime = DateTime.UtcNow;
            _appLogger = appLogger;
        }

        public async Task WaitForNextRequestAsync()
        {
            Stopwatch timer = new Stopwatch();

            timer.Start();
            await _semaphore.WaitAsync();
            try
            {
                await Task.Delay(TimeBetweenRequests);
            }
            finally
            {
                _semaphore.Release();
                timer.Stop();
                TimeSpan duration = timer.Elapsed;
                _appLogger.Info($"Duration of {nameof(WaitForNextRequestAsync)}: {duration}");
            }
        }
    }
}