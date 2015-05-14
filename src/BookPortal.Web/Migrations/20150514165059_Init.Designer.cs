using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using BookPortal.Web.Domain;

namespace BookPortal.Web.Migrations
{
    [ContextType(typeof(BookContext))]
    partial class Init
    {
        public override string Id
        {
            get { return "20150514165059_Init"; }
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
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("Relational:ColumnName", "contest_id");
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("Relational:ColumnName", "contest_work_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<bool>("IsWinner")
                            .Annotation("OriginalValueIndex", 2)
                            .Annotation("Relational:ColumnName", "is_winner");
                        b.Property<int?>("LinkId")
                            .Annotation("OriginalValueIndex", 3)
                            .Annotation("Relational:ColumnName", "link_id");
                        b.Property<int>("LinkType")
                            .Annotation("OriginalValueIndex", 4)
                            .Annotation("Relational:ColumnName", "link_type");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 5)
                            .Annotation("Relational:ColumnName", "name");
                        b.Property<int>("NominationId")
                            .Annotation("OriginalValueIndex", 6)
                            .Annotation("Relational:ColumnName", "nomination_id");
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
                
                builder.Entity("BookPortal.Web.Domain.Models.Edition", b =>
                    {
                        b.Property<string>("Authors")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<int>("Count")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("Format")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 3)
                            .Annotation("Relational:ColumnName", "edition_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<int>("Page")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<string>("Publishers")
                            .Annotation("OriginalValueIndex", 6);
                        b.Property<string>("Series")
                            .Annotation("OriginalValueIndex", 7);
                        b.Property<int>("WorkId")
                            .Annotation("OriginalValueIndex", 8);
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "editions");
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
                
                builder.Entity("BookPortal.Web.Domain.Models.Person", b =>
                    {
                        b.Property<string>("Biography")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("BiographySource")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<DateTime?>("Birthdate")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<int>("CountryId")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<DateTime?>("Deathdate")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<int>("Gender")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 6)
                            .Annotation("Relational:ColumnName", "person_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<int>("LanguageId")
                            .Annotation("OriginalValueIndex", 7);
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 8);
                        b.Property<string>("NameOriginal")
                            .Annotation("OriginalValueIndex", 9);
                        b.Property<string>("NameRp")
                            .Annotation("OriginalValueIndex", 10);
                        b.Property<string>("Notes")
                            .Annotation("OriginalValueIndex", 11);
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "persons");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Publisher", b =>
                    {
                        b.Property<int>("CountryId")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<DateTime?>("DateClose")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<DateTime?>("DateOpen")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<string>("Description")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<string>("DescriptionSource")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 5)
                            .Annotation("Relational:ColumnName", "publisher_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 6);
                        b.Property<int>("Type")
                            .Annotation("OriginalValueIndex", 7);
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "publishers");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Serie", b =>
                    {
                        b.Property<DateTime?>("DateClose")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<DateTime?>("DateOpen")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("Description")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 3)
                            .Annotation("Relational:ColumnName", "serie_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<int>("PublisherId")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<bool>("SerieClosed")
                            .Annotation("OriginalValueIndex", 6);
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "series");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.TranslationEdition", b =>
                    {
                        b.Property<int>("EditionId")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("Relational:ColumnName", "translation_editions")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<int>("TranslationWorkId")
                            .Annotation("OriginalValueIndex", 3);
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "translation_editions");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.TranslationWork", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("Relational:ColumnName", "translation_work_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<int>("WorkId")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<int>("Year")
                            .Annotation("OriginalValueIndex", 2);
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "translation_works");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.TranslationWorkPerson", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("Relational:ColumnName", "translation_work_person_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<int>("PersonId")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<int>("TranslationWorkId")
                            .Annotation("OriginalValueIndex", 2);
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "translation_work_persons");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Work", b =>
                    {
                        b.Property<string>("Description")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("Relational:ColumnName", "work_id")
                            .Annotation("SqlServer:ValueGeneration", "Identity");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<int>("PersonId")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<int>("Year")
                            .Annotation("OriginalValueIndex", 4);
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "works");
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
                
                builder.Entity("BookPortal.Web.Domain.Models.Edition", b =>
                    {
                        b.ForeignKey("BookPortal.Web.Domain.Models.Work", "WorkId");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Nomination", b =>
                    {
                        b.ForeignKey("BookPortal.Web.Domain.Models.Award", "AwardId");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Person", b =>
                    {
                        b.ForeignKey("BookPortal.Web.Domain.Models.Country", "CountryId");
                        b.ForeignKey("BookPortal.Web.Domain.Models.Language", "LanguageId");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Publisher", b =>
                    {
                        b.ForeignKey("BookPortal.Web.Domain.Models.Country", "CountryId");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Serie", b =>
                    {
                        b.ForeignKey("BookPortal.Web.Domain.Models.Publisher", "PublisherId");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.TranslationEdition", b =>
                    {
                        b.ForeignKey("BookPortal.Web.Domain.Models.Edition", "EditionId");
                        b.ForeignKey("BookPortal.Web.Domain.Models.TranslationWork", "TranslationWorkId");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.TranslationWork", b =>
                    {
                        b.ForeignKey("BookPortal.Web.Domain.Models.Work", "WorkId");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.TranslationWorkPerson", b =>
                    {
                        b.ForeignKey("BookPortal.Web.Domain.Models.Person", "PersonId");
                        b.ForeignKey("BookPortal.Web.Domain.Models.TranslationWork", "TranslationWorkId");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Work", b =>
                    {
                        b.ForeignKey("BookPortal.Web.Domain.Models.Person", "PersonId");
                    });
                
                return builder.Model;
            }
        }
    }
}
