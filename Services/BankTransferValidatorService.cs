using System.Globalization;
using System.Text.RegularExpressions;

namespace task_new.Services.Validation
{
    public class BankTransferValidatorService : IBankTransferValidatorService
    {
        private readonly ICurrencyService _currencyService;
        private static readonly Regex IbanFormatRegex = new(@"^[A-Z]{2}\d{2}[A-Z0-9]{11,30}$", RegexOptions.Compiled);

        public BankTransferValidatorService(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public bool Validate(string transferString)
        {
            if (string.IsNullOrWhiteSpace(transferString))
                return false;

            var parts = transferString.Split(';');
            if (parts.Length != 4)
                return false;

            var ibanFrom = parts[0].Trim();
            var ibanTo = parts[1].Trim();
            var currencyCode = parts[2].Trim();
            var amountString = parts[3].Trim();

            if (!ValidateIban(ibanFrom) || !ValidateIban(ibanTo))
                return false;

            if (!_currencyService.IsValidCurrencyCode(currencyCode))
                return false;

            if (!ValidateAmount(amountString))
                return false;

            return true;
        }
        

        private bool ValidateIban(string iban)
        {
            return IbanFormatRegex.IsMatch(iban);
        }

        private bool ValidateAmount(string amountString)
        {
            return decimal.TryParse(amountString, NumberStyles.Number, CultureInfo.InvariantCulture, out var amount) && amount > 0;
        }
    }
}
