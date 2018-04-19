using FluentMigrator;

namespace FluentMigratorPlayground
{
    [Migration(2)]
    public sealed class SecondMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Carro")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("Model").AsFixedLengthString(60)
                .WithColumn("Manufacturer").AsFixedLengthString(40)
                .WithColumn("Value").AsCurrency();

            Alter.Table("Pessoa")
                .AddColumn("CarroId").AsInt64().ForeignKey("FK_Carro_Id_Pessoa_CarroId", "Carro", "Id")
                .AlterColumn("Name").AsFixedLengthString(60);
        }

        public override void Down()
        {
            Delete.Table("Carro");

            Delete.Column("CarroId").FromTable("Pessoa");
            Alter.Table("Pessoa")
                .AlterColumn("Name").AsFixedLengthString(40);
        }
    }
}