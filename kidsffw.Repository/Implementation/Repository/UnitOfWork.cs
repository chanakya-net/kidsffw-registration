using kidsffw.Common.Interfaces.Repository;
using kidsffw.Domain.Base;
using kidsffw.Repository.DbContext;

namespace kidsffw.Repository.Implementation.Repository;

public class UnitOfWork : IUnitOfWork
{
    protected readonly RegistrationDbContext _dbContext;
    private IDictionary<Type, dynamic> _repositories;

    public UnitOfWork(RegistrationDbContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Dictionary<Type, dynamic>();
    }

    public IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity
    {
        var entityType = typeof(T);
        if (_repositories.ContainsKey(entityType))
        {
            return _repositories[entityType];
        }

        var repositoryType = typeof(BaseRepositoryAsync<>);
        var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);
        if (repository == null)
        {
            throw new InvalidOperationException("Repository not found");
        }
        _repositories.Add(entityType, repository);
        return (IBaseRepositoryAsync<T>)repository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task RollBackChangesAsync()
    {
        await _dbContext.Database.RollbackTransactionAsync();
    }
}