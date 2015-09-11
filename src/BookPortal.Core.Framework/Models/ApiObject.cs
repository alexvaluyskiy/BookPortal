using System.Collections.Generic;
using System.Linq;

namespace BookPortal.Core.Framework.Models
{
    public class ApiObject<T> where T : new()
    {
        public ApiObject()
        {
            Values = new List<T>();
        }

        public ApiObject(IEnumerable<T> values) : this(values.ToList())
        {

        }

        public ApiObject(IList<T> values)
        {
            Values = values;
            TotalRows = Values.Count;
        }

        public ApiObject(IList<T> values, int totalRows)
        {
            Values = values;
            TotalRows = totalRows;
        }

        public int TotalRows { get; set; }

        public IList<T> Values { get; set; }
    }
}
