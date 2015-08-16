CREATE TABLE [dbo].[nominations](
	[nomination_id] [int] IDENTITY(1,1) NOT NULL,
	[award_id] [int] NOT NULL,
	[rusname] [nvarchar](255) NULL,
	[name] [nvarchar](255) NOT NULL,
	[description] [nvarchar](max) NULL,
	[number] [int] NOT NULL,
	CONSTRAINT [PK_Nomination] PRIMARY KEY CLUSTERED ([nomination_id]),
	CONSTRAINT [FK_Nomination_Award_AwardId] FOREIGN KEY ([award_id]) REFERENCES [dbo].[awards] ([award_id])
)