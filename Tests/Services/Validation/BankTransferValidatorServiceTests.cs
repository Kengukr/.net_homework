using Microsoft.VisualStudio.TestTools.UnitTesting;
using task_new.Services.Validation;

namespace task_new.Tests.Services.Validation
{
    [TestClass]
    public class BankTransferValidatorServiceTests
    {
        private IBankTransferValidatorService _validator = null!; // Use null-forgiving operator

        [TestInitialize]
        public void Setup()
        {
            _validator = new BankTransferValidatorService();
        }

        [TestMethod]
        public void Validate_ValidTransferString_ReturnsTrue()
        {
            // Arrange
            var validTransfer = "GB33BUKB20201555555555;DE89370400440532013000;USD;100.50";

            // Act
            var result = _validator.Validate(validTransfer);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validate_InvalidIban_ReturnsFalse()
        {
            // Arrange
            var invalidTransfer = "INVALID_IBAN;DE89370400440532013000;USD;100.50";

            // Act
            var result = _validator.Validate(invalidTransfer);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Validate_InvalidCurrencyCode_ReturnsFalse()
        {
            // Arrange
            var invalidTransfer = "GB33BUKB20201555555555;DE89370400440532013000;XXX;100.50";

            // Act
            var result = _validator.Validate(invalidTransfer);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Validate_NegativeAmount_ReturnsFalse()
        {
            // Arrange
            var invalidTransfer = "GB33BUKB20201555555555;DE89370400440532013000;USD;-100.50";

            // Act
            var result = _validator.Validate(invalidTransfer);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Validate_EmptyString_ReturnsFalse()
        {
            // Arrange
            var invalidTransfer = "";

            // Act
            var result = _validator.Validate(invalidTransfer);

            // Assert
            Assert.IsFalse(result);
        }
    }
}