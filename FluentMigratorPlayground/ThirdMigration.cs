using FluentMigrator;

namespace FluentMigratorPlayground
{
    [Migration(3)]
    public sealed class ThirdMigration : Migration
    {
        public override void Up()
        {
            Alter.Table("Pessoa")
                .AlterColumn("Name").AsFixedLengthAnsiString(60).Nullable();
        }

        public override void Down()
        {
            Alter.Table("Pessoa")
                .AlterColumn("Name").AsFixedLengthString(60);
        }
    }
}