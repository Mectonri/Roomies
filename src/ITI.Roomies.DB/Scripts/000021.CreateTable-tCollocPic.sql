create table rm.tCollocPic
(
	PicId int identity(0,1),
	[Owner] int not null,
	[Path] nvarchar(200)

	constraint PK_rm_tCollocPic primary key(PicId),
	constraint PK_rm_tCollocPic_Owner foreign key ([Owner]) references rm.tColloc(CollocId)
);

insert into rm.tCollocPic([Owner], [Path])
					values(0, N'' );