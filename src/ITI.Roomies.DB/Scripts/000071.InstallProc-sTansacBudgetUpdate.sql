create proc rm.sTBudgetUpdate
(
	@TBudgetId int,
	@Desc nvarchar(200),
	@Price int, 
	@Date datetime2,
	@BudgetId int,
	@RoomieId int
)
as
begin
    set transaction isolation level serializable;
	begin tran;

	if not exists(select * from rm.tTBudget tb where tb.TBudgetId = @TBudgetId)
	begin
		rollback;
		return 1;
	end;

	if exists( select * from rm.tTBudget tb where tb.BudgetId <> @TBudgetId
		and tb.RoomieId = @RoomieId
		and tb.[Date] = @Date
		and tb.BudgetId = @BudgetId )
	begin 
		rollback;
		return 2;
	end;

update rm.tTBudget
set [Desc] = @Desc,
	Price = @Price,
	[Date] = @Date,
	BudgetId = @BudgetId,
	RoomieId = @RoomieId

where TBudgetId = @TBudgetId;
	commit;
    return 0;
end;