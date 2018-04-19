using FluentMigrator;

namespace FluentMigratorPlayground
{
    [Migration(1)]
    public sealed class FirstMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Pessoa")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsFixedLengthString(40).NotNullable()
                .WithColumn("BirthDate").AsDateTimeOffset()
                .WithColumn("IsActive").AsBoolean()
                .WithColumn("Salary").AsCurrency();
        }

        public override void Down()
        {
            Delete.Table("Pessoa");
        }
    }
}