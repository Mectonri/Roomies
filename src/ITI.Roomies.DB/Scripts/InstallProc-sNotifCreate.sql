create procedure rm.sNotifCreate
(
	@NotifId int out,
	@UserId int ,
	@Message nvarchar(max)
)
as
begin
	set transaction isolation level serializable;
	begin tran;
	insert into rm.tNotif(UserId, [Message]) 
				   values(@UserId, @Message)
	set @NotifId = SCOPE_IDENTITY();
	
	commit;
	return 0;
end;