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

    }
}
