Create procedure rm.sCategoryCreate
(	
	@CategoryId int out,
	@CategoryName nvarchar(32),
	@Icon nvarchar(max),
	@CollocId int
)
as 
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from rm.tCategory c where c.CategoryName = @CategoryName and c.CollocId = @CollocId )
		begin
			rollback;
			return 1;
		end;

	insert into rm.tCategory(CategoryName, Icon, CollocId)
	values(@CategoryName, @Icon, @CollocId)
	set @CategoryId = SCOPE_IDENTITY();

	commit;
	return 0;
end;