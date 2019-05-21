create proc rm.sCourseTempDelete
(
	@CourseId int
)
as
begin
	delete from rm.tCourseTemp where CourseId = @CourseId;
	return 0;
	
end;