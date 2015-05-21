using System.Runtime.Serialization;

namespace BookPortal.Core.ApiPrimitives.Models
{
    [DataContract]
    public class PageRow<T>
    {
        [DataMember(Name = "rownum")]
        public long RowNum { get; set; }

        [DataMember(Name = "values")]
        public T Values { get; set; }
    }
}