using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Migrations
{
    [ContextType(typeof(BookContext))]
    partial class Init
    {
        public override string Id
        {
            get { return "20150514143055_Init"; }
        }
        
        public override string ProductVersion
        {
            get { return "7.0.0-beta4-12943"; }
        }
        
        public override IModel Target
        {
            get
            {
                var builder = new BasicModelBuilder()
                    .Annotation("SqlServer:ValueGeneration", "Sequence");
                
                builder.Entity("BookPortal.Web.Domain.Models.Award", b =>
                    {
                        b.Property<bool>("AwardClosed")
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("Relational:ColumnName", "award_closed");
                        b.Property<int?>("CountryId")
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("Relational:ColumnName", "country_id");
                        b.Property<string>("Description")
                            .Annotation("OriginalValueIndex", 2)
                            .Annotation("Relational:ColumnName", "description");
                        b.Property<string>("DescriptionCopyright")
                            .Annotation("OriginalValueIndex", 3)
                            .Annotation("Relational:ColumnName", "description_copyright");
                        b.Property<string>("Homepage")
                            .Annotation("OriginalValueIndex", 4)
                            .Annotation("Relational:ColumnName", "homepage");
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 5)
                            .Annotation("Relational:ColumnName", "award_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<bool>("IsOpened")
                            .Annotation("OriginalValueIndex", 6)
                            .Annotation("Relational:ColumnName", "is_opened");
                        b.Property<int?>("LanguageId")
                            .Annotation("OriginalValueIndex", 7)
                            .Annotation("Relational:ColumnName", "language_id");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 8)
                            .Annotation("Relational:ColumnName", "name");
                        b.Property<string>("Notes")
                            .Annotation("OriginalValueIndex", 9)
                            .Annotation("Relational:ColumnName", "notes");
                        b.Property<string>("RusName")
                            .Annotation("OriginalValueIndex", 10)
                            .Annotation("Relational:ColumnName", "rusname");
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "awards");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Contest", b =>
                    {
                        b.Property<int>("AwardId")
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("Relational:ColumnName", "award_id");
                        b.Property<DateTime?>("Date")
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("Relational:ColumnName", "date");
                        b.Property<string>("Description")
                            .Annotation("OriginalValueIndex", 2)
                            .Annotation("Relational:ColumnName", "description");
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 3)
                            .Annotation("Relational:ColumnName", "contest_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 4)
                            .Annotation("Relational:ColumnName", "name");
                        b.Property<int>("NameYear")
                            .Annotation("OriginalValueIndex", 5)
                            .Annotation("Relational:ColumnName", "name_year");
                        b.Property<int>("Number")
                            .Annotation("OriginalValueIndex", 6)
                            .Annotation("Relational:ColumnName", "number");
                        b.Property<string>("Place")
                            .Annotation("OriginalValueIndex", 7)
                            .Annotation("Relational:ColumnName", "place");
                        b.Property<string>("ShortDescription")
                            .Annotation("OriginalValueIndex", 8)
                            .Annotation("Relational:ColumnName", "short_description");
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "contests");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.ContestWork", b =>
                    {
                        b.Property<int>("ContestId")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("Relational:ColumnName", "contest_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<bool>("IsWinner")
                            .Annotation("OriginalValueIndex", 2)
                            .Annotation("Relational:ColumnName", "is_winner");
                        b.Property<int?>("LinkId")
                            .Annotation("OriginalValueIndex", 3)
                            .Annotation("Relational:ColumnName", "link_id");
                        b.Property<ContestWorkType?>("LinkType")
                            .Annotation("OriginalValueIndex", 4)
                            .Annotation("Relational:ColumnName", "link_type");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 5)
                            .Annotation("Relational:ColumnName", "name");
                        b.Property<int>("NominationId")
                            .Annotation("OriginalValueIndex", 6);
                        b.Property<int>("Number")
                            .Annotation("OriginalValueIndex", 7)
                            .Annotation("Relational:ColumnName", "number");
                        b.Property<string>("Postfix")
                            .Annotation("OriginalValueIndex", 8)
                            .Annotation("Relational:ColumnName", "postfix");
                        b.Property<string>("Prefix")
                            .Annotation("OriginalValueIndex", 9)
                            .Annotation("Relational:ColumnName", "prefix");
                        b.Property<string>("RusName")
                            .Annotation("OriginalValueIndex", 10)
                            .Annotation("Relational:ColumnName", "rusname");
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "contest_works");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Country", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("Relational:ColumnName", "country_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("Relational:ColumnName", "name");
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "countries");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Language", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("Relational:ColumnName", "language_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("Relational:ColumnName", "name");
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "languages");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Nomination", b =>
                    {
                        b.Property<int>("AwardId")
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("Relational:ColumnName", "award_id");
                        b.Property<string>("Description")
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("Relational:ColumnName", "description");
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 2)
                            .Annotation("Relational:ColumnName", "nomination_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 3)
                            .Annotation("Relational:ColumnName", "name");
                        b.Property<int>("Number")
                            .Annotation("OriginalValueIndex", 4)
                            .Annotation("Relational:ColumnName", "number");
                        b.Property<string>("RusName")
                            .Annotation("OriginalValueIndex", 5)
                            .Annotation("Relational:ColumnName", "rusname");
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "nominations");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Award", b =>
                    {
                        b.ForeignKey("BookPortal.Web.Domain.Models.Country", "CountryId");
                        b.ForeignKey("BookPortal.Web.Domain.Models.Language", "LanguageId");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Contest", b =>
                    {
                        b.ForeignKey("BookPortal.Web.Domain.Models.Award", "AwardId");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.ContestWork", b =>
                    {
                        b.ForeignKey("BookPortal.Web.Domain.Models.Contest", "ContestId");
                        b.ForeignKey("BookPortal.Web.Domain.Models.Nomination", "NominationId");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Nomination", b =>
                    {
                        b.ForeignKey("BookPortal.Web.Domain.Models.Award", "AwardId");
                    });
                
                return builder.Model;
            }
        }
    }
}
