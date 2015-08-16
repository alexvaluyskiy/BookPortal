CREATE TABLE [dbo].[work_types](
	[work_type_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[name_single] [nvarchar](50) NULL,
	[level] [int] NOT NULL,
	[is_node] [bit] NOT NULL,
	CONSTRAINT [PK_WorkType] PRIMARY KEY CLUSTERED ([work_type_id])
)