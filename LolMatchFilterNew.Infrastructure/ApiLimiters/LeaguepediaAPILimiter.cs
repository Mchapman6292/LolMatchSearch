using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaAPILimiter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LolMatchFilterNew.Infrastructure.ApiLimiters.LeaguepediaAPILimiter
{
    // This is to ensure that any calls made observe the requested delay of 1-2 seconds and to avoid rate limits from Leaguepedia.
    public class LeaguepediaAPILimiter : ILeaguepediaAPILimiter
    {
        private readonly TimeSpan _timeBetweenRequests;
        private  DateTime _lastRequestTime;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);


        public LeaguepediaAPILimiter(TimeSpan timeBetweenRequests)
        {
            _timeBetweenRequests = timeBetweenRequests;
        }

        // I am not fully confident that my handling of Async calls won't result in two threads accessing this method at the same time & violating the limit.
        // SemaphoreSlim limits the number of threads that can access a resource concurrently, limit is set to 1.
        public async Task WaitForNextRequestAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                var timeSinceLastRequest = DateTime.UtcNow - _lastRequestTime;
                if (timeSinceLastRequest < _timeBetweenRequests)
                {
                    await Task.Delay(_timeBetweenRequests - timeSinceLastRequest);
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
