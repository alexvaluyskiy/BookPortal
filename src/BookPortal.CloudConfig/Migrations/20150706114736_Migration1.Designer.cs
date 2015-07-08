using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using BookPortal.CloudConfig.Domain;

namespace BookPortalCloudConfigMigrations
{
    [ContextType(typeof(ConfigContext))]
    partial class Migration1
    {
        public override string Id
        {
            get { return "20150706114736_Migration1"; }
        }

        public override string ProductVersion
        {
            get { return "7.0.0-beta6-13675"; }
        }

        public override void BuildTargetModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13675")
                .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

            builder.Entity("BookPortal.CloudConfig.Domain.Models.Config", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "config_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<string>("Key")
                        .Annotation("Relational:ColumnName", "key")
                        .Annotation("Relational:ColumnType", "nvarchar(50)");

                    b.Property<int>("ProfileId")
                        .Annotation("Relational:ColumnName", "profile_id");

                    b.Property<string>("Value")
                        .Annotation("Relational:ColumnName", "value")
                        .Annotation("Relational:ColumnType", "nvarchar(250)");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "configs");
                });

            builder.Entity("BookPortal.CloudConfig.Domain.Models.ConfigProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "profile_id")
                        .Annotation("SqlServer:ItentityStrategy", "IdentityColumn");

                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name")
                        .Annotation("Relational:ColumnType", "nvarchar(50)");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "profiles");
                });

            builder.Entity("BookPortal.CloudConfig.Domain.Models.Config", b =>
                {
                    b.Reference("BookPortal.CloudConfig.Domain.Models.ConfigProfile")
                        .InverseCollection()
                        .ForeignKey("ProfileId");
                });
        }
    }
}
