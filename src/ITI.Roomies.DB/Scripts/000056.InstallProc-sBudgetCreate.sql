create procedure rm.sBudgetCreate
(	
	@BudgetId int out,
	@CategoryId int,
	@Date1 datetime2,
	@Date2 datetime2,
	@CollocId int,
	@Amount int
)
as
begin

	set transaction isolation level serializable
	begin tran;

	insert into rm.tBudget( CategoryId, Date1, Amount, CollocId )
				values( @CategoryId, @Date1, @Amount, @CollocId)
	set @BudgetId = SCOPE_IDENTITY();
	commit;
	return 0;

end;