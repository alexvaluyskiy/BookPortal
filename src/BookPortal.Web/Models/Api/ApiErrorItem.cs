using System.Runtime.Serialization;

namespace BookPortal.Web.Models.Api
{
    [DataContract]
    public class ApiErrorItem
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "errorcode", EmitDefaultValue = false)]
        public string ErrorCode { get; set; }

        [DataMember(Name = "details", EmitDefaultValue = false)]
        public string Details { get; set; }
    }
}