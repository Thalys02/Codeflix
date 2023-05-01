using Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using Codeflix.Catalog.UnitTests.Application.Category.Common;
using Xunit;
using CategoryEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Application.Category.ListCategories
{
    [CollectionDefinition(nameof(ListCategoriesTestFixture))]
    public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture> { }
    public class ListCategoriesTestFixture : CategoryBaseFixture
    {
        public List<CategoryEntity.Category> GetExampleCategoriesList(int length = 10)
        {
            var list = new List<CategoryEntity.Category>();
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
