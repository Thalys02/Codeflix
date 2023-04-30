using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using Codeflix.Catalog.UnitTests.Common;
using Moq;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.ListCategories
{
    [CollectionDefinition(nameof(ListCategoriesTestFixture))]
    public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture> { }
    public class ListCategoriesTestFixture : BaseFixture
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
        public List<Category> GetExampleCategoriesList(int length = 10)
        {
            var list = new List<Category>();
            for (int i = 0; i < length; i++)
            {
                list.Add(GetExampleCategory());
            }
            return list;
        }
        public ListCategoriesInput GetExampleInput()
        {
            var random = new Random();
            return new ListCategoriesInput(
                page: random.Next(1, 10),
                perPage: random.Next(15, 100),
                search: Faker.Commerce.ProductName(),
                sort: Faker.Commerce.ProductName(),
                direction: random.Next(0, 10) > 5 ? SearchOrder.Asc : SearchOrder.Desc);
        }
    }
}
