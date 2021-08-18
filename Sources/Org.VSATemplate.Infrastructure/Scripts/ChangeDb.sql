BEGIN TRANSACTION;
GO

ALTER TABLE [Students] DROP CONSTRAINT [FK_Students_Classes_ClassId];
GO

DROP INDEX [IX_Students_ClassId] ON [Students];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'ClassId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Students] DROP COLUMN [ClassId];
GO

ALTER TABLE [Classes] ADD [StudentId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

CREATE INDEX [IX_Classes_StudentId] ON [Classes] ([StudentId]);
GO

ALTER TABLE [Classes] ADD CONSTRAINT [FK_Classes_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210818154205_ChangeDb', N'5.0.8');
GO

COMMIT;
GO

