namespace task_new.Services.LifetimeExamples
{
    public class ScopedService : IScopedService
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}