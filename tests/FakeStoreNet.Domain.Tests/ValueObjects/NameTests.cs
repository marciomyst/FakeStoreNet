using System;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Domain.Tests.ValueObjects
{
    public class NameTests
    {
        [Fact(DisplayName = "Ctor with valid firstName and lastName sets properties")]
        public void Ctor_WithValidFirstAndLastName_SetsProperties()
        {
            // Given
            string firstName = "John";
            string lastName = "Doe";

            // When
            var name = new Name(firstName, lastName);

            // Then
            name.FirstName.ShouldBe(firstName);
            name.LastName.ShouldBe(lastName);
        }

        [Theory(DisplayName = "Ctor with invalid firstName throws DomainValidationException")]
        [InlineData("", "Doe")]
        [InlineData(null, "Doe")]
        public void Ctor_InvalidFirstName_ThrowsDomainValidationException(string? firstName, string lastName)
        {
            // Given
            // firstName supplied by InlineData, lastName = lastName

            // When
#pragma warning disable CS8604 // Possible null reference argument.
            Action act = () => new Name(firstName, lastName);
#pragma warning restore CS8604 // Possible null reference argument.

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("FirstName is required");
        }

        [Theory(DisplayName = "Ctor with invalid lastName throws DomainValidationException")]
        [InlineData("John", "")]
        [InlineData("John", null)]
        public void Ctor_InvalidLastName_ThrowsDomainValidationException(string firstName, string? lastName)
        {
            // Given
            // firstName = firstName, lastName supplied by InlineData

            // When
#pragma warning disable CS8604 // Possible null reference argument.
            Action act = () => new Name(firstName, lastName);
#pragma warning restore CS8604 // Possible null reference argument.

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("LastName is required");
        }

        [Fact(DisplayName = "Equals returns true for same values")]
        public void Equals_SameValues_ReturnsTrue()
        {
            // Given
            var a = new Name("Jane", "Smith");
            var b = new Name("Jane", "Smith");

            // When & Then
            a.ShouldBe(b);
            (a == b).ShouldBeTrue();
        }

        [Fact(DisplayName = "Equals returns false for different values")]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            // Given
            var a = new Name("Jane", "Smith");
            var b = new Name("John", "Smith");

            // When & Then
            a.ShouldNotBe(b);
            (a != b).ShouldBeTrue();
        }
    }
}
