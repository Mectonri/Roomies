create proc rm.sCheckInvite
(
    @InvitationKey nvarchar(64)
)
as
begin
	select idColloc from rm.tInvitation where InvitationKey = @InvitationKey;
end;
