
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/08/2016 17:33:53
-- Generated from EDMX file: d:\ПРОЕКТЫ\Sigma\Let-s-Eat-Bee-ProjectSource\Let-s-Eat-Bee-Project\LEBDatabaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LEBDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderIUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_OrderIUser];
GO
IF OBJECT_ID(N'[dbo].[FK_JoiningIUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoiningSet] DROP CONSTRAINT [FK_JoiningIUser];
GO
IF OBJECT_ID(N'[dbo].[FK_JoiningOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoiningSet] DROP CONSTRAINT [FK_JoiningOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageSet] DROP CONSTRAINT [FK_MessageOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageIUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageSet] DROP CONSTRAINT [FK_MessageIUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UnauthorizedUser_inherits_IUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_UnauthorizedUser] DROP CONSTRAINT [FK_UnauthorizedUser_inherits_IUser];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthorizedUser_inherits_IUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_AuthorizedUser] DROP CONSTRAINT [FK_AuthorizedUser_inherits_IUser];
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
IF OBJECT_ID(N'[dbo].[MessageSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MessageSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet_UnauthorizedUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet_UnauthorizedUser];
GO
IF OBJECT_ID(N'[dbo].[UserSet_AuthorizedUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet_AuthorizedUser];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreationDateTime] datetime  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [CreatorId] int  NOT NULL
);
GO

-- Creating table 'JoiningSet'
CREATE TABLE [dbo].[JoiningSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [OrderId] int  NOT NULL
);
GO

-- Creating table 'MessageSet'
CREATE TABLE [dbo].[MessageSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [CreationDateTime] datetime  NOT NULL,
    [OrderId] int  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'UserSet_UnauthorizedUser'
CREATE TABLE [dbo].[UserSet_UnauthorizedUser] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'UserSet_AuthorizedUser'
CREATE TABLE [dbo].[UserSet_AuthorizedUser] (
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [EmailConfirmed] bit  NOT NULL,
    [Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [PK_MessageSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet_UnauthorizedUser'
ALTER TABLE [dbo].[UserSet_UnauthorizedUser]
ADD CONSTRAINT [PK_UserSet_UnauthorizedUser]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet_AuthorizedUser'
ALTER TABLE [dbo].[UserSet_AuthorizedUser]
ADD CONSTRAINT [PK_UserSet_AuthorizedUser]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CreatorId] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_OrderIUser]
    FOREIGN KEY ([CreatorId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderIUser'
CREATE INDEX [IX_FK_OrderIUser]
ON [dbo].[OrderSet]
    ([CreatorId]);
GO

-- Creating foreign key on [UserId] in table 'JoiningSet'
ALTER TABLE [dbo].[JoiningSet]
ADD CONSTRAINT [FK_JoiningIUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JoiningIUser'
CREATE INDEX [IX_FK_JoiningIUser]
ON [dbo].[JoiningSet]
    ([UserId]);
GO

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

-- Creating foreign key on [OrderId] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [FK_MessageOrder]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageOrder'
CREATE INDEX [IX_FK_MessageOrder]
ON [dbo].[MessageSet]
    ([OrderId]);
GO

-- Creating foreign key on [UserId] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [FK_MessageIUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageIUser'
CREATE INDEX [IX_FK_MessageIUser]
ON [dbo].[MessageSet]
    ([UserId]);
GO

-- Creating foreign key on [Id] in table 'UserSet_UnauthorizedUser'
ALTER TABLE [dbo].[UserSet_UnauthorizedUser]
ADD CONSTRAINT [FK_UnauthorizedUser_inherits_IUser]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'UserSet_AuthorizedUser'
ALTER TABLE [dbo].[UserSet_AuthorizedUser]
ADD CONSTRAINT [FK_AuthorizedUser_inherits_IUser]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------