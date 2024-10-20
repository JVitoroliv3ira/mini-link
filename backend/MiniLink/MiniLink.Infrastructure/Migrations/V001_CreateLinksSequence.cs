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
    }

    public override void Down()
    {
        Delete.Sequence("SQ_LINKS");
    }
}