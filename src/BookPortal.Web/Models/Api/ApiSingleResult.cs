using System.Runtime.Serialization;

namespace BookPortal.Web.Models.Api
{
    [DataContract]
    public class ApiSingleResult
    {
        [DataMember(Name = "result")]
        public object Result { get; set; }
    }
}
