namespace RizSoft.CleanArchitecture.Application;

public interface IQueryBaseRepository<out T>
{
     IQueryable<T> Query { get; }

}