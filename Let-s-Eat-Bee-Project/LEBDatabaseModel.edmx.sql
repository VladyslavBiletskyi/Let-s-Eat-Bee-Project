
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/07/2016 00:13:21
-- Generated from EDMX file: D:\ПРОЕКТЫ\Sigma\Let-s-Eat-Bee-Project\Let-s-Eat-Bee-Project\LEBDatabaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [D:\ПРОЕКТЫ\SIGMA\LET-S-EAT-BEE-PROJECT\LET-S-EAT-BEE-PROJECT\APP_DATA\LEBDatabase.MDF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

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
IF OBJECT_ID(N'[dbo].[FK_OrderUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_OrderUser];
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
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Organization] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [CompleteDateTime] datetime  NOT NULL,
    [Creator_Email] nvarchar(50)  NULL
);
GO

-- Creating table 'JoiningSet'
CREATE TABLE [dbo].[JoiningSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TextOfOrder] nvarchar(max)  NOT NULL,
    [OrderId] int  NOT NULL,
    [UserFirstName] nvarchar(max)  NOT NULL,
    [UserLastName] nvarchar(max)  NOT NULL,
    [User_Email] nvarchar(50)  NULL
);
GO

-- Creating table 'ChatMessageSet'
CREATE TABLE [dbo].[ChatMessageSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreationDateTime] datetime  NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [OrderId] int  NOT NULL,
    [User_Email] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Email] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Email] ASC);
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

-- Creating foreign key on [OrderId] in table 'JoiningSet'
ALTER TABLE [dbo].[JoiningSet]
ADD CONSTRAINT [FK_JoiningOrder]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JoiningOrder'
CREATE INDEX [IX_FK_JoiningOrder]
ON [dbo].[JoiningSet]
    ([OrderId]);
GO

-- Creating foreign key on [OrderId] in table 'ChatMessageSet'
ALTER TABLE [dbo].[ChatMessageSet]
ADD CONSTRAINT [FK_ChatMessageOrder]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatMessageOrder'
CREATE INDEX [IX_FK_ChatMessageOrder]
ON [dbo].[ChatMessageSet]
    ([OrderId]);
GO

-- Creating foreign key on [Creator_Email] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_OrderUser]
    FOREIGN KEY ([Creator_Email])
    REFERENCES [dbo].[UserSet]
        ([Email])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderUser'
CREATE INDEX [IX_FK_OrderUser]
ON [dbo].[OrderSet]
    ([Creator_Email]);
GO

-- Creating foreign key on [User_Email] in table 'JoiningSet'
ALTER TABLE [dbo].[JoiningSet]
ADD CONSTRAINT [FK_JoiningUser]
    FOREIGN KEY ([User_Email])
    REFERENCES [dbo].[UserSet]
        ([Email])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JoiningUser'
CREATE INDEX [IX_FK_JoiningUser]
ON [dbo].[JoiningSet]
    ([User_Email]);
GO

-- Creating foreign key on [User_Email] in table 'ChatMessageSet'
ALTER TABLE [dbo].[ChatMessageSet]
ADD CONSTRAINT [FK_ChatMessageUser]
    FOREIGN KEY ([User_Email])
    REFERENCES [dbo].[UserSet]
        ([Email])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatMessageUser'
CREATE INDEX [IX_FK_ChatMessageUser]
ON [dbo].[ChatMessageSet]
    ([User_Email]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------