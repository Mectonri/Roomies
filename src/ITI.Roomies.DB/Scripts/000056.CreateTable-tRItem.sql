create table rm.tRItem
(
	RItemId int identity(0,1),
	RItemPrice int not null,
	RItemName nvarchar(32) not null,
	CollocId int not null,
	CourseTempId int not null,

	constraint PK_rm_RItem primary key (RItemId),
	constraint FK_rm_RItem_CollocId foreign key (CollocId) references rm.tColloc(CollocId),
	constraint FK_rm_RItem_CourseTempId foreign key (CourseTempId) references rm.tCourseTemp(CourseTempId)
);
insert into rm.tRItem( RItemPrice, RItemName, CollocId, CourseTempId)
	values (0, left(convert(nvarchar(36), newid()), 32), 0, 0);
