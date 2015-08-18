CREATE TABLE [dbo].[genre_works_groups] (
	[genre_work_group_id] INT IDENTITY(1,1) NOT NULL,
    [name] NVARCHAR(100) NOT NULL, 
    [level] INT NOT NULL,
	CONSTRAINT [PK_genreworksgroups] PRIMARY KEY CLUSTERED ([genre_work_group_id]),
)
