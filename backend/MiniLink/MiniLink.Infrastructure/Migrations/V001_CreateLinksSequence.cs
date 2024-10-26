using FluentMigrator;

namespace MiniLink.Infrastructure.Migrations;

[Migration(001)]
public class V001_CreateLinksSequence : Migration
{
    public override void Up()
    {
        Create.Sequence("SQ_LINKS")
            .StartWith(1000)
            .IncrementBy(1)
            .MinValue(1000)
            .MaxValue(long.MaxValue)
            .Cache(1);
        
        Execute.Sql("GRANT USAGE ON SEQUENCE SQ_LINKS TO dminilink;");
        Execute.Sql("GRANT SELECT ON SEQUENCE SQ_LINKS TO dminilink;");
    }

    public override void Down()
    {
        Execute.Sql("REVOKE SELECT ON SEQUENCE SQ_LINKS FROM dminilink;");
        Execute.Sql("REVOKE USAGE ON SEQUENCE SQ_LINKS FROM dminilink;");
        
        Delete.Sequence("SQ_LINKS");
    }
}