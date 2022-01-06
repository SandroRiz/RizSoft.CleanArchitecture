using RizSoft.CleanArchitecture.Application;

namespace RizSoft.CleanArchitecture.Services;

public class BaseService<T, Tkey> : IBaseService<T, Tkey>
where T : class
{
    private readonly IBaseRepository<T, Tkey> _repository;
    public BaseService(IBaseRepository<T, Tkey> repository)
    {
        _repository = repository;
    }
    public async Task AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        await _repository.DeleteAsync(entity);
    }

    public async Task DeleteAsync(Tkey id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<T> GetAsync(Tkey id)
    {
        return await _repository.GetAsync(id);
    }

    public async Task<List<T>> ListAsync()
    {
        return await _repository.ListAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        await _repository.UpdateAsync(entity);
    }
}

