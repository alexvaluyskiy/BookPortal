CREATE TABLE [dbo].[work_links](
	[work_link_id] [int] IDENTITY(1,1) NOT NULL,
	[bonus_text] [nvarchar](max) NULL,
	[group_index] [int] NULL,
	[is_addition] [bit] NOT NULL,
	[link_type] [int] NOT NULL,
	[parent_work_id] [int] NULL,
	[work_id] [int] NOT NULL,
	CONSTRAINT [PK_WorkLink] PRIMARY KEY CLUSTERED ([work_link_id]),
	CONSTRAINT [FK_WorkLink_Work_ParentWorkId] FOREIGN KEY ([parent_work_id]) REFERENCES [dbo].[works] ([work_id]),
	CONSTRAINT [FK_WorkLink_Work_WorkId] FOREIGN KEY ([work_id]) REFERENCES [dbo].[works] ([work_id])
)