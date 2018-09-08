using Microsoft.Extensions.Logging;
using System;

namespace Integreat
{
    internal class ConsoleLoggerFactory : ILoggerFactory
    {
        private volatile bool _disposed;

        public ILogger CreateLogger(string categoryName)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ConsoleLoggerFactory));

            return new ConsoleLogger(categoryName);
        }

        public void AddProvider(ILoggerProvider provider)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ConsoleLoggerFactory));

            throw new NotSupportedException();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }
}
