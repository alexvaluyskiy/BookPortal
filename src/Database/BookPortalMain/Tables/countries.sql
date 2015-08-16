CREATE TABLE [dbo].[countries]
(
	[country_id] INT IDENTITY(1,1) NOT NULL, 
    [name] NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_countries] PRIMARY KEY CLUSTERED ([country_id])
)
