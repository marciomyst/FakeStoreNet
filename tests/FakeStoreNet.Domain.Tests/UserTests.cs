using FakeStoreNet.Domain.Entities;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;

namespace FakeStoreNet.Domain.Tests
{
    public class UserTests
    {
        private readonly Name ValidName = new("Jane", "Doe");
        private readonly Address ValidAddress = new("Street", "1", "City", "00000", new Geolocation("0", "0"));

        [Fact(DisplayName = "Given valid parameters, when creating User, then properties are assigned")]
        public void GivenValidParameters_WhenCreatingUser_ThenPropertiesAreAssigned()
        {
            // Given
            var username = "jane";
            var email = "jane@example.com";
            var passwordHash = "hashed";

            // When
            var user = new User(username, email, passwordHash, ValidName, ValidAddress);

            // Then
            user.Username.ShouldBe(username);
            user.Email.ShouldBe(email);
            user.PasswordHash.ShouldBe(passwordHash);
            user.Name.ShouldBe(ValidName);
            user.Address.ShouldBe(ValidAddress);
        }

        [Theory(DisplayName = "Given invalid parameters, when creating User, then DomainValidationException is thrown")]
        [InlineData("", "email@example.com", "hash", "Username is required")]
        [InlineData("user", "", "hash", "Email is required")]
        [InlineData("user", "email@example.com", "", "PasswordHash is required")]
        public void GivenInvalidParameters_WhenCreatingUser_ThenDomainValidationExceptionIsThrown(string username, string email, string passwordHash, string expected)
        {
            Should.Throw<DomainValidationException>(() =>
                new User(username, email, passwordHash, ValidName, ValidAddress))
                  .Message.ShouldBe(expected);
        }

        [Fact(DisplayName = "Given null name, when creating User, then DomainValidationException is thrown")]
        public void GivenNullName_WhenCreatingUser_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() =>
                new User("user", "e@e.com", "h", null!, ValidAddress))
                  .Message.ShouldBe("Name is required");
        }

        [Fact(DisplayName = "Given null address, when creating User, then DomainValidationException is thrown")]
        public void GivenNullAddress_WhenCreatingUser_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() =>
                new User("user", "e@e.com", "h", ValidName, null!))
                  .Message.ShouldBe("Address is required");
        }

        [Fact(DisplayName = "Given new valid address, when updating address, then Address is updated")]
        public void GivenValidAddress_WhenUpdatingAddress_ThenAddressIsUpdated()
        {
            // Given
            var user = new User("user", "e@e.com", "h", ValidName, ValidAddress);
            var newAddress = new Address("NewSt", "2", "NewCity", "11111", new Geolocation("1", "1"));

            // When
            user.UpdateAddress(newAddress);

            // Then
            user.Address.ShouldBe(newAddress);
        }

        [Fact(DisplayName = "Given null new address, when updating address, then DomainValidationException is thrown")]
        public void GivenNullAddress_WhenUpdatingAddress_ThenDomainValidationExceptionIsThrown()
        {
            var user = new User("user", "e@e.com", "h", ValidName, ValidAddress);
            Should.Throw<DomainValidationException>(() =>
                user.UpdateAddress(null!))
                  .Message.ShouldBe("New address is required");
        }

        [Fact(DisplayName = "Given valid new password, when changing password, then PasswordHash is updated")]
        public void GivenValidPassword_WhenChangingPassword_ThenPasswordHashIsUpdated()
        {
            var user = new User("user", "e@e.com", "old", ValidName, ValidAddress);

            user.ChangePassword("newhash");

            user.PasswordHash.ShouldBe("newhash");
        }

        [Fact(DisplayName = "Given empty new password, when changing password, then DomainValidationException is thrown")]
        public void GivenEmptyPassword_WhenChangingPassword_ThenDomainValidationExceptionIsThrown()
        {
            var user = new User("user", "e@e.com", "old", ValidName, ValidAddress);

            Should.Throw<DomainValidationException>(() =>
                user.ChangePassword(""))
                  .Message.ShouldBe("New password hash is required");
        }
    }
}
