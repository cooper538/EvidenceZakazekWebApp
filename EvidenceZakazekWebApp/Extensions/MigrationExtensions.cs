using System;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;

namespace EvidenceZakazekWebApp.Extensions
{
    public static class MigrationExtensions
    {
        public static void DeleteDefaultContraint(this IDbMigration migration, string tableName, string colName, bool suppressTransaction = false)
        {
            var sql = new SqlOperation(String.Format(@"DECLARE @SQL varchar(1000)
            SET @SQL='ALTER TABLE {0} DROP CONSTRAINT ['+(SELECT name
            FROM sys.default_constraints
            WHERE parent_object_id = object_id('{0}')
            AND col_name(parent_object_id, parent_column_id) = '{1}')+']';
            PRINT @SQL;
            EXEC(@SQL);", tableName, colName)) { SuppressTransaction = suppressTransaction };
            migration.AddOperation(sql);
        }

        public static void SetDefaultContraint(this IDbMigration migration, string tableName, string colName, string defaultValue, bool suppressTransaction = false)
        {
            var sql = new SqlOperation(String.Format(@"DECLARE @SQL varchar(1000)
            SET @SQL='ALTER TABLE {0}
            ADD CONSTRAINT DF_{1} 
            DEFAULT {2} 
            FOR {1};';
            PRINT @SQL;
            EXEC(@SQL);", tableName, colName, defaultValue)) { SuppressTransaction = suppressTransaction };
            migration.AddOperation(sql);
        }
    }
}