using Codeflix.Catalog.Application.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;
using UseCase = Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;

namespace Codeflix.Catalog.UnitTests.Application.DeleteCategory
{
    [Collection(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTest
    {
        private readonly DeleteCategoryTestFixture _fixture;

        public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(DeleteCategory))]
        [Trait("Application", "DeleteCategory - Use Cases")]
        public async Task DeleteCategory()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var categoryExample = _fixture.GetValidCategory();

            repositoryMock.Setup(x => x.Get(
                categoryExample.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(categoryExample);

            var input = new UseCase.DeleteCategoryInput(categoryExample.Id);
            var useCase = new UseCase.DeleteCategory(repositoryMock.Object, unitOfWorkMock.Object);

            await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(x => x.Get(
                categoryExample.Id,
                It.IsAny<CancellationToken>()
                ), Times.Once);

            repositoryMock.Verify(x => x.Delete(
               categoryExample,
               It.IsAny<CancellationToken>()
               ), Times.Once);

            unitOfWorkMock.Verify(x => x.Commit(
               It.IsAny<CancellationToken>()
               ), Times.Once);
        }

        [Fact(DisplayName = nameof(ThrowWhenCategoryNotFound))]
        [Trait("Application", "DeleteCategory - Use Cases")]
        public async Task ThrowWhenCategoryNotFound()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var exemploGuid = Guid.NewGuid();

            repositoryMock.Setup(x => x.Get(
                exemploGuid,
                It.IsAny<CancellationToken>())
            ).ThrowsAsync(new NotFoundException($"Category '{exemploGuid}' not found"));

            var input = new UseCase.DeleteCategoryInput(exemploGuid);
            var useCase = new UseCase.DeleteCategory(repositoryMock.Object, unitOfWorkMock.Object);


            var task = async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>();

            repositoryMock.Verify(x => x.Get(
                exemploGuid,
                It.IsAny<CancellationToken>()
                ), Times.Once);
        }
    }
}
