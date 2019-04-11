create procedure rm.sColocCreate
(
    @CollocName   nvarchar(45),
    @CollocPic	  nvarchar(32)
)
as
begin
    set transaction isolation level serializable;
	begin tran;


	insert into rm.tColloc (CollocName, CollocPic)
	                  values(@CollocName, @CollocPic);
	set @collocId = scope_identity();
	
	commit;
	return 0;
end;