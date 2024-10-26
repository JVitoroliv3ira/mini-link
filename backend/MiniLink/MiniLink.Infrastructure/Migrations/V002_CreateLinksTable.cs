using FluentMigrator;

namespace MiniLink.Infrastructure.Migrations;

[Migration(002)]
public class V002_CreateLinksTable : Migration
{
    public override void Up()
    {
        Create.Table("TB_LINKS")
            .InSchema("public")
            .WithColumn("ID").AsInt32().PrimaryKey("PK_LINK").WithDefaultValue(RawSql.Insert("nextval('\"public\".\"SQ_LINKS\"')"))
            .WithColumn("ORIGINAL_URL").AsCustom("TEXT").NotNullable()
            .WithColumn("SLUG").AsString(6).NotNullable().Unique("IX_TB_LINKS_SLUG")
            .WithColumn("CREATED_AT").AsDateTime().NotNullable()
            .WithColumn("EXPIRES_AT").AsDate().NotNullable();
        
        Execute.Sql("GRANT SELECT, INSERT, UPDATE, DELETE ON \"public\".\"TB_LINKS\" TO dminilink;");
    }

    public override void Down()
    {
        Execute.Sql("REVOKE SELECT, INSERT, UPDATE, DELETE ON \"public\".\"TB_LINKS\" FROM dminilink;");
        Delete.Table("TB_LINKS").InSchema("public");
    }
}