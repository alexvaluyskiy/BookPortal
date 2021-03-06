﻿CREATE TABLE [dbo].[genre_works](
	[genre_work_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[description] [nvarchar](max) NULL,
	[genre_work_group_id] [int] NOT NULL,
	[level] [int] NOT NULL,
	[parent_genre_work_id] [int] NULL,
	CONSTRAINT [PK_genreworks] PRIMARY KEY CLUSTERED ([genre_work_id]),
	CONSTRAINT [FK_genreworks_genreworksgroups_genreworkgroupid] FOREIGN KEY ([genre_work_group_id]) REFERENCES [dbo].[genre_works_groups] ([genre_work_group_id]),
)