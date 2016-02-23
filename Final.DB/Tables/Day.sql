CREATE TABLE [dbo].[Day]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Date] DATE NOT NULL, 
    [OpeningShift] INT NOT NULL, 
    [MorningShift1] INT NOT NULL, 
    [MorningShift2] INT NOT NULL, 
    [LunchShift1] INT NOT NULL, 
    [LunchShift2] INT NOT NULL, 
    [AfternoonShift1] INT NOT NULL, 
    [AfternoonShift2] INT NOT NULL, 
    [ClosingShift] INT NOT NULL, 
)
