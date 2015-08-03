using Microsoft.ApplicationInsights;
using Microsoft.Framework.Logging;

namespace BookPortal.Core.Logging
{
    public class LoggingServiceProvider : ILoggerProvider
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly LogLevel _minLevel;

        public LoggingServiceProvider(TelemetryClient telemetryClient, LogLevel minLevel)
        {
            _telemetryClient = telemetryClient;
            _minLevel = minLevel;
        }

        public ILogger CreateLogger(string name)
        {
            return new ApplicationInsightsLogger(_telemetryClient, _minLevel);
        }
    }
}