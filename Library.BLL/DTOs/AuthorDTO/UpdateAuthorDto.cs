
public class UpdateAuthorDto
{
    public required string FullName { get; set; }
    public required string Biography { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public required string Nationality { get; set; }
}