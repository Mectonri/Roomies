create table rm.tRoomiePic
(
	PicId int identity(0,1),
	[Owner] int not null,
	[Path] nvarchar(200)

	constraint PK_rm_tRoomiePic primary key(PicId),
	constraint PK_rm_tRoomiePic_Owner foreign key ([Owner]) references rm.tRoomie(RoomieId)
);

insert into rm.tRoomiePic([Owner], [Path])
					values(0, N'' );