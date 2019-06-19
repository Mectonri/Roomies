create proc rm.sUpdateItemBought
(
	@ItemId	 int,
	@ItemBought	bit
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if not exists(select * from rm.tItem t where t.ItemId = @ItemId)
	begin
		rollback;
		return 1;
	end;

	update rm.tItem
	set ItemBought = @ItemBought
	where ItemId = @ItemId;

	commit;
    return 0;
end;
