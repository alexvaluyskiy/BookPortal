CREATE TABLE [dbo].[rating_work_expect_view](
	[rating_work_expect_id] [int] IDENTITY(1,1) NOT NULL,
	[expect_type] [int] NOT NULL,
	[plan_date] [nvarchar](10) NULL,
	[users_count] [int] NOT NULL,
	[work_id] [int] NOT NULL,
	CONSTRAINT [PK_RatingWorkExpectView] PRIMARY KEY CLUSTERED ([rating_work_expect_id]),
	CONSTRAINT [FK_RatingWorkExpectView_Work_WorkId] FOREIGN KEY ([work_id]) REFERENCES [dbo].[works] ([work_id])
)