using Microsoft.EntityFrameworkCore;
using task_new.Models;

namespace task_new.Data.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Author?> GetAuthorWithBooksAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public override async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();
        }
    }
}