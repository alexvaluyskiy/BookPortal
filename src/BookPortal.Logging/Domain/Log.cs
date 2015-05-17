using System;
using System.ComponentModel.DataAnnotations;

namespace BookPortal.Logging.Domain
{
    public class Log
    {
        [Required]
        public Guid OperationContext { get; set; }

        public DateTime Timestamp { get; set; }

        public string Severity { get; set; }

        public string Layer { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }
    }
}