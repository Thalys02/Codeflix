using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.UnitTests.Common;
using Moq;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.UpdateCategory
{
    [CollectionDefinition(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture> { }
    public class UpdateCategoryTestFixture : BaseFixture
    {
        public Mock<ICategoryRepository> GetRepositoryMock()
            => new();

        public Mock<IUnitOfWork> GetUnitOfWorkMock()
            => new();
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
        public bool GetRandomBoolean()
            => (new Random().NextDouble() < 0.5);
        public Category GetExampleCategory()
            => new Category(GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBoolean());
        public UpdateCategoryInput GetValidInput(Guid? id = null)

            => new(id ?? Guid.NewGuid(),
                    GetValidCategoryName(),
                    GetValidCategoryDescription(),
                    GetRandomBoolean());

    }
}
