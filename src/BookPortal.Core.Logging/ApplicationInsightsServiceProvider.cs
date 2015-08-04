using Microsoft.ApplicationInsights;
using Microsoft.Framework.Logging;

namespace BookPortal.Core.Logging
{
    public class ApplicationInsightsServiceProvider : ILoggerProvider
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly LogLevel _minLevel;

        public ApplicationInsightsServiceProvider(TelemetryClient telemetryClient, LogLevel minLevel)
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