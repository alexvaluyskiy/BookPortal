using System.Collections.Generic;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Domain.Models.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BookPortal.Web.Models.Responses
{
    public class WorkResponse
    {
        public int WorkId { get; set; }

        public List<PersonResponse> Persons { get; set; } 

        public string RusName { get; set; }

        public string Name { get; set; }

        public string AltName { get; set; }

        public int? Year { get; set; }

        public string Description { get; set; }

        public string Notes { get; set; }

        public int WorkTypeId { get; set; }

        public string WorkTypeName { get; set; }

        public int? WorkTypeLevel { get; set; }

        public List<int> ChildWorks { get; set; }

        public object GroupIndex { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PublishType? PublishType { get; set; }

        public bool? NotFinished { get; set; }

        public bool? InPlans { get; set; }

        public byte? ShowInBiblio { get; set; }

        public byte? ShowSubworksInBiblio { get; set; }

        public IReadOnlyList<AwardItemResponse> Awards { get; set; }
        public IReadOnlyList<EditionResponse> Editions { get; set; }
        public IReadOnlyList<ReviewResponse> Reviews { get; set; }
        public IReadOnlyList<TranslationResponse> Translations { get; set; }
        public IReadOnlyList<GenreWorkResponse> Genres { get; set; }

        // public int? ParentWorkId { get; set; }
    }
}
