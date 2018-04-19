using System.Data.SqlClient;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SqlServer;

namespace FluentMigratorPlayground
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var csb = new SqlConnectionStringBuilder
            {
                DataSource = @".",
                IntegratedSecurity = true
            };

            CreateDatabaseIfDoestNotExists(csb, "FluentMigrationDataBase");

            MigrateDatabaseToLastestVersion(csb);
        }

        private static void MigrateDatabaseToLastestVersion(SqlConnectionStringBuilder csb)
        {
            var announcer = new ConsoleAnnouncer { ShowSql = true };
            var options = new ProcessorOptions();
            var processorFactory = new SqlServer2016ProcessorFactory();
            var processor = processorFactory.Create(csb.ConnectionString, announcer, options);
            var context = new RunnerContext(announcer) { AllowBreakingChange = true };

            var runner = new MigrationRunner(typeof(FirstMigration).Assembly, context, processor);
            runner.MigrateUp();
        }

        private static void CreateDatabaseIfDoestNotExists(SqlConnectionStringBuilder csb, string databaseName)
        {
            csb.InitialCatalog = "master";
            using (var connection = new SqlConnection(csb.ConnectionString))
            using (var sqlCommand = connection.CreateCommand())
            {
                connection.Open();
                sqlCommand.CommandText = $"IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE Name = \'{databaseName}\') CREATE DATABASE [{databaseName}]";
                sqlCommand.ExecuteNonQuery();
            }

            csb.InitialCatalog = databaseName;
        }
    }
}