CREATE TABLE [dbo].[awards](
	[award_id] [int] IDENTITY(1,1) NOT NULL,
	[rusname] [nvarchar](255) NOT NULL,
	[name] [nvarchar](255) NULL,
	[award_closed] [bit] NOT NULL,
	[country_id] [int] NULL,
	[language_id] [int] NULL,
	[description] [nvarchar](max) NULL,
	[description_source] [nvarchar](255) NULL,
	[notes] [nvarchar](max) NULL,
	[homepage] [nvarchar](255) NULL,
	[is_opened] [bit] NOT NULL, 
	CONSTRAINT [PK_awards] PRIMARY KEY ([award_id]),
	CONSTRAINT [FK_awards_countries_countryid] FOREIGN KEY ([country_id]) REFERENCES [dbo].[countries] ([country_id]),
	CONSTRAINT [FK_awards_languages_languageid] FOREIGN KEY ([language_id]) REFERENCES [dbo].[languages] ([language_id])    
)