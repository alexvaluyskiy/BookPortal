using System.Runtime.Serialization;

namespace BookPortal.Core.ApiPrimitives.Models
{
    [DataContract]
    public class ApiSingleResult
    {
        [DataMember(Name = "result")]
        public object Result { get; set; }
    }
}
