CREATE TABLE [dbo].[publishers](
	[publisher_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[country_id] [int] NULL,
	[description] [nvarchar](max) NULL,
	[description_source] [nvarchar](max) NULL,
	[type] [int] NOT NULL,
	[year_close] [int] NULL,
	[year_open] [int] NULL,
	CONSTRAINT [PK_Publisher] PRIMARY KEY CLUSTERED ([publisher_id]),
	CONSTRAINT [FK_Publisher_Country_CountryId] FOREIGN KEY([country_id]) REFERENCES [dbo].[countries] ([country_id])
)