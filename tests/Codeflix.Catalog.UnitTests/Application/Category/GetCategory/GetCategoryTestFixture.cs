using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.UnitTests.Application.Category.Common;
using Moq;
using Xunit;

using DomainEntity = Codeflix.Catalog.Domain.Entity;
namespace Codeflix.Catalog.UnitTests.Application.Category.GetCategory
{
    [CollectionDefinition(nameof(GetCategoryTestFixture))]
    public class GetCategoryTestFixtureCollection :
        ICollectionFixture<GetCategoryTestFixture>
    { }

    public class GetCategoryTestFixture : CategoryBaseFixture
    { }
}
