CREATE TABLE [dbo].[contest_works](
	[contest_work_id] [int] IDENTITY(1,1) NOT NULL,
	[contest_id] [int] NOT NULL,
	[rusname] [nvarchar](255) NULL,
	[name] [nvarchar](255) NULL,
	[is_winner] [bit] NOT NULL,
	[link_id] [int] NULL,
	[link_type] [int] NOT NULL,
	[nomination_id] [int] NULL,
	[number] [int] NOT NULL,
	[postfix] [nvarchar](255) NULL,
	[prefix] [nvarchar](255) NULL,
    CONSTRAINT [PK_contestworks] PRIMARY KEY CLUSTERED ([contest_work_id]),
	CONSTRAINT [FK_contestworks_contests_contestid] FOREIGN KEY ([contest_id]) REFERENCES [dbo].[contests] ([contest_id]),
	CONSTRAINT [FK_contestworks_nominations_nominationid] FOREIGN KEY ([nomination_id]) REFERENCES [dbo].[nominations] ([nomination_id])
)