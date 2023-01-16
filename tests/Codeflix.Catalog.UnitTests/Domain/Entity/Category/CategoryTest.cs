using Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entity;
namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category;
public class CategoryTest
{
    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Instantiate()
    {
        //Arrange
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        //Act
        DateTime dateTimeBefore = DateTime.Now;

        var category = new DomainEntity.Category(validData.Name, validData.Description);

        DateTime dateTimeAfter = DateTime.Now;

        //Assert
        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        (category.CreatedAt > dateTimeBefore).Should().BeTrue();
        (category.CreatedAt < dateTimeAfter).Should().BeTrue();
        (category.IsActive).Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        //Arrange
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        //Act
        DateTime dateTimeBefore = DateTime.Now;

        var category = new DomainEntity.Category(validData.Name, validData.Description, isActive);

        DateTime dateTimeAfter = DateTime.Now;

        //Assert
        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        (category.CreatedAt > dateTimeBefore).Should().BeTrue();
        (category.CreatedAt < dateTimeAfter).Should().BeTrue();
        (category.IsActive).Should().Be(isActive);
    }

    [Theory(DisplayName = nameof(ErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void ErrorWhenNameIsEmpty(string? name)
    {

        Action action = () => new DomainEntity.Category(name!, "Category Description");
        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should not be empty or null");

    }

    [Fact(DisplayName = nameof(ErrorWhenDescriptionIsNull))]
    [Trait("Domain", "Category - Aggregates")]
    public void ErrorWhenDescriptionIsNull()
    {

        Action action = () => new DomainEntity.Category("Category Name", null!);
        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Description should not be empty or null");
    }

    [Theory(DisplayName = nameof(ErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("ab")]
    [InlineData("1")]
    [InlineData("12")]
    public void ErrorWhenNameIsLessThan3Characters(string invalidName)
    {
        Action action = () => new DomainEntity.Category(invalidName, "Category Ok description");
        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should be at leats 3 characters long");
    }

    [Fact(DisplayName = nameof(ErrorWhenNameIsGreaterThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void ErrorWhenNameIsGreaterThan255Characters()
    {
        var invalidName = string.Join(null, Enumerable.Range(0, 256).Select(s => "a").ToArray());
        Action action = () => new DomainEntity.Category(invalidName, "Category Ok description");
        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should be less or equals 255 characters long");

    }

    [Fact(DisplayName = nameof(ErrorWhenDescriptionIsGreaterThan10_000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void ErrorWhenDescriptionIsGreaterThan10_000Characters()
    {
        var invalidDescription = string.Join(null, Enumerable.Range(0, 10_001).Select(s => "a").ToArray());
        Action action = () => new DomainEntity.Category("Category Name", invalidDescription);
        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Description should be less or equals 10.000 characters long");
    }
    [Fact(DisplayName = nameof(Activate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Activate()
    {
        //Arrange
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        //Act
        var category = new DomainEntity.Category(validData.Name, validData.Description, false);
        category.Activate();

        category.IsActive.Should().BeTrue();
    }
    [Fact(DisplayName = nameof(Deactivate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Deactivate()
    {
        //Arrange
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        //Act
        var category = new DomainEntity.Category(validData.Name, validData.Description, true);
        category.Deactivate();

        category.IsActive.Should().BeFalse();

    }
    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Category - Aggregates")]
    public void Update()
    {
        var category = new DomainEntity.Category("Category name", "Category description");
        var newValues = new { Name = "New Name", Description = "New Description" };

        category.Update(newValues.Name, newValues.Description);

        category.Name.Should().Be(newValues.Name);
        category.Description.Should().Be(newValues.Description);
    }
    [Fact(DisplayName = nameof(UpdateOnlyName))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateOnlyName()
    {
        var category = new DomainEntity.Category("Category name", "Category description");
        var newValues = new { Name = "New Name" };

        category.Update(newValues.Name);

        category.Name.Should().Be(newValues.Name);
    }
    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void UpdateErrorWhenNameIsEmpty(string? name)
    {
        var category = new DomainEntity.Category("Category name", "Category description");
        Action action = () => category.Update(name!);
        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should not be empty or null");
    }
    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("ab")]
    [InlineData("1")]
    [InlineData("12")]
    public void UpdateErrorWhenNameIsLessThan3Characters(string invalidName)
    {
        var category = new DomainEntity.Category("Category name", "Category description");
        Action action = () => category.Update(invalidName);
        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should be at leats 3 characters long");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenNameIsGreaterThan255Characters()
    {
        var category = new DomainEntity.Category("Category name", "Category description");
        var invalidName = string.Join(null, Enumerable.Range(0, 256).Select(s => "a").ToArray());

        Action action = () => category.Update(invalidName);
        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should be less or equals 255 characters long");

    }

    [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10_000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenDescriptionIsGreaterThan10_000Characters()
    {
        var category = new DomainEntity.Category("Category name", "Category description");
        var invalidDescription = string.Join(null, Enumerable.Range(0, 10_001).Select(s => "a").ToArray());
        Action action = () => category.Update("Category new name", invalidDescription);
        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Description should be less or equals 10.000 characters long");
    }
}

