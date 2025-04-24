using task_new.Models;

namespace task_new.Data.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByYearAsync(int year);
        Task<IEnumerable<Book>> GetAllWithAuthorsAsync();
        Task<Book?> GetBookWithAuthorAsync(int id);
    }
}