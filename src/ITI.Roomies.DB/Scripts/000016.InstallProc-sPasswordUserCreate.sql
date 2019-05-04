create procedure rm.sPasswordUserCreate
(   
    @FirstName nvarchar(32),
    @LastName  nvarchar(32),
    @Email     nvarchar(64),
    @BirthDate datetime2,
    @Phone     char(10),
    @Password  varbinary(128),
	@UserId    int out
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from rm.tUser u where u.Email = @Email)
	begin
		rollback;
		return 1;
	end;

    insert into rm.tUser(FirstName, LastName, Email, BirthDate, Phone) 
				  values(@FirstName, @LastName, @Email, @BirthDate, @Phone);
    select @UserId = scope_identity();
    insert into rm.tPasswordUser(UserId,  [Password])
                          values(@UserId, @Password);
	commit;
    return 0;
end;
