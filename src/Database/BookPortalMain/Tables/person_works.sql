CREATE TABLE [dbo].[person_works](
	[person_work_id] [int] IDENTITY(1,1) NOT NULL,
	[person_id] [int] NOT NULL,
	[type] [int] NOT NULL,
	[work_id] [int] NOT NULL,
	[order] [int] NOT NULL,
	CONSTRAINT [PK_PersonWork] PRIMARY KEY CLUSTERED ([person_work_id]),
	CONSTRAINT [FK_PersonWork_Person_PersonId] FOREIGN KEY ([person_id]) REFERENCES [dbo].[persons] ([person_id]),
	CONSTRAINT [FK_PersonWork_Work_WorkId] FOREIGN KEY([work_id]) REFERENCES [dbo].[works] ([work_id])
)