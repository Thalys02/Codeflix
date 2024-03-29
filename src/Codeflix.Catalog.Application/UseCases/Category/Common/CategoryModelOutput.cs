﻿using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.Application.UseCases.Category.Common
{
    public class CategoryModelOutput
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public CategoryModelOutput(Guid id,
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

        public static CategoryModelOutput FromCategory(DomainEntity.Category category)
        => new(
                category.Id,
                category.Name,
                category.Description,
                category.CreatedAt,
                category.IsActive
            );
    }
}
