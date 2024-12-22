namespace CN_API.Repositories;

public interface IRepository<T> where T : IAggregateRoot
{
    Task<T> Add(T data);
}