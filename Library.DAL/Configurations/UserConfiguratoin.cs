using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .UseIdentityColumn(1, 1);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(20);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(150);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasColumnType("char(60)");

        builder.Property(u => u.Role)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(50);

    }
}