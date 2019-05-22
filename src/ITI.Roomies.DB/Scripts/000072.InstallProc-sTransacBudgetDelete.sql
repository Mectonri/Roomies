create procedure rm.sTransacBudgetDelete
(
    @TBudgetId int
)
as
begin
   
    delete from rm.tTBudget where TBudgetId = @TBudgetId;
    return 0;
end;