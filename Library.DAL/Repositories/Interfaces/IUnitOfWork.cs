namespace Library.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Author> Authors { get; }
        IBaseRepository<Category> Categories { get; }
        IBaseRepository<Publisher> Publishers { get; }
        IBookRepository Books { get; }
        IBaseRepository<Member> Members { get; }
        IBaseRepository<Borrowing> Borrowings { get; }
        IBaseRepository<RefreshToken> RefreshTokens { get; }
        IBaseRepository<User> Users { get; }


        Task<int> CompleteAsync();
    }
}