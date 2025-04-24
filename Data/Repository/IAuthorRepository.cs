using task_new.Models;

namespace task_new.Data.Repository
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author?> GetAuthorWithBooksAsync(int id);
    }
}