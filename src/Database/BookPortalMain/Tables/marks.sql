CREATE TABLE [dbo].[marks](
	[mark_id] [int] IDENTITY(1,1) NOT NULL,
	[mark_value] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[work_id] [int] NOT NULL,
	CONSTRAINT [PK_marks] PRIMARY KEY CLUSTERED ([mark_id]), 
    CONSTRAINT [FK_marks_works_work_id] FOREIGN KEY ([work_id]) REFERENCES [dbo].[works]([work_id])
)