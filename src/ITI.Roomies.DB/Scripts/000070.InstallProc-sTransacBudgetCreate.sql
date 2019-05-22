Create procedure rm.sTrasacBudgetCreate
(	
	@TBudgetId int,
	@Price int,
	@Date datetime2,
	@BudgetId int,
	@RoomieId int
)
as 
begin
	set transaction isolation level serializable;
	begin tran;

	insert into rm.tTBudget(TBudgetId, Price, [Date], BudgetId, RoomieId)
					values(@TBudgetId, @Price, @Date, @BudgetId, @RoomieId)
	set @TBudgetId = SCOPE_IDENTITY();

	commit;
	return 0;
end;