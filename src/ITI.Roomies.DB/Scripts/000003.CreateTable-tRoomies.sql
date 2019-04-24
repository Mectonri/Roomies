create table rm.tRoomies
(
	RoomieId int not null,
	FirstName  nvarchar(32) collate Latin1_General_100_CI_AS not null,
	LastName   nvarchar(32) collate Latin1_General_100_CI_AS not null,
	Email  nvarchar(64) collate Latin1_General_CI_AI not null,
	BirthDate Datetime2 not  null,
	Phone	  char(10) not null,
	[Description] nvarchar(200),
	RoomiePic nvarchar(45), 

	constraint PK_rm_tRoomies primary key(RoomieId),
	constraint CK_rm_tRoomies_Phone check( len(Phone) = 10 ),
);
insert into rm.tRoomies( RoomieId, FirstName, LastName, Email, BirthDate, Phone)
values(0, left(convert(nvarchar(36), newid()), 32), left(convert(nvarchar(36), newid()), 32), N'', '19990101', '0000000000')
