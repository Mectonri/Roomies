create view rm.vCategory
as
	select * from rm.tCategory where CategoryId <> 0;