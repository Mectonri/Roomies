create procedure rm.sCollocCreate
(
    @CollocName   nvarchar(32),
	@CollocId	  int out 
)
as
begin
    set transaction isolation level serializable;
	begin tran;
	
	insert into rm.tColloc  ( CollocName )
	                  values( @CollocName )
	set @CollocId = scope_identity();
	
	commit;
	return 0;
end;