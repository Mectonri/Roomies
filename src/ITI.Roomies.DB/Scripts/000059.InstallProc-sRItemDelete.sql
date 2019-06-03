create procedure rm.sRItemDelete
(
    @RItemId int
)
as
begin
   
    delete from rm.tRItem where RItemId = @RItemId;
    return 0;
end;