using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaAPILimiter;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Infrastructure.ApiLimiters.LeaguepediaAPILimiter
{
    // This is to ensure that any calls made observe a delay of 2 seconds to avoid rate limits from Leaguepedia.
    public class LeaguepediaAPILimiter : ILeaguepediaAPILimiter
    {
        private static readonly TimeSpan TimeBetweenRequests = TimeSpan.FromSeconds(2);
        private DateTime _lastRequestTime;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public LeaguepediaAPILimiter()
        {
            _lastRequestTime = DateTime.UtcNow;
        }

        public async Task WaitForNextRequestAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                var timeSinceLastRequest = DateTime.UtcNow - _lastRequestTime;
                if (timeSinceLastRequest < TimeBetweenRequests)
                {
                    await Task.Delay(TimeBetweenRequests - timeSinceLastRequest);
                }
                _lastRequestTime = DateTime.UtcNow;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}