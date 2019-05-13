create proc rm.sInvitation
(
	@CourseId int,
    @InvitationKey nvarchar(64),
    @IdReceiver int,
    @IdSender int,
    @IdColloc int
)
as
begin
	
    insert into rm.tInvitation (InvitationKey,IdColloc,IdReceiver,IdSender,IdColloc)
    values (@InvitationKey,@IdColloc,@IdReceiver,@IdSender,@IdColloc);
	
end;
