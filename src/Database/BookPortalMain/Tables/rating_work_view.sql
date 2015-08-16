CREATE TABLE [dbo].[rating_work_view](
	[rating_work_id] [int] IDENTITY(1,1) NOT NULL,
	[marks_count] [int] NOT NULL,
	[rating] [float] NOT NULL,
	[rating_type] [nvarchar](50) NULL,
	[work_id] [int] NOT NULL,
	CONSTRAINT [PK_RatingWorkView] PRIMARY KEY CLUSTERED ([rating_work_id]),
	CONSTRAINT [FK_RatingWorkView_Work_WorkId] FOREIGN KEY ([work_id]) REFERENCES [dbo].[works] ([work_id])
)