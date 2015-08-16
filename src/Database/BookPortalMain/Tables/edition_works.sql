CREATE TABLE [dbo].[edition_works](
	[edition_work_id] [int] IDENTITY(1,1) NOT NULL,
	[edition_id] [int] NOT NULL,
	[work_id] [int] NOT NULL,
	CONSTRAINT [PK_EditionWork] PRIMARY KEY CLUSTERED ([edition_work_id]),
	CONSTRAINT [FK_EditionWork_Edition_EditionId] FOREIGN KEY ([edition_id]) REFERENCES [dbo].[editions] ([edition_id]),
	CONSTRAINT [FK_EditionWork_Work_WorkId] FOREIGN KEY ([work_id]) REFERENCES [dbo].[works] ([work_id])
)