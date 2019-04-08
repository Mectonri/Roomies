create table rm.tTransaction
(
	TransacId int  identity(0,1),
	TransacDesc nvarchar(32),
	TransacPrice int not null,
	TransacDate datetime2 default getdate(),
	CollocId int not null,
	RoomieId int not null,

	constraint PK_rm_tTrasnsaction primary key (TransacId), 
	constraint FK_rm_tTransaction_CollocId foreign key (CollocId,RoomieId) references rm.tiCollRoom(CollocId,RoomieId)
	--constraint FK_rm_tTransaction_RoomieId foreign key () references rm.tiCollRoom(),
);

insert into rm.tTransaction( TransacPrice, CollocId, RoomieId) values ( 0,0,0); 