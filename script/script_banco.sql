CREATE DATABASE [crud_produtos]
GO

USE crud_produtos
GO

CREATE DATABASE [crud_produtos1]
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Produtos] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(100) NOT NULL,
    [Descricao] nvarchar(max) NOT NULL,
    [Preco] decimal(18,2) NOT NULL,
    [NomeArquivo] nvarchar(2500) NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210102230044_Initial', N'3.1.1');

GO

