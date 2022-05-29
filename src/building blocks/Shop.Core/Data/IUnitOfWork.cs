namespace Shop.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}