CREATE TABLE [dbo].[UserRole]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    [Active] BIT NOT NULL,
	constraint fk_User_RoleId foreign key(RoleId) references [Role](Id),
	constraint fk_Role_UserId foreign key(UserId) references [User](Id)
)