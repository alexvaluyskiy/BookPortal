using System.Collections.Generic;

namespace BookPortal.Web.Models.Responses
{
    public class GenreWorkResponse
    {
        public int GenreWorkId { get; set; }
        public int? GenreParentWorkId { get; set; }
        public string Name { get; set; }
        public int? Level { get; set; }
        public int GenreCount { get; set; }
        public int GenreTotal { get; set; }
        public int? GenreWorkGroupId { get; set; }

        public List<GenreWorkResponse> Genres { get; set; }
    }
}
