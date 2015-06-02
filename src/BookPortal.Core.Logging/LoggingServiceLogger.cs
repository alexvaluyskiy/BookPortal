using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Framework.Logging;
using Newtonsoft.Json;

namespace BookPortal.Core.Logging
{
    public class LoggingServiceLogger : ILogger
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly LogLevel _minLogLevel;
        private readonly Uri _loggingServiceUrl;
        private readonly string _applicationName;

        public LoggingServiceLogger(Uri loggingServiceUrl, string applicationName, LogLevel minLevel)
        {
            _loggingServiceUrl = loggingServiceUrl;
            _applicationName = applicationName;
            _minLogLevel = minLevel;
        }

        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var message = string.Empty;
            if (state != null)
            {
                message += state;
            }
            if (exception != null)
            {
                message += Environment.NewLine + exception;
            }

            if (!string.IsNullOrEmpty(message))
            {
                LogEntry entry = new LogEntry();
                entry.OperationContext = Guid.NewGuid();
                entry.Message = message;
                entry.Timestamp = DateTime.UtcNow;
                entry.Layer = _applicationName;
                entry.Severity = logLevel.ToString();

                HttpRequestMessage requestMessage = new HttpRequestMessage();
                requestMessage.RequestUri = new Uri(_loggingServiceUrl, "api/logs");
                requestMessage.Method = HttpMethod.Post;
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(entry));
                requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                _client.SendAsync(requestMessage).Wait();
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _minLogLevel;
        }

        public IDisposable BeginScopeImpl(object state)
        {
            return null;
        }
    }
}
