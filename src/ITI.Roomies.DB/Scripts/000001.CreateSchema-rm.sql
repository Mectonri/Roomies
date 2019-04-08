if not exists(
	select * from sys.schemas s where s.[name] = 'rm')
		exec('create schema rm;');