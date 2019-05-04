create table rm.tUser
(
	UserId int identity(0, 1),
	FirstName  nvarchar(32) collate Latin1_General_100_CI_AS NOT NULL,
	LastName   nvarchar(32) collate Latin1_General_100_CI_AS NOT NULL,
    Email  nvarchar(64) collate Latin1_General_CI_AI not null,
	BirthDate Datetime2,
	Phone	  char(10),
	[Description] nvarchar(200),
	RoomiePic nvarchar(45),

    constraint PK_tUser primary key(UserId),
    constraint UK_tUser_Email unique(Email),
    constraint UK_tUser_Phone unique(Phone),
);

insert into rm.tUser(FirstName,                                LastName,                                 Email, BirthDate, Phone )
              values(left(convert(nvarchar(36), newid()), 32), left(convert(nvarchar(36), newid()), 32), N'',   '20190101','0000000000');