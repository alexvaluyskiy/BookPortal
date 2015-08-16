CREATE TABLE [dbo].[edition_publishers](
	[edition_publisher_id] [int] IDENTITY(1,1) NOT NULL,
	[edition_id] [int] NOT NULL,
	[publisher_id] [int] NOT NULL,
	CONSTRAINT [PK_EditionPublisher] PRIMARY KEY CLUSTERED ([edition_publisher_id]),
	CONSTRAINT [FK_EditionPublisher_Edition_EditionId] FOREIGN KEY ([edition_id]) REFERENCES [dbo].[editions] ([edition_id]),
	CONSTRAINT [FK_EditionPublisher_Publisher_PublisherId] FOREIGN KEY([publisher_id]) REFERENCES [dbo].[publishers] ([publisher_id])
)
