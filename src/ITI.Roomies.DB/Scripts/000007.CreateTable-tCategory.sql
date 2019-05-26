create table rm.tCategory
(
	CategoryId int identity(0,1),
	CategoryName nvarchar(32) not null,
	Icon nvarchar(max),
	CollocId int not null,

	constraint PK_rm_tCategory primary key(CategoryId),
	constraint FK_rm_tCategory_CollocId foreign key (CollocId) references rm.tColloc(CollocId)
);

insert into rm.tCategory( CategoryName, Icon, CollocId) 
				 values ( N'', N'', 0);
