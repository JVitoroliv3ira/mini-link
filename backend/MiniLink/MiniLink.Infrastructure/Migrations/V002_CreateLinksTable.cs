using FluentMigrator;

namespace MiniLink.Infrastructure.Migrations;

[Migration(002)]
public class V002_CreateLinksTable : Migration
{
    public override void Up()
    {
        Create.Table("TB_LINKS")
            .WithColumn("ID").AsInt32().PrimaryKey("PK_LINK").NotNullable()
            .WithColumn("ORIGINAL_URL").AsString(int.MaxValue).NotNullable()
            .WithColumn("SLUG").AsString(6).NotNullable().Unique("IX_TB_LINKS_SLUG")
            .WithColumn("CREATED_AT").AsDateTime().NotNullable()
            .WithColumn("EXPIRES_AT").AsDate().Nullable();
        
        Execute.Sql("GRANT SELECT, INSERT, UPDATE, DELETE ON TB_LINKS TO dminilink;");
    }

    public override void Down()
    {
        Execute.Sql("REVOKE SELECT, INSERT, UPDATE, DELETE ON TB_LINKS FROM dminilink;");
        Delete.Table("TB_LINKS");
    }
}