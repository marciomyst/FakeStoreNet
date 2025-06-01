using System;
using FakeStoreNet.Domain.ValueObjects;
using FakeStoreNet.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Domain.Tests.ValueObjects
{
    public class RatingTests
    {
        [Fact(DisplayName = "Ctor with valid rate and count sets properties")]
        public void Ctor_WithValidRateAndCount_SetsProperties()
        {
            // Given
            double expectedRate = 4.5;
            int expectedCount = 10;

            // When
            Rating rating = new Rating(expectedRate, expectedCount);

            // Then
            rating.Rate.ShouldBe(expectedRate);
            rating.Count.ShouldBe(expectedCount);
        }

        [Theory(DisplayName = "Ctor with invalid rate throws DomainValidationException")]
        [InlineData(-1.0)]
        [InlineData(5.1)]
        public void Ctor_InvalidRate_ThrowsDomainValidationException(double invalidRate)
        {
            // Given
            int anyCount = 0;

            // When
            Action act = () => new Rating(invalidRate, anyCount);

            // Then
            var exception = Should.Throw<DomainValidationException>(act);
            exception.Message.ShouldBe("Rate must be between 0.0 and 5.0");
        }

        [Fact(DisplayName = "Ctor with negative count throws DomainValidationException")]
        public void Ctor_NegativeCount_ThrowsDomainValidationException()
        {
            // Given
            double validRate = 0.0;
            int invalidCount = -1;

            // When
            Action act = () => new Rating(validRate, invalidCount);

            // Then
            var exception = Should.Throw<DomainValidationException>(act);
            exception.Message.ShouldBe("Count must be ≥ 0");
        }

        [Fact(DisplayName = "Equals returns true for equal rate and count")]
        public void Equals_SameValues_ReturnsTrue()
        {
            // Given
            Rating firstRating = new Rating(3.3, 5);
            Rating secondRating = new Rating(3.3, 5);

            // When & Then
            firstRating.ShouldBe(secondRating);
            (firstRating == secondRating).ShouldBeTrue();
            (firstRating != secondRating).ShouldBeFalse();
            firstRating.Equals(secondRating).ShouldBeTrue();
            firstRating.Equals((object)secondRating).ShouldBeTrue();
            firstRating.GetHashCode().ShouldBe(secondRating.GetHashCode());
        }

        [Fact(DisplayName = "Equals returns false for same rate and different count")]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            // Given
            Rating firstRating = new Rating(2.0, 5);
            Rating secondRating = new Rating(2.0, 6);

            // When & Then
            firstRating.ShouldNotBe(secondRating);
            (firstRating != secondRating).ShouldBeTrue();
            (firstRating == secondRating).ShouldBeFalse();
            firstRating.Equals(secondRating).ShouldBeFalse();
            firstRating.Equals((object)secondRating).ShouldBeFalse();
        }

        [Fact(DisplayName = "Equals returns false when comparing to null")]
        public void Equals_Null_ReturnsFalse()
        {
            // Given
            Rating rating = new Rating(1.0, 1);

            // When & Then
            rating.ShouldNotBe(null);
            (rating == null).ShouldBeFalse();
            (rating != null).ShouldBeTrue();
            rating.Equals(null).ShouldBeFalse();
        }

        [Fact(DisplayName = "Equals returns true when comparing object to itself")]
        public void Equals_Self_ReturnsTrue()
        {
            // Given
            Rating rating = new Rating(1.0, 1);
            Rating sameRating = rating;

            // When & Then
            rating.ShouldBe(sameRating);
            (rating == sameRating).ShouldBeTrue();
            (rating != sameRating).ShouldBeFalse();
            rating.Equals(sameRating).ShouldBeTrue();
        }
    }
}
