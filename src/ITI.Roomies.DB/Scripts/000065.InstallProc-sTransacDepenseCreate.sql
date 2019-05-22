Create procedure rm.sTrasacDepenseCreate
(	
	@TDepenseId int out,
	@Price int,
	@Date datetime2,
	@SRoomieId int,
	@RRoomieId int
)
as 
begin
	set transaction isolation level serializable;
	begin tran;

	insert into rm.tTransactionDepense(TDepenseId, Price, [Date], SRoomieId, RRoomieId)
	values(@TDepenseId, @Price, @Date, @SRoomieId, @RRoomieId)
	set @TDepenseId = SCOPE_IDENTITY();

	commit;
	return 0;
end;