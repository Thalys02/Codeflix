using Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;
using UseCases = Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory
{
    [Collection(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryTest
    {
        private readonly CreateCategoryTestFixture _fixture;

        public CreateCategoryTest(CreateCategoryTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async void CreateCategory()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var useCase = new UseCases.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

            var input = _fixture.GetInput();

            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(
                repository => repository.Insert(
                    It.IsAny<Category>(),
                    It.IsAny<CancellationToken>()
                    ),
                Times.Once
            );

            unitOfWorkMock.Verify(
                uow => uow.Commit(It.IsAny<CancellationToken>()),
                Times.Once
            );

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(input.IsActive);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        }

        [Fact(DisplayName = nameof(CreateCategoryWithOnlyName))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async void CreateCategoryWithOnlyName()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var useCase = new UseCases.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

            var input = new CreateCategoryInput(_fixture.GetValidCategoryName());

            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(
                repository => repository.Insert(
                    It.IsAny<Category>(),
                    It.IsAny<CancellationToken>()
                    ),
                Times.Once
            );

            unitOfWorkMock.Verify(
                uow => uow.Commit(It.IsAny<CancellationToken>()),
                Times.Once
            );

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be("");
            output.IsActive.Should().BeTrue();
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        }

        [Fact(DisplayName = nameof(CreateCategoryWithOnlyNameAndDescription))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async void CreateCategoryWithOnlyNameAndDescription()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var useCase = new UseCases.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

            var input = new CreateCategoryInput(_fixture.GetValidCategoryName(),_fixture.GetValidCategoryDescription());

            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(
                repository => repository.Insert(
                    It.IsAny<Category>(),
                    It.IsAny<CancellationToken>()
                    ),
                Times.Once
            );

            unitOfWorkMock.Verify(
                uow => uow.Commit(It.IsAny<CancellationToken>()),
                Times.Once
            );

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().BeTrue();
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        }


        [Theory(DisplayName = nameof(ThrowWhenCantInstantiateAggregate))]
        [Trait("Application", "CreateCategory - Use Cases")]
        [MemberData(nameof(GetInvalidInputs))]
        public async void ThrowWhenCantInstantiateAggregate(CreateCategoryInput input, string exceptionMessage)
        {
            var useCase = new UseCases.CreateCategory(_fixture.GetRepositoryMock().Object, _fixture.GetUnitOfWorkMock().Object);

            Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should()
                      .ThrowAsync<EntityValidationException>()
                      .WithMessage(exceptionMessage);
        }
        public static IEnumerable<object[]> GetInvalidInputs()
        {
            var fixture = new CreateCategoryTestFixture();

            var invalidInputsList = new List<object[]>();

            //Nome não pode ser menor que 3 caracteres
            #region InvalidInputShortName
            var invalidInputShortName = fixture.GetInput();

            invalidInputShortName.Name = invalidInputShortName.Name.Substring(0, 2);

            invalidInputsList.Add(new object[]{
                invalidInputShortName,
                "Name should be at leats 3 characters long"
            });
            #endregion
            //Nome não pode ser maior do que 255 caracteres
            #region InvalidInputTooLongName
            var invalidInputTooLongName = fixture.GetInput();

            var tooLongNameForCategory = string.Empty;

            while (tooLongNameForCategory.Length <= 255)
                tooLongNameForCategory = $"{tooLongNameForCategory} {fixture.Faker.Commerce.ProductName}";

            invalidInputTooLongName.Name = tooLongNameForCategory;

            invalidInputsList.Add(new object[]{
                invalidInputTooLongName,
                "Name should be less or equals 255 characters long"
            });
            #endregion
            //Descrição não pode ser nulo
            #region InvalidInputNullableDescription
            var invalidInputDescriptionNull = fixture.GetInput();

            invalidInputDescriptionNull.Description = null!;

            invalidInputsList.Add(new object[]{
                invalidInputDescriptionNull,
                "Description should not be null"
            });
            #endregion

            //Descrição não pode ser maior do que 10.000 caracteres
            #region InvalidInputDescriptionGreater
            var invalidInputTooLongDescription = fixture.GetInput();

            var tooLongDescriptionForCategory = fixture.Faker.Commerce.ProductDescription();

            while (tooLongDescriptionForCategory.Length <= 10_000)
                tooLongDescriptionForCategory = $"{tooLongDescriptionForCategory} {fixture.Faker.Commerce.ProductDescription()}";

            invalidInputTooLongDescription.Description = tooLongDescriptionForCategory;

            invalidInputsList.Add(new object[]{
                invalidInputTooLongDescription,
                "Description should be less or equals 10000 characters long" });
            #endregion
            return invalidInputsList;
        }

    }
}
