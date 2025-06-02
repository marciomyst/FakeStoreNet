using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.ValueObjects;
using Shouldly;

namespace FakeStoreNet.Domain.Tests
{
    public class NameTests
    {
        [Fact(DisplayName = "Given valid first and last names, when creating Name, then properties are assigned")]
        public void GivenValidFirstAndLastNames_WhenCreatingName_ThenPropertiesAreAssigned()
        {
            // Given
            var first = "John";
            var last = "Doe";

            // When
            var name = new Name(first, last);

            // Then
            name.FirstName.ShouldBe(first);
            name.LastName.ShouldBe(last);
        }

        [Fact(DisplayName = "Given empty first name, when creating Name, then DomainValidationException is thrown")]
        public void GivenEmptyFirstName_WhenCreatingName_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() => new Name("", "Doe"))
                  .Message.ShouldBe("FirstName is required");
        }

        [Fact(DisplayName = "Given empty last name, when creating Name, then DomainValidationException is thrown")]
        public void GivenEmptyLastName_WhenCreatingName_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() => new Name("John", ""))
                  .Message.ShouldBe("LastName is required");
        }
    }
}
