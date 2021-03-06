﻿/*
Deployment script for Capstone

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Capstone"
:setvar DefaultFilePrefix "Capstone"
:setvar DefaultDataPath "C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column [dbo].[Day].[ShiftId] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Day])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Altering [dbo].[Day]...';


GO
ALTER TABLE [dbo].[Day] DROP COLUMN [ShiftId];


GO
PRINT N'Creating [dbo].[DayShift]...';


GO
CREATE TABLE [dbo].[DayShift] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [DayId]   INT NOT NULL,
    [ShiftId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[fk_Day_ShiftId]...';


GO
ALTER TABLE [dbo].[DayShift] WITH NOCHECK
    ADD CONSTRAINT [fk_Day_ShiftId] FOREIGN KEY ([DayId]) REFERENCES [dbo].[Day] ([Id]);


GO
PRINT N'Creating [dbo].[fk_Shift_DayId]...';


GO
ALTER TABLE [dbo].[DayShift] WITH NOCHECK
    ADD CONSTRAINT [fk_Shift_DayId] FOREIGN KEY ([ShiftId]) REFERENCES [dbo].[Shift] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[DayShift] WITH CHECK CHECK CONSTRAINT [fk_Day_ShiftId];

ALTER TABLE [dbo].[DayShift] WITH CHECK CHECK CONSTRAINT [fk_Shift_DayId];


GO
PRINT N'Update complete.';


GO
