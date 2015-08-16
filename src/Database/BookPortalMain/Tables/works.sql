CREATE TABLE [dbo].[works](
	[work_id] [int] IDENTITY(1,1) NOT NULL,
	[rusname] [nvarchar](255) NULL,
	[name] [nvarchar](255) NULL,
	[altname] [nvarchar](255) NULL,
	[description] [nvarchar](max) NULL,
	[notes] [nvarchar](max) NULL,
	[work_type_id] [int] NOT NULL,
	[year] [int] NULL,
	[show_in_biblio] [tinyint] NOT NULL,
	[show_subworks_in_biblio] [tinyint] NOT NULL,
	[in_plans] [bit] NOT NULL,
	[publish_type] [tinyint] NOT NULL,
	[not_finished] [bit] NOT NULL,
	CONSTRAINT [PK_Work] PRIMARY KEY CLUSTERED ([work_id]),
	CONSTRAINT [FK_Work_WorkType_WorkTypeId] FOREIGN KEY ([work_type_id]) REFERENCES [dbo].[work_types] ([work_type_id])
)