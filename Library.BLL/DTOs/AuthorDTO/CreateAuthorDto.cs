
public class CreateAuthorDto
{
    public string FullName { get; set; }
    public string Biography { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string Nationality { get; set; }
}