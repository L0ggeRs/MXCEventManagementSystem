using MXC.Infrastructure.Context;
using System.Linq.Expressions;

namespace MXC.Infrastructure.Repositories.RepositoryBase.TrackingRepositoryBase;

public abstract class TrackingRepositoryBase<T>(ApplicationTrackingDbContext context) : ITrackingRepositoryBase<T> where T : class
{
    protected ApplicationTrackingDbContext Context { get; set; } = context;

    public void Create(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    public IQueryable<T> FindAll()
    {
        return Context.Set<T>();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return Context.Set<T>().Where(expression);
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }
}