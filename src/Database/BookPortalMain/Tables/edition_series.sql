CREATE TABLE [dbo].[edition_series](
	[edition_serie_id] [int] IDENTITY(1,1) NOT NULL,
	[edition_id] [int] NOT NULL,
	[serie_id] [int] NOT NULL,
	[sort] [int] NOT NULL,
	CONSTRAINT [PK_EditionSerie] PRIMARY KEY CLUSTERED ([edition_serie_id]),
	CONSTRAINT [FK_EditionSerie_Edition_EditionId] FOREIGN KEY ([edition_id]) REFERENCES [dbo].[editions] ([edition_id]),
	CONSTRAINT [FK_EditionSerie_Serie_SerieId] FOREIGN KEY ([serie_id]) REFERENCES [dbo].[series] ([serie_id])
)