using System.Globalization;
using System.Text.RegularExpressions;

namespace task_new.Services.Validation
{
    public interface IBankTransferValidatorService
    {
        bool Validate(string transferString);
    }

    public class BankTransferValidatorService : IBankTransferValidatorService
    {
        private static readonly Regex IbanRegex = new(@"^[A-Z]{2}\d{2}[A-Z0-9]{1,30}$", RegexOptions.Compiled);
        private static readonly HashSet<string> Iso4217CurrencyCodes = new(CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Where(c => !c.IsNeutralCulture)
                .Select(c => new RegionInfo(c.Name).ISOCurrencySymbol)
                .Distinct());


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

            if (!ValidateIban(ibanFrom))
                return false;

            if (!ValidateIban(ibanTo))
                return false;

            if (!ValidateCurrencyCode(currencyCode))
                return false;

            if (!ValidateAmount(amountString))
                return false;

            return true;
        }


        private bool ValidateIban(string iban)
        {
            return IbanRegex.IsMatch(iban);
        }

        private bool ValidateCurrencyCode(string currencyCode)
        {
            return Iso4217CurrencyCodes.Contains(currencyCode);
        }

        private bool ValidateAmount(string amountString)
        {
            return decimal.TryParse(amountString, NumberStyles.Number, CultureInfo.InvariantCulture, out var amount) && amount > 0;
        }

    }
}
