CREATE TABLE [dbo].[rating_author_view](
	[rating_author_id] [int] IDENTITY(1,1) NOT NULL,
	[marks_count] [int] NOT NULL,
	[marks_weight] [int] NOT NULL,
	[person_id] [int] NOT NULL,
	[rating] [float] NOT NULL,
	[users_count] [int] NOT NULL,
	CONSTRAINT [PK_RatingAuthorView] PRIMARY KEY CLUSTERED ([rating_author_id]),
	CONSTRAINT [FK_RatingAuthorView_Person_PersonId] FOREIGN KEY ([person_id]) REFERENCES [dbo].[persons] ([person_id])
)