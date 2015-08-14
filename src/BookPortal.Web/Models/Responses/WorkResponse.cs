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

        public int? WorkTypeId { get; set; }

        public string WorkTypeName { get; set; }

        public int? WorkTypeLevel { get; set; }

        public bool? WorkTypeNode { get; set; }

        public List<WorkResponse> ChildWorks { get; set; }

        public object GroupIndex { get; set; }

        public string PublishType { get; set; }

        public string NotFinished { get; set; }

        public bool? InPlans { get; set; }

        public byte? ShowInBiblio { get; set; }

        public byte? ShowSubworksInBiblio { get; set; }

        public IReadOnlyList<AwardItemResponse> Awards { get; set; }
        public IReadOnlyList<EditionResponse> Editions { get; set; }
        public IReadOnlyList<ReviewResponse> Reviews { get; set; }
        public IReadOnlyList<TranslationResponse> Translations { get; set; }
        public IReadOnlyList<GenreWorkResponse> Genres { get; set; }
        public int VotesCount { get; set; }
        public double Rating { get; set; }
        public bool IsAddition { get; set; }
        public string BonusText { get; set; }
        public string CoAuthorType { get; set; }
        public int UserMark { get; set; }
        public int? RootCycleWorkId { get; set; }
        public string RootCycleWorkName { get; set; }
        public int? RootCycleWorkTypeId { get; set; }

        // public int? ParentWorkId { get; set; }

        public WorkResponse Clone()
        {
            return (WorkResponse) MemberwiseClone();
        }
    }
}
