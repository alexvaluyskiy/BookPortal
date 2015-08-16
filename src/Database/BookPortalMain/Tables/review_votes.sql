CREATE TABLE [dbo].[review_votes](
	[reviews_vote_id] [int] IDENTITY(1,1) NOT NULL,
	[date_created] [datetime2](7) NOT NULL,
	[review_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[vote] [int] NOT NULL,
	CONSTRAINT [PK_ReviewVote] PRIMARY KEY CLUSTERED ([reviews_vote_id]),
	CONSTRAINT [FK_ReviewVote_Review_ReviewId] FOREIGN KEY ([review_id]) REFERENCES [dbo].[reviews] ([review_id])
)