using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.UnitTests.Application.Category.Common;
using Codeflix.Catalog.UnitTests.Common;
using Moq;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.Category.CreateCategory
{
    [CollectionDefinition(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture> { }
    public class CreateCategoryTestFixture : CategoryBaseFixture
    {

        public CreateCategoryInput GetInput()
            => new(
                GetValidCategoryName(),
                GetValidCategoryDescription(),
                GetRandomBoolean());
    }
}
