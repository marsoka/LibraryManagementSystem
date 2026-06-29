using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .UseIdentityColumn(1, 1);

        builder.Property(a => a.FullName)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(150);

        builder.Property(a => a.Biography)
            .IsRequired()
            .HasColumnType("varchar(max)");

        builder.Property(a => a.DateOfBirth)
            .HasColumnType("DATE");

        builder.Property(a => a.Nationality)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(100);
    }
}