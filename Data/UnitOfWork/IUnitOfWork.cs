using task_new.Data.Repository;

namespace task_new.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        Task<int> SaveAsync();
    }
}