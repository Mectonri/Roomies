create proc rm.sCourseUpdate
(
	@CourseId int, 
	@CourseName nvarchar(32),
	@CourseDate datetime2,
	@CoursePrice int,
	@CollocId int
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if not exists(select * from rm.tCourse c where c.CourseId = @CourseId)
	begin
		rollback;
		return 1;
	end;

	if exists(select * from rm.tCourse c where c.CourseId <> @CourseId)
	begin
		rollback;
		return 2;
	end;

	update rm.tCourse
	set CourseName = @CourseName,
		CourseDate = @CourseDate,
		@CoursePrice = @CoursePrice,
		CollocId = @CollocId
	where CourseId = @CourseId;

	commit;
    return 0;


end;