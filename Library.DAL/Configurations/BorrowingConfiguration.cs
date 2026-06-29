
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BorrowingConfiguration : IEntityTypeConfiguration<Borrowing>
{
    public void Configure(EntityTypeBuilder<Borrowing> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .UseIdentityColumn(1, 1);

        builder.Property(b => b.BorrowDate)
            .IsRequired()
            .HasColumnType("DATETIME2");

        builder.Property(b => b.DueDate)
            .IsRequired()
            .HasColumnType("DATETIME2");

        builder.Property(b => b.ReturnDate)
            .HasColumnType("DATETIME2");

        builder.Property(b => b.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        // Relationships
        builder.HasOne(b => b.Member)
            .WithMany(m => m.Borrowings)
            .HasForeignKey(b => b.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Book)
            .WithMany(m => m.Borrowings)
            .HasForeignKey(b => b.BookId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}