DECLARE @SP_NAME VARCHAR(100)
DECLARE @DTS VARCHAR(100)
SET @SP_NAME = '%perl%'

------Text search for objects in PL SQL---------
SELECT distinct o.[name], c.text
FROM InventoryControlSystem.dbo.sysobjects o (NOLOCK) JOIN InventoryControlSystem.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME
UNION ALL
SELECT distinct o.[name], c.text
FROM Operations.dbo.sysobjects o (NOLOCK) JOIN Operations.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME
UNION ALL
SELECT distinct o.[name], c.text
FROM Operations_Development.dbo.sysobjects o (NOLOCK) JOIN Operations_Development.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME
UNION ALL
SELECT distinct o.[name], c.text
FROM OPIS_DEV.dbo.sysobjects o (NOLOCK) JOIN OPIS_DEV.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME
UNION ALL
SELECT distinct o.[name], c.text
FROM test.dbo.sysobjects o (NOLOCK) JOIN test.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME

----------Text search in SQL Jobs:-------------------

SELECT      j.job_id,   j.originating_server,
      j.name,     js.step_id, js.command, j.enabled 
FROM  MSDB.dbo.sysjobs j JOIN MSDB.dbo.sysjobsteps js ON    js.job_id = j.job_id 
WHERE js.command LIKE @SP_NAME
