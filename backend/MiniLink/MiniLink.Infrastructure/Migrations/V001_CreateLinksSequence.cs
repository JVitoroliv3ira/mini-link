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
            .MaxValue(long.MaxValue);
        
        Execute.Sql("GRANT USAGE, SELECT ON SEQUENCE \"public\".\"SQ_LINKS\" TO dminilink;");
    }

    public override void Down()
    {
        Execute.Sql("REVOKE USAGE, SELECT ON SEQUENCE \"public\".\"SQ_LINKS\" FROM dminilink;");
        
        Delete.Sequence("SQ_LINKS");
    }
}