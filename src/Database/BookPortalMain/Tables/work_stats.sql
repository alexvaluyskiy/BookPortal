CREATE TABLE [dbo].[work_stats](
	[work_stat_id] [int] IDENTITY(1,1) NOT NULL,
	[work_id] [int] NOT NULL,
	[marks_count] [int] NOT NULL,
	[rating] [float] NOT NULL,
	CONSTRAINT [PK_workstats] PRIMARY KEY CLUSTERED ([work_stat_id]),
	CONSTRAINT [FK_workstats_works_workid] FOREIGN KEY ([work_id]) REFERENCES [dbo].[works] ([work_id]),
)
GO

CREATE UNIQUE INDEX [IX_workstats_workid] ON [dbo].[work_stats] ([work_id])
