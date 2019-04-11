create procedure rm.sTasksCreate
(
	@TaskId int,
    @TaskName   nvarchar(32),
    @TaskDes	nvarchar(200),
	@TaskDate	datetime2
)
as
begin
    set transaction isolation level serializable;
	begin tran;


	insert into rm.tTasks (TaskName, TaskDes, TaskDate)
	                  values(@TaskName, @TaskDes, @TaskDate);
	set @TaskId = scope_identity();
	
	commit;
	return 0;
end;