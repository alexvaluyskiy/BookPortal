using System.Collections.Generic;

namespace BookPortal.Core.Framework.Models
{
    public class ApiObject<T> where T : new()
    {
        public ApiObject()
        {
            Values = new List<T>();
        }

        public int TotalRows { get; set; }

        public IReadOnlyList<T> Values { get; set; }
    }
}
