namespace task_new.Services.Validation
{
    public interface ICurrencyService
    {
        bool IsValidCurrencyCode(string currencyCode);
    }
}