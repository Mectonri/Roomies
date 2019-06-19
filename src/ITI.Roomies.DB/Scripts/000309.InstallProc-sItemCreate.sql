create procedure rm.sItemCreate
(
	@CourseId	int,
	@RoomieId	int,
    @ItemName   nvarchar(32),
    @ItemQuantite   nvarchar(32),
    @ItemPrice	int
)
as
begin
    set transaction isolation level serializable;
	begin tran;

	insert into rm.tItem(ItemName, ItemPrice, ItemQuantite, RoomieId, CourseId)
	                  values(@ItemName, @ItemPrice, @ItemQuantite, @RoomieId, @CourseId);
	
	commit;
	return 0;
end;
