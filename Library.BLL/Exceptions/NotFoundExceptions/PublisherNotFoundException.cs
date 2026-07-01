using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.NotFoundExceptions
{
    public class PublisherNotFoundException : NotFoundException
    {
        public PublisherNotFoundException(int id)
            : base($"Publisher with id {id} was not found.")
        {
        }
    }
}