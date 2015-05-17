using System;
using Microsoft.Framework.Logging;

namespace BookPortal.Core.Logging
{
    public class LoggingServiceProvider : ILoggerProvider
    {
        private readonly Uri _loggingServiceUrl;
        private readonly string _applicationName;
        private readonly LogLevel _minLevel;

        public LoggingServiceProvider(Uri loggingServiceUrl, string applicationName) 
            : this(loggingServiceUrl, applicationName, LogLevel.Verbose)
        {

        }

        public LoggingServiceProvider(Uri loggingServiceUrl, string applicationName, LogLevel minLevel)
        {
            _loggingServiceUrl = loggingServiceUrl;
            _applicationName = applicationName;
            _minLevel = minLevel;
        }

        public ILogger CreateLogger(string name)
        {
            return new LoggingServiceLogger(_loggingServiceUrl, _applicationName, _minLevel);
        }
    }
}