using System.Linq.Expressions;

namespace MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;

public interface INoTrackingRepositoryBase<T>
{
    IQueryable<T> FindAll();

    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
}