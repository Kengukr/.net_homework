using Microsoft.EntityFrameworkCore;
using task_new.Models;

namespace task_new.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> GetBooksByYearAsync(int year)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Where(b => b.Year == year)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllWithAuthorsAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<Book?> GetBookWithAuthorAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}