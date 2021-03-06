create procedure rm.sCollocCreate
(
    @CollocName   nvarchar(32),
	@RoomieId int,
	@CollocId	  int out 
)
as
begin
    set transaction isolation level serializable;
	begin tran;
	
	insert into rm.tColloc  ( CollocName )
	                  values( @CollocName )
	set @CollocId = scope_identity();
	insert into rm.tiCollRoom(collocId, RoomieId, AdminColloc)
		values(@CollocId, @RoomieId, 1);

	commit;
	return 0;
end;
