CREATE TABLE [dbo].[series](
	[serie_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[description] [nvarchar](max) NULL,
	[parent_serie_id] [int] NULL,
	[serie_closed] [bit] NOT NULL,
	[language_id] [int] NULL,
	[year_close] [int] NULL,
	[year_open] [int] NULL,
	CONSTRAINT [PK_Serie] PRIMARY KEY CLUSTERED ([serie_id]),
	CONSTRAINT [FK_Serie_Language_LanguageId] FOREIGN KEY([language_id]) REFERENCES [dbo].[languages] ([language_id])
)