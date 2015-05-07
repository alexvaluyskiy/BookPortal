using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace BookPortal.Web.Models.Api
{
    [DataContract]
    public class ApiError
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "errors", EmitDefaultValue = false)]
        public ICollection<ApiErrorItem> Errors { get; set; }

        [DataMember(Name = "modelerrors", EmitDefaultValue = false)]
        public IEnumerable<ModelError> ModelErrors { get; set; }
    }
}
