CREATE TABLE [dbo].[genre_persons_view](
	[genre_person_view_id] [int] IDENTITY(1,1) NOT NULL,
	[genre_count] [int] NOT NULL,
	[genre_total] [int] NOT NULL,
	[person_id] [int] NOT NULL,
	[genre_work_id] [int] NOT NULL,
	CONSTRAINT [PK_genrepersonsview] PRIMARY KEY CLUSTERED ([genre_person_view_id]),
	CONSTRAINT [FK_genrepersonsview_persons_personid] FOREIGN KEY ([person_id]) REFERENCES [dbo].[persons] ([person_id])
)