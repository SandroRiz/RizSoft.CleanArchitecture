using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RizSoft.CleanArchitecture.Application;

namespace RizSoft.CleanArchitecture.Repository.SqlServer.EfCore.Factory;

public class QueryBaseRepository<T> : IQueryBaseRepository<T>
where T : class
{
    protected IDbContextFactory<DataContext> CtxFactory { get; }

    public QueryBaseRepository(IDbContextFactory<DataContext> ctxFactory)
    {
        this.CtxFactory = ctxFactory;
    }

    public IQueryable<T> Query
    {
        get
        {
            var dataContext = CtxFactory.CreateDbContext();
            return new DisposableQueryable<T>(dataContext, dataContext.Set<T>());
        }
    }

}

internal class DisposableQueryable<T> :  IOrderedQueryable<T>, IAsyncEnumerable<T>
{
    private readonly DataContext _context;
    private readonly IQueryable<T> _queryableImplementation;

    public DisposableQueryable(DataContext context, IQueryable<T> innerQueryable)
    {
        _context = context;
        _queryableImplementation = innerQueryable;
        Provider = new DisposableQueryProvider(context, innerQueryable.Provider);
    }

    public IEnumerator<T> GetEnumerator()
    {
        try
        {
            using var enumerator = _queryableImplementation.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
        finally
        {
            _context.Dispose();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Type ElementType => _queryableImplementation.ElementType;

    public Expression Expression => _queryableImplementation.Expression;

    public IQueryProvider Provider { get; }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        if (_queryableImplementation is not IAsyncEnumerable<T> ae) throw new NotSupportedException();
        
        try
        {
            await using var enumerator = ae.GetAsyncEnumerator(cancellationToken);
            while (await enumerator.MoveNextAsync())
            {
                yield return enumerator.Current;
            }
        }
        finally
        {
            await _context.DisposeAsync();
        }
    }
}

internal class DisposableQueryProvider : IQueryProvider
{
    private readonly DataContext _context;
    private readonly IQueryProvider _innerQueryProvider;

    public DisposableQueryProvider(DataContext context, IQueryProvider innerQueryProvider)
    {
        _context = context;
        _innerQueryProvider = innerQueryProvider;
    }

    public IQueryable CreateQuery(Expression expression)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        var queryable = _innerQueryProvider.CreateQuery<TElement>(expression);
        return new DisposableQueryable<TElement>(_context, queryable);
    }

    public object? Execute(Expression expression)
    {
        try
        {
            return _innerQueryProvider.Execute(expression);
        }
        finally
        {
            _context.Dispose();
        }
    }

    public TResult Execute<TResult>(Expression expression)
    {
        try
        {
            return _innerQueryProvider.Execute<TResult>(expression);
        }
        finally
        {
            _context.Dispose();
        }
    }
}