using DomainEntity = Codeflix.Catalog.Domain.Entity;
namespace Codeflix.Catalog.Application.UseCases.Category.CreateCategory
{
    public class CreateCategoryOutput
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public CreateCategoryOutput(Guid id,
                                    string name,
                                    string description,
                                    DateTime createdAt,
                                    bool isActive)
        {
            Id = id;
            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = createdAt;
        }

        public static CreateCategoryOutput FromCategory(DomainEntity.Category category)
        => new CreateCategoryOutput(
                category.Id,
                category.Name,
                category.Description,
                category.CreatedAt,
                category.IsActive
            );


    }
}
