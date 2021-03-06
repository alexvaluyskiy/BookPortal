﻿CREATE TABLE [dbo].[contests](
	[contest_id] [int] IDENTITY(1,1) NOT NULL,
	[award_id] [int] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[date] [nvarchar](10) NOT NULL,
	[place] [nvarchar](255) NULL,
	[description] [nvarchar](max) NULL,
	[short_description] [nvarchar](max) NULL,
	[name_year] [int] NOT NULL,
	[number] [int] NOT NULL,
	CONSTRAINT [PK_contests] PRIMARY KEY CLUSTERED ([contest_id]),
	CONSTRAINT [FK_contests_awards_awardid] FOREIGN KEY ([award_id]) REFERENCES [dbo].[awards] ([award_id])
)
GO

CREATE INDEX [IX_contests_awardid] ON [dbo].[contests] ([award_id])
