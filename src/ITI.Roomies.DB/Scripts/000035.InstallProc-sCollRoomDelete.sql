create procedure rm.sCollRoomDelete
(
    @CollocId int,
	@RoomieId int
)
as
begin
   
    delete from rm.tiCollRoom where CollocId = @CollocId and RoomieId = @RoomieId;
    return 0;
end;