create procedure rm.sCourseCreate
(
	@CourseId int out,
	@CourseName nvarchar(32) ,
	@CourseDate datetime2 ,
	@CoursePrice int ,
	@CollocId int 
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	insert into rm.tCourse( CourseName, CourseDate, CoursePrice, CollocId )
					values(@CourseName, @CourseDate, @CoursePrice, @CollocId);
	set @CourseId = SCOPE_IDENTITY();

	commit;
	return 0;
end;

