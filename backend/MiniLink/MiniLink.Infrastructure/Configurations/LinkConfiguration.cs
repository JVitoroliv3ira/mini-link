using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniLink.Domain.Models;

namespace MiniLink.Infrastructure.Configurations;

public class LinkConfiguration : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.ToTable("TB_LINKS");

        builder.HasKey(e => e.Id).HasName("PK_LINK");

        builder.Property(e => e.Id)
            .HasColumnName("ID")
            .HasColumnType("int")
            .HasDefaultValueSql("NEXT VALUE FOR SQ_LINKS");

        builder.Property(e => e.OriginalUrl)
            .IsRequired()
            .HasColumnName("ORIGINAL_URL")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.Slug)
            .IsRequired()
            .HasMaxLength(6)
            .HasColumnName("SLUG")
            .HasColumnType("nvarchar(6)");

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasColumnName("CREATED_AT")
            .HasColumnType("datetime");

        builder.Property(e => e.ExpiresAt)
            .HasColumnName("EXPIRES_AT")
            .HasColumnType("date");

        builder.HasIndex(e => e.Slug)
            .IsUnique()
            .HasDatabaseName("IX_TB_LINKS_SLUG");
    }
}