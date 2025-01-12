using Xunit;

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
            Email = "test@test.com",
            PhoneNumber = "0701234567",
            StreetAddress = "Testgatan 1",
            PostalCode = "12345",
            City = "Teststad"
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