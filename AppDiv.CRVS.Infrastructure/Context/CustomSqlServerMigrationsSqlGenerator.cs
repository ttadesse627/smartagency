
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Infrastructure.Context
{
    public class CustomSqlServerMigrationsSqlGenerator : SqlServerMigrationsSqlGenerator
    {
        internal const string DatabaseCollationName = "Latin1_General_100_CI_AS";

        public CustomSqlServerMigrationsSqlGenerator(
            MigrationsSqlGeneratorDependencies dependencies,
           IRelationalAnnotationProvider migrationsAnnotations
        ) : base(dependencies, migrationsAnnotations)
        {

        }

        protected override void Generate(SqlServerCreateDatabaseOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            try
            {
                base.Generate(operation, model, builder);

                if (!Directory.Exists(@"D:\Databases\CRVSFiles"))
                {
                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(
                        new FileSystemAccessRule(
                            "Everyone",
                            FileSystemRights.FullControl,
                            AccessControlType.Allow)
                    );

                    Directory.CreateDirectory(@"D:\Databases\CRVSFiles");
                    DirectoryInfo directoryInfo = new DirectoryInfo(@"D:\Databases\CRVSFiles");
                    directoryInfo.SetAccessControl(securityRules);
                }

                if (DatabaseCollationName != null)
                {
                    builder
                        .Append("ALTER DATABASE ")
                        .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                        .Append(" COLLATE ")
                        .Append(DatabaseCollationName)
                        .AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator)
                        .EndCommand(suppressTransaction: true);

                    builder
                        .AppendLine("EXEC sp_configure filestream_access_level, 2")
                        .AppendLine("RECONFIGURE")
                        .EndCommand(suppressTransaction: true);

                    builder
                        .Append("ALTER DATABASE ")
                        .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                        .AppendLine(" ADD FILEGROUP [AppDiv.CRVSFG] CONTAINS FILESTREAM")
                        .EndCommand(suppressTransaction: true);

                    builder
                        .Append("ALTER DATABASE ")
                        .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                        .AppendLine(" SET FILESTREAM(NON_TRANSACTED_ACCESS = FULL, DIRECTORY_NAME = N'CRVSFSDirectory' ) WITH NO_WAIT")
                        .EndCommand(suppressTransaction: true);

                    builder
                        .Append("ALTER DATABASE ")
                        .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                        .Append(" ADD FILE (")
                        .Append("NAME = N'CRVSFiles',")
                        .Append(@"FILENAME = 'D:\Databases\CRVSFiles\CRVSDB_Data.ldf'")
                        .AppendLine(") TO FILEGROUP [CRVSFG]")
                        .EndCommand(suppressTransaction: true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
