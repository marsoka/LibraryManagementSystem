

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.ToTable("Publishers");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
        .UseIdentityColumn(1, 1);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(150);

        builder.Property(p => p.Address)
            .HasColumnType("varchar")
            .HasMaxLength(250);

        builder.Property(p => p.Phone)
            .HasColumnType("varchar")
            .HasMaxLength(20);
    }

}