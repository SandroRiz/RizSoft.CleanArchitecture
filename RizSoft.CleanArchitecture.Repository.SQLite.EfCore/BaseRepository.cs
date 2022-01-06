using Microsoft.EntityFrameworkCore;
using RizSoft.CleanArchitecture.Application;

namespace RizSoft.CleanArchitecture.Repository.SQLite.EfCore;

public class BaseRepository<T, Tkey> : IBaseRepository<T, Tkey>
where T : class
{

    protected NorthwindDbContext Context { get; }

    public BaseRepository(NorthwindDbContext context)
    {
        this.Context = context;
    }
    public DbSet<T> Set => Context.Set<T>();

    public virtual async Task AddAsync(T entity)
    {
        Set.Add(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        Context.Entry(entity).State = EntityState.Deleted;
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(Tkey id)
    {
        T entity = await Set.FindAsync(id);

        Context.Entry(entity).State = EntityState.Deleted;
        await Context.SaveChangesAsync();
    }

    public virtual async Task<T> GetAsync(Tkey id)
    {
        return await Set.FindAsync(id);
    }

    public virtual async Task<List<T>> ListAsync()
    {
        return await Set.ToListAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;

        await Context.SaveChangesAsync();
    }


}