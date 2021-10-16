select SPECIFIC_NAME, ROUTINE_CATALOG
  from InventoryControlSystem.information_schema.routines 
 where routine_type = 'PROCEDURE'
UNION ALL
select SPECIFIC_NAME, ROUTINE_CATALOG
  from Operations.information_schema.routines 
 where routine_type = 'PROCEDURE'
 UNION ALL
select SPECIFIC_NAME, ROUTINE_CATALOG
  from Operations_Development.information_schema.routines 
 where routine_type = 'PROCEDURE'
 UNION ALL
select SPECIFIC_NAME, ROUTINE_CATALOG
  from OPIS_DEV.information_schema.routines 
 where routine_type = 'PROCEDURE'
 UNION ALL
select SPECIFIC_NAME, ROUTINE_CATALOG
  from test.information_schema.routines 
 where routine_type = 'PROCEDURE'