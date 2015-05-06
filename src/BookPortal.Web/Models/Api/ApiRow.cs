using System.Runtime.Serialization;

namespace BookPortal.Web.Models.Api
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