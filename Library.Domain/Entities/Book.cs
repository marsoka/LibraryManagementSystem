
public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public int PublicationYear { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public decimal Price { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
    public int PublisherId { get; set; }

    public required Author Author { get; set; }
    public required Category Category { get; set; }
    public required Publisher Publisher { get; set; }
    public List<Borrowing>? Borrowings { get; set; }

}