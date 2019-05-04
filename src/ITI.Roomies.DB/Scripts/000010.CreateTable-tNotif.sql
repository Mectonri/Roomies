create table rm.tNotifs
(
	NotifId int identity(0,1),
	UserId int not null,
	[Message] nvarchar(max) not null,
	Seen bit default 0,

	constraint Pk_rm_tNotif primary key(NotifId),
	constraint FK_rm_tNotif_UserId foreign key(UserId) references rm.tUser(UserId)
);
	insert into rm.tNotifs(UserId, Message) 
					values(0,      N''); 