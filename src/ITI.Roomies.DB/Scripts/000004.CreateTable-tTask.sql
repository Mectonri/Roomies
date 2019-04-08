create table rm.tTasks
(
	TaskId int identity(0,1) not null,
	TaskName nvarchar(32) not null,
	TaskDes  nvarchar(200),
	TaskDate datetime2,
	[State] bit default 0,
	CollocId int not null,


	constraint PK_rm_tTasks primary key(TaskId),
	constraint FK_rm_tTasks_CollocId foreign key(CollocId) references rm.tColloc(CollocId)
);
insert into rm.tTasks( TaskName, TaskDate, CollocId)
values( left(convert(nvarchar(36), newid()), 32), '20190101', 0);