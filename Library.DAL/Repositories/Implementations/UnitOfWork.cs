using Library.DAL.Repositories.Interfaces;

namespace Library.DAL.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<Author> Authors { get; }
        public IBaseRepository<Category> Categories { get; }
        public IBaseRepository<Publisher> Publishers { get; }
        public IBookRepository Books { get; }
        public IBaseRepository<Borrowing> Borrowings { get; }
        public IBaseRepository<Member> Members { get; }

        public IBaseRepository<RefreshToken> RefreshTokens { get; }

        public IBaseRepository<User> Users { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Authors = new BaseRepository<Author>(_context);
            Categories = new BaseRepository<Category>(_context);
            Publishers = new BaseRepository<Publisher>(_context);
            Books = new BookRepository(_context);
            Borrowings = new BaseRepository<Borrowing>(_context);
            Members = new BaseRepository<Member>(_context);
            RefreshTokens = new BaseRepository<RefreshToken>(_context);
            Users = new BaseRepository<User>(_context);
        }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}