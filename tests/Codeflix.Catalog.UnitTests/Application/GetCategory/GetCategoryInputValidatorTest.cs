using Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using FluentAssertions;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.GetCategory
{
    [Collection(nameof(GetCategoryTestFixture))]
    public class GetCategoryInputValidatorTest
    {
        private readonly GetCategoryTestFixture _fixture;

        public GetCategoryInputValidatorTest(GetCategoryTestFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact(DisplayName = nameof(ValidationOk))]
        [Trait("Application", "GetCategoryInputValidator - UseCases")]
        public void ValidationOk()
        {
            var validInput = new GetCategoryInput(Guid.NewGuid());
            var validator = new GetCategoryInputValidator();

            var validResult = validator.Validate(validInput);

            validResult.Should().NotBeNull();
            validResult.IsValid.Should().BeTrue();
            validResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = nameof(InvalidWhenEmptyGuidId))]
        [Trait("Application", "GetCategoryInputValidator - UseCases")]
        public void InvalidWhenEmptyGuidId()
        {
            var invalidInput = new GetCategoryInput(Guid.Empty);
            var validator = new GetCategoryInputValidator();

            var validResult = validator.Validate(invalidInput);

            validResult.Should().NotBeNull();
            validResult.IsValid.Should().BeFalse();
            validResult.Errors.Should().HaveCount(1);
            validResult.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");

        }
    }
}
