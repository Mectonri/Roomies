create proc rm.sCourseTempUpdate
(
	@CourseId int, 
	@CourseName nvarchar(32),
	@CollocId int

)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if not exists(select * from rm.tCourseTemp c where c.CourseId = @CourseId)
	begin
		rollback;
		return 1;
	end;

	update rm.tCourseTemp
	set CourseName = @CourseName
	where CourseId = @CourseId;

	commit;
    return 0;

end;
