using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Logging;

namespace BookPortal.Core.Logging
{
    public class DebugLogger : ILogger
    {
        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            var message = string.Empty;
            if (state != null)
            {
                message += state;
            }
            if (exception != null)
            {
                message += Environment.NewLine + exception;
            }

            Debug.WriteLine(message);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScopeImpl(object state)
        {
            return null;
        }
    }

    public class DebugLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string name)
        {
            return new DebugLogger();
        }
    }
}
