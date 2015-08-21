CREATE TABLE [dbo].[genre_work_users](
	[genre_work_user_id] [int] IDENTITY(1,1) NOT NULL,
	[genre_work_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[work_id] [int] NOT NULL,
	CONSTRAINT [PK_genreworkusers] PRIMARY KEY CLUSTERED ([genre_work_user_id]),
	CONSTRAINT [FK_genreworkusers_genreworks_genreworkid] FOREIGN KEY ([genre_work_id]) REFERENCES [dbo].[genre_works] ([genre_work_id]),
	CONSTRAINT [FK_genreworkusersr_works_workid] FOREIGN KEY ([work_id]) REFERENCES [dbo].[works] ([work_id])
)
GO

CREATE INDEX [IX_genreworkusers_workid] ON [dbo].[genre_work_users] ([work_id]) INCLUDE ([user_id])
