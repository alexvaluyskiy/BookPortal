using System.Collections.Generic;

namespace BookPortal.Web.Models
{
    public class SerieTreeItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<SerieTreeItem> Series { get; set; }
    }
}