create proc rm.sDeleteInvite
(
    @InvitationKey nvarchar(64)
)
as
begin
	
    Delete from rm.tInvitation where InvitationKey = @InvitationKey;
	
end;
