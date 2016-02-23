CREATE TABLE [dbo].[Availibility]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Date] DATE NOT NULL, 
    [StartHour] INT NOT NULL, 
    [EndHour] INT NOT NULL, 
    [UserID] INT NOT NULL,
	constraint fk_User_Availibility foreign key(UserID) references [User](Id)
)
