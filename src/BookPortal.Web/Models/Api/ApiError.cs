using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BookPortal.Web.Models.Api
{
    [DataContract]
    public class ApiError
    {
        public ApiError()
        {
            Errors = new List<ApiErrorItem>();
        }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "errors", EmitDefaultValue = false)]
        public List<ApiErrorItem> Errors { get; set; }
    }
}
