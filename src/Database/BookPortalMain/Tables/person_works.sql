CREATE TABLE [dbo].[person_works](
	[person_work_id] [int] IDENTITY(1,1) NOT NULL,
	[person_id] [int] NOT NULL,
	[work_id] [int] NOT NULL,
	[type] [int] NOT NULL,
	[order] [int] NOT NULL,
	CONSTRAINT [PK_personworks] PRIMARY KEY CLUSTERED ([person_work_id]),
	CONSTRAINT [FK_personworks_persons_personid] FOREIGN KEY ([person_id]) REFERENCES [dbo].[persons] ([person_id]),
	CONSTRAINT [FK_personworks_works_workid] FOREIGN KEY([work_id]) REFERENCES [dbo].[works] ([work_id]) 
)
GO

CREATE INDEX [IX_personworks_workid] ON [dbo].[person_works] ([work_id])
GO

CREATE INDEX [IX_personworks_personid] ON [dbo].[person_works] ([person_id])
