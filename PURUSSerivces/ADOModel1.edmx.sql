
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/18/2016 03:28:21
-- Generated from EDMX file: C:\Users\Amir\Documents\Visual Studio 2013\Projects\PURUSSerivces\PURUSSerivces\ADOModel1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyInsuranceDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Insurers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Insurers];
GO
IF OBJECT_ID(N'[dbo].[Quotes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Quotes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(max)  NULL,
    [Password] nvarchar(max)  NULL,
    [Role] nvarchar(max)  NULL,
    [FullName] nvarchar(max)  NULL,
    [Age] int  NULL,
    [YearsOfInsurance] int  NULL,
    [City] nvarchar(max)  NULL,
    [InsuranceType] nvarchar(max)  NULL
);
GO

-- Creating table 'Insurers'
CREATE TABLE [dbo].[Insurers] (
    [ID] int  NOT NULL,
    [CustomerID] int  NOT NULL
);
GO

-- Creating table 'Quotes'
CREATE TABLE [dbo].[Quotes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CustomerID] int  NOT NULL,
    [Status] nvarchar(max)  NULL,
    [MinimumCoverage] decimal(18,2)  NOT NULL,
    [MaximumCoverage] decimal(18,2)  NOT NULL,
    [InsurerID] int  NOT NULL,
    [CustomerQuote_Quote_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID], [CustomerID] in table 'Insurers'
ALTER TABLE [dbo].[Insurers]
ADD CONSTRAINT [PK_Insurers]
    PRIMARY KEY CLUSTERED ([ID], [CustomerID] ASC);
GO

-- Creating primary key on [ID], [CustomerID], [MinimumCoverage], [MaximumCoverage] in table 'Quotes'
ALTER TABLE [dbo].[Quotes]
ADD CONSTRAINT [PK_Quotes]
    PRIMARY KEY CLUSTERED ([ID], [CustomerID], [MinimumCoverage], [MaximumCoverage] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerQuote_Quote_ID] in table 'Quotes'
ALTER TABLE [dbo].[Quotes]
ADD CONSTRAINT [FK_CustomerQuote]
    FOREIGN KEY ([CustomerQuote_Quote_ID])
    REFERENCES [dbo].[Customers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerQuote'
CREATE INDEX [IX_FK_CustomerQuote]
ON [dbo].[Quotes]
    ([CustomerQuote_Quote_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------