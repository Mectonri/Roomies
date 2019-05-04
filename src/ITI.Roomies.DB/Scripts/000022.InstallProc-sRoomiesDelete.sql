create proc rm.sRoomiesDelete
(
    @RoomieId int
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if not exists(select * from rm.tUser r where r.UserId = @RoomieId)
	begin
		rollback;
		return 1;
	end;

  
    delete from rm.tUser where UserId = @RoomieId;

	commit;
    return 0;
end;