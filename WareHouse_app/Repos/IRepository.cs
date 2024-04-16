public interface IRepository<T> 
{
    public Task AddAsync(T item);
    public Task DeleteAsync(Guid id);
}