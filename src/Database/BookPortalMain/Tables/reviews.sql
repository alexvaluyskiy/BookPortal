CREATE TABLE [dbo].[reviews](
	[review_id] [int] IDENTITY(1,1) NOT NULL,
	[date_created] [datetime2](7) NOT NULL,
	[text] [nvarchar](max) NULL,
	[user_id] [int] NOT NULL,
	[work_id] [int] NOT NULL,
	CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED ([review_id]),
	CONSTRAINT [FK_Review_Work_WorkId] FOREIGN KEY ([work_id]) REFERENCES [dbo].[works] ([work_id])
)
