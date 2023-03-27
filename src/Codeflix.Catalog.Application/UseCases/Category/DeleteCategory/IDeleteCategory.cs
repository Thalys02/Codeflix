using MediatR;

namespace Codeflix.Catalog.Application.UseCases.Category.DeleteCategory
{
    public interface IDeleteCategory : IRequestHandler<DeleteCategoryInput>
    {
    }
}
