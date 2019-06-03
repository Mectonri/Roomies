create procedure rm.sCourseTempCreate
(
	@CourseTempId int out,
	@CourseName nvarchar(32),
	@CollocId int
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	insert into rm.tCourseTemp( CourseName, CollocId )
					values(@CourseName, @CollocId);
	set @CourseTempId = SCOPE_IDENTITY();

	commit;
	return 0;
end;

