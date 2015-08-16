CREATE TABLE [dbo].[translation_works](
	[translation_work_id] [int] IDENTITY(1,1) NOT NULL,
	[language_id] [int] NOT NULL,
	[work_id] [int] NOT NULL,
	[year] [int] NULL,
	CONSTRAINT [PK_TranslationWork] PRIMARY KEY CLUSTERED ([translation_work_id]),
	CONSTRAINT [FK_TranslationWork_Language_LanguageId] FOREIGN KEY ([language_id]) REFERENCES [dbo].[languages] ([language_id]),
	CONSTRAINT [FK_TranslationWork_Work_WorkId] FOREIGN KEY ([work_id]) REFERENCES [dbo].[works] ([work_id])
)