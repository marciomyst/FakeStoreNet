using System;
using FakeStoreNet.Domain.Entities;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Domain.Tests.Entities
{
    public class UserTests
    {
        private readonly Name validName = new Name("John", "Doe");
private readonly Address validAddress = new Address("Street", "42", "City", "12345", new Geolocation(1m, 2m));

        [Fact]
        public void Constructor_ValidData_ShouldCreateUser()
        {
            var user = new User(1, "username", "email@example.com", "hash", validName, validAddress);
            user.Id.ShouldBe(1);
            user.Username.ShouldBe("username");
            user.Email.ShouldBe("email@example.com");
            user.PasswordHash.ShouldBe("hash");
            user.Name.ShouldBe(validName);
            user.Address.ShouldBe(validAddress);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_InvalidId_ShouldThrow(int id)
        {
            var ex = Should.Throw<DomainValidationException>(() =>
                new User(id, "u", "e", "h", validName, validAddress));
            ex.Message.ShouldBe("Id must be positive");
        }

        [Theory]
        [InlineData("", "Username is required")]
        [InlineData(null, "Username is required")]
        public void Constructor_InvalidUsername_ShouldThrow(string? username, string expectedMessage)
        {
            var ex = Should.Throw<DomainValidationException>(() =>
                new User(1, username!, "e", "h", validName, validAddress));
            ex.Message.ShouldBe(expectedMessage);
        }

        [Theory]
        [InlineData("", "Email is required")]
        [InlineData(null, "Email is required")]
        public void Constructor_InvalidEmail_ShouldThrow(string? email, string expectedMessage)
        {
            var ex = Should.Throw<DomainValidationException>(() =>
                new User(1, "u", email!, "h", validName, validAddress));
            ex.Message.ShouldBe(expectedMessage);
        }

        [Theory]
        [InlineData("", "PasswordHash is required")]
        [InlineData(null, "PasswordHash is required")]
        public void Constructor_InvalidPasswordHash_ShouldThrow(string? hash, string expectedMessage)
        {
            var ex = Should.Throw<DomainValidationException>(() =>
                new User(1, "u", "e", hash!, validName, validAddress));
            ex.Message.ShouldBe(expectedMessage);
        }

        [Fact]
        public void Constructor_NullName_ShouldThrow()
        {
            var ex = Should.Throw<DomainValidationException>(() =>
                new User(1, "u", "e", "h", null!, validAddress));
            ex.Message.ShouldBe("Name is required");
        }

        [Fact]
        public void Constructor_NullAddress_ShouldThrow()
        {
            var ex = Should.Throw<DomainValidationException>(() =>
                new User(1, "u", "e", "h", validName, null!));
            ex.Message.ShouldBe("Address is required");
        }

        [Fact]
        public void Equals_SameId_ShouldBeEqual()
        {
            var a = new User(1, "u1", "e1", "h1", validName, validAddress);
            var b = new User(1, "u2", "e2", "h2", validName, validAddress);
            a.ShouldBe(b);
            (a == b).ShouldBeTrue();
        }

        [Fact]
        public void Equals_DifferentId_ShouldNotBeEqual()
        {
            var a = new User(1, "u", "e", "h", validName, validAddress);
            var b = new User(2, "u", "e", "h", validName, validAddress);
            a.ShouldNotBe(b);
            (a != b).ShouldBeTrue();
        }
    }
}
