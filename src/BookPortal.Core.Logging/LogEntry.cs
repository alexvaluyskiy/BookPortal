using System;

namespace BookPortal.Core.Logging
{
    public class LogEntry
    {
        //[Required]
        public Guid OperationContext { get; set; }

        public DateTime Timestamp { get; set; }

        public string Severity { get; set; }

        public string Layer { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }
    }
}