create table rm.tCourseTemp
(
	CourseId int identity(0,1),
	CourseName nvarchar(32) not null,
	Price int,
	CollocId int not null,

	constraint PK_rm_tCourse primary key(CourseId),
	constraint FK_rm_tCourse_CollocId foreign key (CollocId) references rm.tColloc(CollocId)
);

insert into rm.tCourseTemp( CourseName, CollocId) 
			   values ( N'', 0);