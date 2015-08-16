CREATE TABLE [dbo].[persons](
	[person_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[name_original] [nvarchar](255) NULL,
	[name_rp] [nvarchar](255) NULL,
	[name_sort] [nvarchar](255) NULL,
	[biography] [nvarchar](max) NULL,
	[biography_source] [nvarchar](255) NULL,
	[birthdate] [nvarchar](50) NULL,
	[country_id] [int] NULL,
	[deathdate] [nvarchar](50) NULL,
	[gender] [int] NOT NULL,
	[default_language_id] [int] NULL,
	[notes] [nvarchar](max) NULL,
	[is_opened] [bit] NOT NULL,
	CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([person_id]),
	CONSTRAINT [FK_Person_Country_CountryId] FOREIGN KEY ([country_id]) REFERENCES [dbo].[countries] ([country_id])
)