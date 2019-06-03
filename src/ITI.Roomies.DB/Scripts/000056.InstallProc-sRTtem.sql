create table rm.tRItem
(
	RItemId int identity(0,1),
	RItemPrice int not null,
	RItemName nvarchar(32) not null,
	CourseTempId int not null,

	constraint PK_tRItem primary key (RItemId),
	constraint FK_rm_tRItem_CourseTempId foreign key (CourseTempId) references rm.tCourseTemp(CourseTempId),
);
insert into rm.RItem( RItemPrice, RItemName, CourseTempId)
	values (0, left(convert(nvarchar(36), newid()), 32), 0);
