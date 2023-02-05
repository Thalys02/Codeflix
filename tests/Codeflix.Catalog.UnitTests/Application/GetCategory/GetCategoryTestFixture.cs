using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.UnitTests.Common;
using Moq;
using Xunit;

using DomainEntity = Codeflix.Catalog.Domain.Entity;
namespace Codeflix.Catalog.UnitTests.Application.GetCategory
{
    [CollectionDefinition(nameof(GetCategoryTestFixture))]
    public class GetCategoryTestFixtureCollection :
        ICollectionFixture<GetCategoryTestFixture>
    { }

    public class GetCategoryTestFixture : BaseFixture
    {
        public Mock<ICategoryRepository> GetRepositoryMock() => new();

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
