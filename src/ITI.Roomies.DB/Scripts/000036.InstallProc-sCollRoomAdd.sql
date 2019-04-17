create Procedure rm.sCollRoomAdd
(
	@CollocId int out,
	@RoomieId int out
)
as 
begin
	set transaction isolation level serializable;
	begin tran;


	if exists(select * from rm.tiCollRoom c where c.CollocId = @CollocId and c.RoomieId = @RoomieId)
	begin
		rollback;
		return 1;
	end;

	insert into rm.tiCollRoom(CollocId, RoomieId)
	                  values(@CollocId, @RoomieId );
	commit;
	return 0;
end;

	