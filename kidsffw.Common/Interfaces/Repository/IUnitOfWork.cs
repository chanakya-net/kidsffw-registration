using kidsffw.Domain.Base;

namespace kidsffw.Common.Interfaces.Repository;


public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    Task RollBackChangesAsync();
    IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity;
}
