
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");
        
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .UseIdentityColumn(1,1);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(200);

        builder.Property(b => b.ISBN)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(17);

        builder.Property(b => b.PublicationYear)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(b => b.TotalCopies)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(b => b.AvailableCopies)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(b => b.Price)
            .IsRequired()
            .HasPrecision(18, 2);


        // Relationships
        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId)
            .OnDelete(DeleteBehavior.Restrict);

        
    }
}