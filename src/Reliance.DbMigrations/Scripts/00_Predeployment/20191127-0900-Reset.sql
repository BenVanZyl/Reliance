--Reset table script to implement new updates for it

Update dbo.SchemaVersions Set ScriptName = ScriptName + ' --Dt '+ Format(GetDate(), 'yyyy-MM-dd HH:mm:ss') + ' --Reset' Where ScriptName like '%Tbl-RepositoryOwner.sql'
Update dbo.SchemaVersions Set ScriptName = ScriptName + ' --Dt '+ Format(GetDate(), 'yyyy-MM-dd HH:mm:ss') + ' --Reset' Where ScriptName like '%Tbl-Repository.sql'