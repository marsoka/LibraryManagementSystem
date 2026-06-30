using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.NotFoundExceptions
{
    public class AuthorNotFoundException : NotFoundException
    {
        public AuthorNotFoundException(int id)
            : base($"Author with id {id} was not found.")
        {
        }
    }
}