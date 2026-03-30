BEGIN TRANSACTION;
GO

DROP INDEX [IX_QuantityMeasurements_CreatedAt] ON [QuantityMeasurements];
GO

DROP INDEX [IX_QuantityMeasurements_Operation] ON [QuantityMeasurements];
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Username] nvarchar(100) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260330053344_AddUserEntity', N'8.0.0');
GO

COMMIT;
GO

