
namespace task_new.Services.Validation

{
    public interface IBankTransferValidatorService
    {
        bool Validate(string transferString);
    }
}
