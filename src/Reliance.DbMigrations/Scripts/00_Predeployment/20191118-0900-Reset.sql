--Reset table script to implement new updates for it

Update dbo.SchemaVersions Set ScriptName = ScriptName + ' --Dt '+ Format(GetDate(), 'yyyy-MM-dd HH:mm:ss') + ' --Reset' Where ScriptName = 'Reliance.DbMigrations.Scripts._01_Deployment._01_Schema_Tables.20190906-1001-Tbl-Repository.sql'