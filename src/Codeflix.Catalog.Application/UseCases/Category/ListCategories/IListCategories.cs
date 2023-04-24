using MediatR;

namespace Codeflix.Catalog.Application.UseCases.Category.ListCategories
{
    public interface IListCategories
        : IRequestHandler<ListCategoriesInput, ListCategoriesOutput>
    {
    }
}
