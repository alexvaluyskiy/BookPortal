using System.Runtime.Serialization;

namespace BookPortal.Core.ApiPrimitives.Models
{
    [DataContract]
    public class ApiRow
    {
        [DataMember(Name = "rownum", EmitDefaultValue = false)]
        public int RowNum { get; set; }

        [DataMember(Name = "values")]
        public object Values { get; set; }
    }
}