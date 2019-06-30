create procedure rm.sItemCreate
(
	@CollocId	int,
	@ItemName   nvarchar(32),
    @ItemPrice	int,
    @ItemSaved  bit
)
as
begin
    set transaction isolation level serializable;
	begin tran;

	insert into rm.tItem(ItemName, ItemPrice, CollocId, ItemSaved)
	                  values(@ItemName, @ItemPrice, @CollocId, @ItemSaved);
	
	commit;
	return 0;
end;
