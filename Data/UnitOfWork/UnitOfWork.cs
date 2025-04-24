using task_new.Data.Repository;

namespace task_new.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IAuthorRepository? _authorRepository;
        private IBookRepository? _bookRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAuthorRepository Authors => _authorRepository ??= new AuthorRepository(_context);
        public IBookRepository Books => _bookRepository ??= new BookRepository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}