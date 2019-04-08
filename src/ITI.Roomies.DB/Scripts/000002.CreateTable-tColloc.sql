create table rm.tColloc
(
	CollocId int identity(0,1),
	CollocName nvarchar(32) not null,
	CreationDate datetime2 not null,
	CollocPic nvarchar(45),

	constraint PK_rm_tColloc primary key( CollocId ),
	constraint CK_rm_tColloc_CollocName check( CollocName <> N'') 
);
insert into rm.tColloc( CollocName, CreationDate) 
values                ( left(convert(nvarchar(36), newid()), 32),'19910101');	
