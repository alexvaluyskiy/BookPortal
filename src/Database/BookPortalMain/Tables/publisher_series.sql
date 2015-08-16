CREATE TABLE [dbo].[publisher_series](
	[publisher_serie_id] [int] IDENTITY(1,1) NOT NULL,
	[publisher_id] [int] NOT NULL,
	[serie_id] [int] NOT NULL,
	CONSTRAINT [PK_PublisherSerie] PRIMARY KEY CLUSTERED ([publisher_serie_id]),
	CONSTRAINT [FK_PublisherSerie_Publisher_PublisherId] FOREIGN KEY ([publisher_id]) REFERENCES [dbo].[publishers] ([publisher_id]),
	CONSTRAINT [FK_PublisherSerie_Serie_SerieId] FOREIGN KEY([serie_id]) REFERENCES [dbo].[series] ([serie_id])
)