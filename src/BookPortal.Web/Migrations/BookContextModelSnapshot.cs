using System;
using BookPortal.Web.Domain;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;

namespace BookPortal.Web.Migrations
{
    [ContextType(typeof(BookContext))]
    partial class BookContextModelSnapshot : ModelSnapshot
    {
        public override IModel Model
        {
            get
            {
                var builder = new BasicModelBuilder()
                    .Annotation("SqlServer:ValueGeneration", "Sequence");
                
                builder.Entity("BookPortal.Web.Domain.Models.Award", b =>
                    {
                        b.Property<int>("AwardType")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("Comment")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("Compiler")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<string>("Copyright")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<string>("CopyrightLink")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<int?>("CountryId")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<string>("Description")
                            .Annotation("OriginalValueIndex", 6);
                        b.Property<string>("Homepage")
                            .Annotation("OriginalValueIndex", 7);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 8)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<bool>("IsClosed")
                            .Annotation("OriginalValueIndex", 9);
                        b.Property<bool>("IsOpened")
                            .Annotation("OriginalValueIndex", 10);
                        b.Property<int?>("LanguageId")
                            .Annotation("OriginalValueIndex", 11);
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 12);
                        b.Property<string>("Notes")
                            .Annotation("OriginalValueIndex", 13);
                        b.Property<string>("ProcessStatus")
                            .Annotation("OriginalValueIndex", 14);
                        b.Property<string>("RusName")
                            .Annotation("OriginalValueIndex", 15);
                        b.Property<bool>("ShowInList")
                            .Annotation("OriginalValueIndex", 16);
                        b.Key("Id");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Contest", b =>
                    {
                        b.Property<int>("AwardId")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<DateTime?>("Date")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("Description")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 3)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<int>("NameYear")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<int>("Number")
                            .Annotation("OriginalValueIndex", 6);
                        b.Property<string>("Place")
                            .Annotation("OriginalValueIndex", 7);
                        b.Property<string>("ShortDescription")
                            .Annotation("OriginalValueIndex", 8);
                        b.Key("Id");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.ContestWork", b =>
                    {
                        b.Property<int>("ContestId")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<bool>("IsWinner")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<int>("NominationId")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<int>("Number")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<string>("Postfix")
                            .Annotation("OriginalValueIndex", 6);
                        b.Property<string>("Prefix")
                            .Annotation("OriginalValueIndex", 7);
                        b.Property<string>("RusName")
                            .Annotation("OriginalValueIndex", 8);
                        b.Key("Id");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Country", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 1);
                        b.Key("Id");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Language", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 1);
                        b.Key("Id");
                    });
                
                builder.Entity("BookPortal.Web.Domain.Models.Nomination", b =>
                    {
                        b.Property<int>("AwardId")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("Description")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 2)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<int>("Number")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<int>("RusName")
                            .Annotation("OriginalValueIndex", 5);
                        b.Key("Id");
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
