using System;
using Microsoft.ApplicationInsights;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.Logging;

namespace BookPortal.Core.Logging
{
    public static class ApplicationInsightsExtensions
    {
        public static IApplicationBuilder UseApplicationInsightsTracingTelemetry(this IApplicationBuilder app, LogLevel minLevel)
        {
            var loggerFactory = (ILoggerFactory)app.ApplicationServices.GetService(typeof(ILoggerFactory));
            var telemetryClient = (TelemetryClient)app.ApplicationServices.GetService(typeof(TelemetryClient));
            loggerFactory.AddProvider(new LoggingServiceProvider(telemetryClient, minLevel));

            return app;
        }
    }
}
