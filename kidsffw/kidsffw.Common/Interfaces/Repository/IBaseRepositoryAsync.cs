using kidsffw.Domain.Base;

namespace kidsffw.Common.Interfaces.Repository;

public interface IBaseRepositoryAsync<T> where T: BaseEntity
{
    public Task<T> GetByIdAsync(int id);
    Task<IList<T>> ListAllAsync();
    Task<T> AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<IList<T?>> ListAsync(ISpecification<T> spec);
    Task<T?> FirstOrDefaultAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);
}