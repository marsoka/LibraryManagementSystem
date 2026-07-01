
public class Member
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }
    public DateTime RegistrationDate { get; set; }

    public List<Borrowing>? Borrowings {get; set;}

}