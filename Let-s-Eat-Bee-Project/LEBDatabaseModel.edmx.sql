
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/05/2016 21:49:00
-- Generated from EDMX file: d:\ПРОЕКТЫ\Sigma\Let-s-Eat-Bee-Project\Let-s-Eat-Bee-Project\LEBDatabaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [D:\ПРОЕКТЫ\SIGMA\LET-S-EAT-BEE-PROJECT\LET-S-EAT-BEE-PROJECT\APP_DATA\LEBDATABASE.MDF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_OrderUser];
GO
IF OBJECT_ID(N'[dbo].[FK_JoiningUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoiningSet] DROP CONSTRAINT [FK_JoiningUser];
GO
IF OBJECT_ID(N'[dbo].[FK_JoiningOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoiningSet] DROP CONSTRAINT [FK_JoiningOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_ChatMessageUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChatMessageSet] DROP CONSTRAINT [FK_ChatMessageUser];
GO
IF OBJECT_ID(N'[dbo].[FK_ChatMessageOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChatMessageSet] DROP CONSTRAINT [FK_ChatMessageOrder];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[OrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderSet];
GO
IF OBJECT_ID(N'[dbo].[JoiningSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JoiningSet];
GO
IF OBJECT_ID(N'[dbo].[ChatMessageSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChatMessageSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Organization] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [CompleteDateTime] datetime  NOT NULL,
    [Creator_Id] int  NULL
);
GO

-- Creating table 'JoiningSet'
CREATE TABLE [dbo].[JoiningSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TextOfOrder] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [OrderId] int  NOT NULL
);
GO

-- Creating table 'ChatMessageSet'
CREATE TABLE [dbo].[ChatMessageSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreationDateTime] datetime  NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [OrderId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JoiningSet'
ALTER TABLE [dbo].[JoiningSet]
ADD CONSTRAINT [PK_JoiningSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ChatMessageSet'
ALTER TABLE [dbo].[ChatMessageSet]
ADD CONSTRAINT [PK_ChatMessageSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'JoiningSet'
ALTER TABLE [dbo].[JoiningSet]
ADD CONSTRAINT [FK_JoiningUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JoiningUser'
CREATE INDEX [IX_FK_JoiningUser]
ON [dbo].[JoiningSet]
    ([UserId]);
GO

-- Creating foreign key on [OrderId] in table 'JoiningSet'
ALTER TABLE [dbo].[JoiningSet]
ADD CONSTRAINT [FK_JoiningOrder]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE CASCADE;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JoiningOrder'
CREATE INDEX [IX_FK_JoiningOrder]
ON [dbo].[JoiningSet]
    ([OrderId]);
GO

-- Creating foreign key on [UserId] in table 'ChatMessageSet'
ALTER TABLE [dbo].[ChatMessageSet]
ADD CONSTRAINT [FK_ChatMessageUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE CASCADE;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatMessageUser'
CREATE INDEX [IX_FK_ChatMessageUser]
ON [dbo].[ChatMessageSet]
    ([UserId]);
GO

-- Creating foreign key on [OrderId] in table 'ChatMessageSet'
ALTER TABLE [dbo].[ChatMessageSet]
ADD CONSTRAINT [FK_ChatMessageOrder]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE CASCADE;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatMessageOrder'
CREATE INDEX [IX_FK_ChatMessageOrder]
ON [dbo].[ChatMessageSet]
    ([OrderId]);
GO

-- Creating foreign key on [Creator_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_OrderUser]
    FOREIGN KEY ([Creator_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderUser'
CREATE INDEX [IX_FK_OrderUser]
ON [dbo].[OrderSet]
    ([Creator_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------