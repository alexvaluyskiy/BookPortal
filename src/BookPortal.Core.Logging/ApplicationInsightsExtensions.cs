using System;
using Microsoft.ApplicationInsights;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace BookPortal.Core.Logging
{
    public static class ApplicationInsightsExtensions
    {
        public static ILoggerFactory AddApplicationInsightsLoggingService(
            this ILoggerFactory factory,
            IApplicationBuilder app,
            LogLevel minLevel)
        {
            var telemetryClient = app.ApplicationServices.GetService<TelemetryClient>();
            factory.AddProvider(new ApplicationInsightsServiceProvider(telemetryClient, minLevel));
            return factory;
        }
    }
}
