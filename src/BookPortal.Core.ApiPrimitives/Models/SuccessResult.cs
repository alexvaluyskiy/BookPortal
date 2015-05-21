using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BookPortal.Core.ApiPrimitives.Models
{
    [DataContract]
    public class SuccessResult<T>
    {
        [DataMember(Name = "result")]
        public T Result { get; set; }
    }
}
