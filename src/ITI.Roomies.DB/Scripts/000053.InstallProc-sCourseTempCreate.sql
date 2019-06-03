create procedure rm.sCourseTempCreate
(
	@CourseTempId int out,
	@CourseTempName nvarchar(32),
	@CollocId int
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	insert into rm.tCourseTemp( CourseTempName, CollocId )
					values(@CourseTempName, @CollocId);
	set @CourseTempId = SCOPE_IDENTITY();

	commit;
	return 0;
end;

