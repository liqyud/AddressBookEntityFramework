
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/16/2017 21:33:22
-- Generated from EDMX file: D:\Dev\PROG17\AddressBookEntityFramework\AddressBookFromDatabase\AddressBookModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AdressBok];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_KontaktAdress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Adresses] DROP CONSTRAINT [FK_KontaktAdress];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Adresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Adresses];
GO
IF OBJECT_ID(N'[dbo].[Kontakts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kontakts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Adresses'
CREATE TABLE [dbo].[Adresses] (
    [AdressId] int IDENTITY(1,1) NOT NULL,
    [Gatuadress] varchar(200)  NOT NULL,
    [Postnummer] varchar(10)  NOT NULL,
    [Postort] varchar(200)  NOT NULL,
    [KontaktId] int  NULL
);
GO

-- Creating table 'Kontakts'
CREATE TABLE [dbo].[Kontakts] (
    [KontaktId] int IDENTITY(1,1) NOT NULL,
    [Namn] varchar(200)  NOT NULL,
    [Telefon] varchar(20)  NOT NULL,
    [Epost] varchar(200)  NOT NULL,
    [KontaktTyp] varchar(20)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AdressId] in table 'Adresses'
ALTER TABLE [dbo].[Adresses]
ADD CONSTRAINT [PK_Adresses]
    PRIMARY KEY CLUSTERED ([AdressId] ASC);
GO

-- Creating primary key on [KontaktId] in table 'Kontakts'
ALTER TABLE [dbo].[Kontakts]
ADD CONSTRAINT [PK_Kontakts]
    PRIMARY KEY CLUSTERED ([KontaktId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [KontaktId] in table 'Adresses'
ALTER TABLE [dbo].[Adresses]
ADD CONSTRAINT [FK_KontaktAdress]
    FOREIGN KEY ([KontaktId])
    REFERENCES [dbo].[Kontakts]
        ([KontaktId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_KontaktAdress'
CREATE INDEX [IX_FK_KontaktAdress]
ON [dbo].[Adresses]
    ([KontaktId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------