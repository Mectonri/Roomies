create procedure rm.sItemCreate
(
	@ItemId		int,
	@CourseId	int,
	@RoomieId	int,
    @ItemName   nvarchar(32),
    @ItemPrice	int
)
as
begin
    set transaction isolation level serializable;
	begin tran;


	insert into rm.tItem(ItemName, ItemPrice, RoomieId, CourseId)
	                  values(@ItemName, @ItemPrice, @RoomieId, @CourseId);
	set @ItemId = scope_identity();
	
	commit;
	return 0;
end;

select * from rm.tItem