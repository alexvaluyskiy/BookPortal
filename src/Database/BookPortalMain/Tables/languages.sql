CREATE TABLE [dbo].[languages]
(
	[language_id] INT IDENTITY(1,1) NOT NULL, 
    [name] NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_languages] PRIMARY KEY CLUSTERED ([language_id])
)
