create table rm.tPasswordUser
(
    UserId     int,
    FirstName  nvarchar(32) collate Latin1_General_100_CI_AS NOT NULL,
	LastName   nvarchar(32) collate Latin1_General_100_CI_AS NOT NULL,
    Email  nvarchar(64) collate Latin1_General_CI_AI not null,
	BirthDate Datetime2,
	Phone	  char(10),
	[Description] nvarchar(200),
	RoomiePic nvarchar(45),
    [Password] varbinary(128) not null,

    constraint PK_tPasswordUser primary key(UserId),
    constraint FK_tPasswordUser_UserId foreign key(UserId) references rm.tUser(UserId)
);

insert into rm.tPasswordUser(UserId, FirstName, LastName, Email, BirthDate, Phone, [Password])
                      values(0, left(convert(nvarchar(36), newid()), 32), left(convert(nvarchar(36), newid()), 32), N'', '20190101', '0000000000', convert(varbinary(128), newid()));
