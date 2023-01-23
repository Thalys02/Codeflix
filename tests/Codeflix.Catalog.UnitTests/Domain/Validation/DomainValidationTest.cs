using Bogus;
using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Domain.Validation
{
    public class DomainValidationTest
    {

        private Faker Faker { get; set; } = new Faker();

        [Fact(DisplayName = nameof(NotNullOk))]
        [Trait("Domain", "DomainValidation - Validaton")]
        public void NotNullOk()
        {
            var value = Faker.Commerce.ProductName();
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
            Action action = () => DomainValidation.NotNull(value, fieldName);
            action.Should().NotThrow();

        }
        [Fact(DisplayName = nameof(NotNullThrowWhenNull))]
        [Trait("Domain", "DomainValidation - Validaton")]
        public void NotNullThrowWhenNull()
        {
            string? value = null;
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
            Action action = () => DomainValidation.NotNull(value, fieldName);
            action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should not be null");

        }
        [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
        [Trait("Domain", "DomainValidation - Validaton")]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void NotNullOrEmptyThrowWhenEmpty(string? target)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
            Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);

            action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should not be null or empty");
        }

        [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
        [Trait("Domain", "DomainValidation - Validaton")]
        public void NotNullOrEmptyOk()
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
            var target = Faker.Commerce.ProductName();

            Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);

            action.Should().NotThrow();
        }

        [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
        [Trait("Domain", "DomainValidation - Validaton")]
        [MemberData(nameof(GetValuesSmallerThanTheMin), parameters: 10)]
        public void MinLengthThrowWhenLess(string target, int minLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action = () => DomainValidation.MinLength(target, minLength, fieldName);

            action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should not be less than {minLength} caracters long");
        }
        public static IEnumerable<object[]> GetValuesSmallerThanTheMin(int numberOftests = 5)
        {
            var faker = new Faker();
            for (int i = 0; i < numberOftests; i++)
            {
                var example = faker.Commerce.ProductName();
                var minLength = example.Length + (new Random()).Next(1, 20);
                yield return new object[] { example, minLength };

            }
        }

        [Theory(DisplayName = nameof(MinLengthOk))]
        [Trait("Domain", "DomainValidation - Validaton")]
        [MemberData(nameof(GetValuesGreaterThanTheMin), parameters: 10)]
        public void MinLengthOk(string target, int minLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action = () => DomainValidation.MinLength(target, minLength, fieldName);

            action.Should().NotThrow();
        }
        public static IEnumerable<object[]> GetValuesGreaterThanTheMin(int numberOftests = 5)
        {
            var faker = new Faker();
            for (int i = 0; i < numberOftests; i++)
            {
                var example = faker.Commerce.ProductName();
                var minLength = example.Length - (new Random()).Next(1, 5);
                yield return new object[] { example, minLength };

            }
        }
        [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
        [Trait("Domain", "DomainValidation - Validaton")]
        [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
        public void MaxLengthThrowWhenGreater(string target, int maxLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
            Action action = () => DomainValidation.MaxLength(target, maxLength, fieldName);

            action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should not be greater than {maxLength} caracters long");
        }

        public static IEnumerable<object[]> GetValuesGreaterThanMax(int numberOftests = 5)
        {
            var faker = new Faker();
            for (int i = 0; i < numberOftests; i++)
            {
                var example = faker.Commerce.ProductName();
                var maxLength = example.Length - (new Random()).Next(1, 5);
                yield return new object[] { example, maxLength };

            }
        }
        [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
        [Trait("Domain", "DomainValidation - Validaton")]
        [MemberData(nameof(GetValuesLessThanMax), parameters: 10)]
        public void MaxLengthOk(string target, int maxLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action = () => DomainValidation.MaxLength(target, maxLength, fieldName);

            action.Should().NotThrow();
        }

        public static IEnumerable<object[]> GetValuesLessThanMax(int numberOftests = 5)
        {
            var faker = new Faker();
            for (int i = 0; i < numberOftests; i++)
            {
                var example = faker.Commerce.ProductName();
                var maxLength = example.Length + (new Random()).Next(0, 5);
                yield return new object[] { example, maxLength };

            }
        }
    }
}
