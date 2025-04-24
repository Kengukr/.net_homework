namespace task_new.Services.LifetimeExamples
{
    public class TransientService : ITransientService
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}