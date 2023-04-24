using Codeflix.Catalog.Application.Common;
using Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace Codeflix.Catalog.Application.UseCases.Category.ListCategories
{
    public class ListCategoriesInput : PaginatedListInput, IRequest<ListCategoriesOutput>
    {
        public ListCategoriesInput(int page,
                                   int perPage,
                                   string search,
                                   string sort,
                                   SearchOrder direction)
            : base(page, perPage, search, sort, direction)
        {
        }
    }
}
