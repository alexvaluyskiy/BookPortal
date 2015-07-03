using System.Runtime.Serialization;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Core.Framework.Models
{
    [DataContract]
    public class ErrorResult
    {
        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        [DataMember(Name = "modelerrors", EmitDefaultValue = false)]
        public SerializableError ModelErrors { get; set; }

        [DataMember(Name = "details", EmitDefaultValue = false)]
        public string Details { get; set; }
    }
}
