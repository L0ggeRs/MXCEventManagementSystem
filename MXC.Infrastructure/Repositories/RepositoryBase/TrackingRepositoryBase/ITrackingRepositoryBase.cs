using System.Linq.Expressions;

namespace MXC.Infrastructure.Repositories.RepositoryBase.TrackingRepositoryBase;

public interface ITrackingRepositoryBase<T>
{
    IQueryable<T> FindAll();

    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

    void Create(T entity);

    void Delete(T entity);

    Task<int> SaveAsync(CancellationToken cancellationToken);
}