CREATE TABLE [dbo].[edition_translations](
	[edition_translation_id] [int] IDENTITY(1,1) NOT NULL,
	[edition_id] [int] NOT NULL,
	[name] [nvarchar](255) NULL,
	[translation_work_id] [int] NOT NULL,
	CONSTRAINT [PK_EditionTranslation] PRIMARY KEY CLUSTERED ([edition_translation_id]),
	CONSTRAINT [FK_EditionTranslation_Edition_EditionId] FOREIGN KEY ([edition_id]) REFERENCES [dbo].[editions] ([edition_id]),
	CONSTRAINT [FK_EditionTranslation_TranslationWork_TranslationWorkId] FOREIGN KEY ([translation_work_id]) REFERENCES [dbo].[translation_works] ([translation_work_id])
)