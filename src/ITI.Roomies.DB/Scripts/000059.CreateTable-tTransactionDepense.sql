create table rm.tTransactionDepense
(	
	TDepenseId int identity(0,1) not null,
	[Desc] nvarchar(200),
	Price int not null,
	[Date] datetime2,
	SRoomieId int not null, 
	RRoomieId int not null,

	constraint PK_rm_tTransactionDepense primary key (TDepenseId),
	constraint FK_rm_tTransactionDepense_SRoomieId foreign key (SRoomieId) references rm.tRoomie(RoomieId),
	constraint FK_rm_tTransactionDepense_RRoomieId foreign key (RRoomieId) references rm.tRoomie(RoomieId),
);

insert into rm.tTransactionDepense([Desc], Price, [Date], SRoomieId, RRoomieId )
							values(N'',0, '20000101', 0, 0);