using Microsoft.EntityFrameworkCore;
using RizSoft.CleanArchitecture.Application;

namespace RizSoft.CleanArchitecture.Repository.SqlServer.EfCore.Factory;

public class BaseRepository<T, Tkey> : IBaseRepository<T, Tkey>
where T : class
{
    protected IDbContextFactory<DataContext> CtxFactory { get; }


    public BaseRepository(IDbContextFactory<DataContext> ctxFactory)
    {
        this.CtxFactory = ctxFactory;
    }
    //public DbSet<T> Set => Context.Set<T>();

    public virtual async Task AddAsync(T entity)
    {
        using (var ctx = CtxFactory.CreateDbContext())
        {
            var Set = ctx.Set<T>();
            Set.Add(entity);
            await ctx.SaveChangesAsync();
        }
    }

    public virtual async Task DeleteAsync(T entity)
    {
        using (var ctx = CtxFactory.CreateDbContext())
        {
            var Set = ctx.Set<T>();
            ctx.Entry(entity).State = EntityState.Deleted;
            await ctx.SaveChangesAsync();
        }
    }

    public virtual async Task DeleteAsync(Tkey id)
    {
        using (var ctx = CtxFactory.CreateDbContext())
        {
            var Set = ctx.Set<T>();

            T entity = await Set.FindAsync(id);

            ctx.Entry(entity).State = EntityState.Deleted;
            await ctx.SaveChangesAsync();
        }
    }

    public virtual async Task<T> GetAsync(Tkey id)
    {
        using (var ctx = CtxFactory.CreateDbContext())
        {
            var Set = ctx.Set<T>();
            return await Set.FindAsync(id);
        }
    }

    public virtual async Task<List<T>> ListAsync()
    {
        using (var ctx = CtxFactory.CreateDbContext())
        {
            var Set = ctx.Set<T>();
            return await Set.ToListAsync();
        }
    }

    public virtual async Task UpdateAsync(T entity)
    {
        using (var ctx = CtxFactory.CreateDbContext())
        {
            var Set = ctx.Set<T>();
            ctx.Entry(entity).State = EntityState.Modified;

            await ctx.SaveChangesAsync();
        }
    }


}

