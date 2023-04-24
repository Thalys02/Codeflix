using Codeflix.Catalog.Application.UseCases.Category.Common;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.Domain.SeedWork.SearchableRepository;

namespace Codeflix.Catalog.Application.UseCases.Category.ListCategories
{
    public class ListCategories : IListCategories
    {
        private readonly ICategoryRepository _categoryRepository;

        public ListCategories(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ListCategoriesOutput> Handle(ListCategoriesInput request,
                                                 CancellationToken cancellationToken)
        {
            var searchOutput = await _categoryRepository.Search(
                new SearchInput(request.Page,
                                request.PerPage,
                                request.Search,
                                request.Sort,
                                request.Direction), cancellationToken);

            return new ListCategoriesOutput(
                searchOutput.CurrentPage,
                searchOutput.PerPage,
                searchOutput.Total,
                searchOutput.Items
                            .Select(CategoryModelOutput.FromCategory)
                            .ToList()
                );
        }
    }
}
