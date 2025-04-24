namespace task_new.Services.LifetimeExamples
{
    public class SingletonService : ISingletonService
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}