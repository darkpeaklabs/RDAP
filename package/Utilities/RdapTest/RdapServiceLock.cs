using System.Diagnostics;

namespace DarkPeakLabs.Rdap.Utilities
{
    internal sealed class RdapServiceLock : IDisposable
    {
        private readonly SemaphoreSlim _semaphoreSlim;
        private readonly string _host;
        private DateTimeOffset? _retryAfter;

        public RdapServiceLock(string host)
        {
            _semaphoreSlim = new SemaphoreSlim(1, 1);
            _host = host ?? throw new ArgumentNullException(paramName: nameof(host));
        }

        internal async Task WaitAsync(int id)
        {
            do
            {
                var retryAfter = await GetRetryAfterAsync().ConfigureAwait(false);
                if (!retryAfter.HasValue || retryAfter.Value < DateTimeOffset.UtcNow)
                {
                    return;
                }

                var delay = retryAfter.Value - DateTimeOffset.UtcNow;
                DebugWriteLine($"Request delayed by {delay}", id);

                await Task.Delay(delay).ConfigureAwait(false);
            }
            while (true);
        }

        private async Task<DateTimeOffset?> GetRetryAfterAsync()
        {
            await _semaphoreSlim.WaitAsync().ConfigureAwait(false);
            try
            {
                return _retryAfter;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        internal async Task AddDelayAsync(DateTimeOffset? dateTime, int id)
        {
            await _semaphoreSlim.WaitAsync().ConfigureAwait(false);
            try
            {
                dateTime ??= DateTime.UtcNow + TimeSpan.FromSeconds(30);
                if (!_retryAfter.HasValue || _retryAfter.Value < dateTime)
                {
                    _retryAfter = dateTime.Value;
                    DebugWriteLine($"Delay requests until {_retryAfter}", id);
                }
                else
                {
                    DebugWriteLine($"Delay has not increased, using current value {_retryAfter}", id);
                }
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        private void DebugWriteLine(string message, int id)
        {
            Console.WriteLine($"[{_host}:{id}] {message}");
        }

        public void Dispose()
        {
            _semaphoreSlim?.Dispose();
        }
    }
}
