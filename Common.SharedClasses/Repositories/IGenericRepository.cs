namespace Common.SharedClasses.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FindByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task SoftDeleteAsync(T entity);
        Task HardDeleteAsync(T entity);
        Task SaveChangesAsync();
        Task<IEnumerable<T>> GetPagedResponseAsync(int pageNumber, int pageSize);
        Task UpdateAsync(T entity);
    }
}
