using FluentMigrator.Builders.Create.Table;

namespace AdocaoPB.Infrastructure.Migrations;

public static class VersionBase
{

    public static ICreateTableColumnOptionOrWithColumnSyntax InserirColunasPadrao(
        ICreateTableWithColumnOrSchemaOrDescriptionSyntax tabela
    )
    {

        return tabela
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("CreationDate").AsDateTime().NotNullable();

    }

}
