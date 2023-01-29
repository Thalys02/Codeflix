using Codeflix.Catalog.UnitTests.Common;
using Codeflix.Catalog.UnitTests.Domain.Entity.Category;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entity;
namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category
{
    [CollectionDefinition(nameof(CategoryTestFixture))]
    public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture> { }

    public class CategoryTestFixture : BaseFixture
    {
        public CategoryTestFixture() : base() { }
        public string GetValidCategoryName()
        {
            string categoryName = "";

            while (categoryName.Length < 3)
                categoryName = Faker.Commerce.Categories(1)[0];
            while (categoryName.Length > 255)
                categoryName = categoryName[..255]; // Pega os 255 caracteres da string
            return categoryName;
        }
        public string GetValidCategoryDescription()
        {
            string categoryDescription = Faker.Commerce.ProductDescription();

            if (categoryDescription.Length > 10_000)
                categoryDescription = categoryDescription[..10_000];
            return categoryDescription;
        }
        public DomainEntity.Category GetValidCategory() => new DomainEntity.Category(GetValidCategoryName(), GetValidCategoryDescription());
    }
}




