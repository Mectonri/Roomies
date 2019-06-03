create procedure rm.sRItemCreate
(
	@RItemPrice int,
	@RItemName nvarchar(32),
	@CollocId int, 
	@CourseTempId int
)
as begin 
	set transaction isolation level serializable;
	begin tran;

	insert into rm.tRItem(RItemPrice, RItemName, CollocId, CourseTempId)
					values(@RItemPrice, @RItemName, @CollocId, @CourseTempId);

	commit;
	return 0;
end;