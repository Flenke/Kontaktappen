using Xunit;
using Kontaktapp.Models;
using Kontaktapp.Services;

namespace Kontaktapp.Tests
{
    public class ContactValidatorTests
    {
        private readonly ContactValidator _validator = new();

        [Fact]
        public void Validate_ValidContact_ReturnsTrue()
        {
            // Arrange
            var contact = new Contact
            {
                FirstName = "Test",
                LastName = "Testsson",
                Email = "Adam.E@hotmail.com",
                PhoneNumber = "07012233445",
                StreetAddress = "Hamngatan 1",
                PostalCode = "12345",
                City = "Falun"
            };

            // Act
            var (isValid, errors) = _validator.Validate(contact);

            // Assert
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void Validate_InvalidEmail_ReturnsFalse()
        {
            // Arrange
            var contact = new Contact
            {
                FirstName = "Test",
                LastName = "Testsson",
                Email = "invalid-email",
                PhoneNumber = "0701234567",
                StreetAddress = "Testgatan 1",
                PostalCode = "12345",
                City = "Teststad"
            };

            // Act
            var (isValid, errors) = _validator.Validate(contact);

            // Assert
            Assert.False(isValid);
            Assert.Contains(errors, e => e.Contains("e-postadress"));
        }
    }
}