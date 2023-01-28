namespace Codeflix.Catalog.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public Task Commit(CancellationToken cancellationToken);
    }
}
