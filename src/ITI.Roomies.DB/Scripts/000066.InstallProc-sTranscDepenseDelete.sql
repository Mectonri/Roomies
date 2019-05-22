create procedure rm.sTransacDepenseDelete
(
    @TDepenseId int
)
as
begin
   
    delete from rm.tTransactionDepense where TDepenseId = @TDepenseId;
    return 0;
end;