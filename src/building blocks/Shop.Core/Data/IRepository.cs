using Shop.Core.DomainObjects;

namespace Shop.Core.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
    Task<string> PersistirDados();
}
