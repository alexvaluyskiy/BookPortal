CREATE TABLE [dbo].[translation_work_persons](
	[translation_work_person_id] [int] IDENTITY(1,1) NOT NULL,
	[person_id] [int] NOT NULL,
	[translation_work_id] [int] NOT NULL,
	[person_order] [int] NOT NULL,
	CONSTRAINT [PK_TranslationWorkPerson] PRIMARY KEY CLUSTERED ([translation_work_person_id]),
	CONSTRAINT [FK_TranslationWorkPerson_Person_PersonId] FOREIGN KEY ([person_id]) REFERENCES [dbo].[persons] ([person_id]),
	CONSTRAINT [FK_TranslationWorkPerson_TranslationWork_TranslationWorkId] FOREIGN KEY ([translation_work_id]) REFERENCES [dbo].[translation_works] ([translation_work_id])
)