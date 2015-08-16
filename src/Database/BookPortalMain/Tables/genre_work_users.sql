CREATE TABLE [dbo].[genre_work_users](
	[genre_work_user_id] [int] IDENTITY(1,1) NOT NULL,
	[genre_work_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[work_id] [int] NOT NULL,
	CONSTRAINT [PK_GenreWorkUser] PRIMARY KEY CLUSTERED ([genre_work_user_id]),
	CONSTRAINT [FK_GenreWorkUser_GenreWork_GenreWorkId] FOREIGN KEY ([genre_work_id]) REFERENCES [dbo].[genre_works] ([genre_work_id]),
	CONSTRAINT [FK_GenreWorkUser_Work_WorkId] FOREIGN KEY ([work_id]) REFERENCES [dbo].[works] ([work_id])
)