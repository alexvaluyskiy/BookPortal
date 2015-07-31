using System.Collections.Generic;

namespace BookPortal.Web.Models.Responses
{
    public class SerieTreeItem
    {
        public int SerieId { get; set; }

        public string Name { get; set; }

        public List<SerieTreeItem> Series { get; set; }
    }
}