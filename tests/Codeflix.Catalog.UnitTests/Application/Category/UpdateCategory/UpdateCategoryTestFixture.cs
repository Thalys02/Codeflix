using Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using Codeflix.Catalog.UnitTests.Application.Category.Common;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory
{
    [CollectionDefinition(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture> { }
    public class UpdateCategoryTestFixture : CategoryBaseFixture
    {
        public UpdateCategoryInput GetValidInput(Guid? id = null)
            => new(id ?? Guid.NewGuid(),
                    GetValidCategoryName(),
                    GetValidCategoryDescription(),
                    GetRandomBoolean());
        public UpdateCategoryInput GetInvalidInputShortName()
        {
            var invalidInputShortName = GetValidInput();
            invalidInputShortName.Name = invalidInputShortName.Name.Substring(0, 2);
            return invalidInputShortName;
        }
        public UpdateCategoryInput GetInvalidInputTooLongName()
        {
            var invalidInputTooLongName = GetValidInput();
            var tooLongNameForCategory = Faker.Commerce.ProductName();
            while (tooLongNameForCategory.Length <= 255)
                tooLongNameForCategory = $"{tooLongNameForCategory} {Faker.Commerce.ProductName()}";
            invalidInputTooLongName.Name = tooLongNameForCategory;
            return invalidInputTooLongName;
        }
        public UpdateCategoryInput GetInvalidInputTooLongDescription()
        {
            var invalidInputTooLongDescription = GetValidInput();
            var tooLongDescriptionForCategory = Faker.Commerce.ProductDescription();
            while (tooLongDescriptionForCategory.Length <= 10_000)
                tooLongDescriptionForCategory = $"{tooLongDescriptionForCategory} {Faker.Commerce.ProductDescription()}";
            invalidInputTooLongDescription.Description = tooLongDescriptionForCategory;
            return invalidInputTooLongDescription;
        }
    }
}
