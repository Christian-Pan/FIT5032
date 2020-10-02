
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/01/2020 01:53:28
-- Generated from EDMX file: C:\Users\Christian\Desktop\AlgorithmicThinking\AlgorithmicThinking\Models\Algorithmic_Thinking_Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Chapter_Database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SectionComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_SectionComment];
GO
IF OBJECT_ID(N'[dbo].[FK_ChapterSection]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sections] DROP CONSTRAINT [FK_ChapterSection];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_CommentComment];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Sections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sections];
GO
IF OBJECT_ID(N'[dbo].[Chapters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chapters];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Uid] nvarchar(max)  NOT NULL,
    [Datetime] datetime  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [SectionId] int  NOT NULL,
    [CommentId] int  NOT NULL
);
GO

-- Creating table 'Sections'
CREATE TABLE [dbo].[Sections] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [ChapterId] int  NOT NULL,
    [LastUpdatedTime] datetime  NOT NULL
);
GO

-- Creating table 'Chapters'
CREATE TABLE [dbo].[Chapters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [LastUpdatedTime] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sections'
ALTER TABLE [dbo].[Sections]
ADD CONSTRAINT [PK_Sections]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Chapters'
ALTER TABLE [dbo].[Chapters]
ADD CONSTRAINT [PK_Chapters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SectionId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_SectionComment]
    FOREIGN KEY ([SectionId])
    REFERENCES [dbo].[Sections]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SectionComment'
CREATE INDEX [IX_FK_SectionComment]
ON [dbo].[Comments]
    ([SectionId]);
GO

-- Creating foreign key on [ChapterId] in table 'Sections'
ALTER TABLE [dbo].[Sections]
ADD CONSTRAINT [FK_ChapterSection]
    FOREIGN KEY ([ChapterId])
    REFERENCES [dbo].[Chapters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChapterSection'
CREATE INDEX [IX_FK_ChapterSection]
ON [dbo].[Sections]
    ([ChapterId]);
GO

-- Creating foreign key on [CommentId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_CommentComment]
    FOREIGN KEY ([CommentId])
    REFERENCES [dbo].[Comments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentComment'
CREATE INDEX [IX_FK_CommentComment]
ON [dbo].[Comments]
    ([CommentId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------