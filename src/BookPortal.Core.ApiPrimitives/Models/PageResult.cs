using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BookPortal.Core.ApiPrimitives.Models
{
    [DataContract]
    public class PageResult<T>
    {
        [DataMember(Name = "totalrows", EmitDefaultValue = false)]
        public long TotalRows { get; set; }

        [DataMember(Name = "offset", EmitDefaultValue = false)]
        public long Offset { get; set; }

        [DataMember(Name = "limit", EmitDefaultValue = false)]
        public long Limit { get; set; }

        [DataMember(Name = "rows")]
        public T Rows { get; set; }
    }
}