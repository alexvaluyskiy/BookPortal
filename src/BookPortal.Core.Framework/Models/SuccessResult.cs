using System.Runtime.Serialization;

namespace BookPortal.Core.Framework.Models
{
    [DataContract]
    public class SuccessResult<T>
    {
        [DataMember(Name = "result")]
        public T Result { get; set; }
    }
}
