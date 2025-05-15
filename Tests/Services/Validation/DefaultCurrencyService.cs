using System.Globalization;

namespace task_new.Services.Validation
{
    public class DefaultCurrencyService : ICurrencyService
    {
        

        public bool IsValidCurrencyCode(string currencyCode)
        {
            return currencyCode == "USD";
        }
    }
}
