using Microsoft.EntityFrameworkCore;
using MXC.Infrastructure.Context;
using System.Linq.Expressions;

namespace MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;

public class NoTrackingRepositoryBase<T>(ApplicationNoTrackingDbContext context) : INoTrackingRepositoryBase<T> where T : class
{
    protected ApplicationNoTrackingDbContext Context { get; set; } = context;

    public IQueryable<T> FindAll()
    {
        return Context.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return Context.Set<T>().Where(expression).AsNoTracking();
    }
}