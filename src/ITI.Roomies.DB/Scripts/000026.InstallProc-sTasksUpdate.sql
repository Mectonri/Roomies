create proc rm.sTasksUpdate
(
	@TaskId	 int, 
	@TaskName   nvarchar(32),
    @TaskDes	nvarchar(200) = null,
	@TaskDate	datetime2,
	@State		bit,
	@CollocId int
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if not exists(select * from rm.tTasks t where t.TaskId = @TaskId)
	begin
		rollback;
		return 1;
	end;

	if exists(select * from rm.tTasks t where t.TaskId <> @TaskId)
	begin
		rollback;
		return 2;
	end;

	update rm.tTasks
	set TaskName = @TaskName, TaskDate = @TaskDate, TaskDes = @TaskDes, [State] = @State, CollocId = @CollocId
	where TaskId = @TaskId;

	commit;
    return 0;
end;