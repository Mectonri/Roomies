create proc rm.sCourseTempUpdate
(
	@CourseTempId int, 
	@CourseTempName nvarchar(32),
	@CollocId int

)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if not exists(select * from rm.tCourseTemp ct where ct.CourseTempId = @CourseTempId)
	begin
		rollback;
		return 1;
	end;

	update rm.tCourseTemp
	set CourseTempName = @CourseTempName
	where CourseTempId = @CourseTempId;

	commit;
    return 0;

end;
