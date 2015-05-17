using System;
using Microsoft.Framework.Logging;

namespace BookPortal.Core.Logging
{
    public static class LoggingServiceLoggerExtensions
    {
        public static ILoggerFactory AddLoggingService(this ILoggerFactory factory, string applicationName, string loggingServiceUri)
        {
            return AddLoggingService(factory, loggingServiceUri, applicationName, LogLevel.Verbose);
        }

        public static ILoggerFactory AddLoggingService(
            this ILoggerFactory factory,
            string loggingServiceUri,
            string applicationName,
            LogLevel minLevel)
        {
            factory.AddProvider(new LoggingServiceProvider(new Uri(loggingServiceUri), applicationName, minLevel));
            return factory;
        }
    }
}