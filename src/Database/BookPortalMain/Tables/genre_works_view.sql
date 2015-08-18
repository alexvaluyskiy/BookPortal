CREATE TABLE [dbo].[genre_works_view](
	[genre_work_view_id] [int] IDENTITY(1,1) NOT NULL,
	[work_id] [int] NOT NULL,
	[genre_work_id] [int] NOT NULL,
	[genre_count] [int] NOT NULL,
	CONSTRAINT [PK_genreworksview] PRIMARY KEY CLUSTERED ([genre_work_view_id]),
	CONSTRAINT [FK_genreworksview_works_workid] FOREIGN KEY ([work_id]) REFERENCES [dbo].[works] ([work_id])
)