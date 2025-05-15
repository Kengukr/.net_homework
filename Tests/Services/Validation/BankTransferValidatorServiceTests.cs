using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using task_new.Services.Validation;

namespace task_new.Tests.Services.Validation
{
    [TestClass]
    public class BankTransferValidatorServiceTests
    {
        private Mock<ICurrencyService> _mockCurrencyService = null!;
        private IBankTransferValidatorService _validator = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockCurrencyService = new Mock<ICurrencyService>();
            _validator = new BankTransferValidatorService(_mockCurrencyService.Object);
        }

        [TestMethod]
        public void Validate_ValidTransferString_ReturnsTrue()
        {
            var validTransfer = "GB33BUKB20201555555555;DE89370400440532013000;UKR;100.50";
            _mockCurrencyService.Setup(s => s.IsValidCurrencyCode("UKR")).Returns(true);

            var result = _validator.Validate(validTransfer);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void Validate_InvalidIban_ReturnsFalse()
        {
            var invalidTransfer = "INVALID_IBAN;DE89370400440532013000;USD;100.50";
            _mockCurrencyService.Setup(s => s.IsValidCurrencyCode("USD")).Returns(true);

            var result = _validator.Validate(invalidTransfer);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Validate_InvalidCurrencyCode_ReturnsFalse()
        {
            var invalidTransfer = "GB33BUKB20201555555555;DE89370400440532013000;XXX;100.50";
            _mockCurrencyService.Setup(s => s.IsValidCurrencyCode("XXX")).Returns(false);

            var result = _validator.Validate(invalidTransfer);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Validate_NegativeAmount_ReturnsFalse()
        {
            var invalidTransfer = "GB33BUKB20201555555555;DE89370400440532013000;USD;-100.50";
            _mockCurrencyService.Setup(s => s.IsValidCurrencyCode("USD")).Returns(true);

            var result = _validator.Validate(invalidTransfer);

            Assert.IsFalse(result);
        }
        

        [TestMethod]
        public void Validate_EmptyString_ReturnsFalse()
        {
            var invalidTransfer = "";

            var result = _validator.Validate(invalidTransfer);

            Assert.IsFalse(result);
        }
    }
}
