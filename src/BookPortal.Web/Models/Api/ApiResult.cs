using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BookPortal.Web.Models.Api
{
    [DataContract]
    public class ApiResult
    {
        public ApiResult()
        {
            Rows = new List<ApiRow>();
        }

        [DataMember(Name = "totalrows")]
        public int TotalRows { get; set; }

        [DataMember(Name = "offset", EmitDefaultValue = false)]
        public int Offset { get; set; }

        [DataMember(Name = "limit", EmitDefaultValue = false)]
        public int Limit { get; set; }

        [DataMember(Name = "rows")]
        public IList<ApiRow> Rows { get; set; }
    }
}