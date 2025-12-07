using Microsoft.EntityFrameworkCore;

namespace Common.SharedClasses.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext dbContext;
        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            dbContext.Set<T>().Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task SoftDeleteAsync(T entity)
        {
            var property = entity.GetType().GetProperty("IsDeleted");
            if (property != null && property.PropertyType == typeof(bool))
            {
                property.SetValue(entity, true);
                await dbContext.SaveChangesAsync();
            }

            else
            {
                throw new InvalidOperationException($"Entity {typeof(T).Name} Does not Have IsDeleted as a property");
            }
        }

        public async Task HardDeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<T?> FindByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }
        public async Task<IEnumerable<T>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            return await dbContext.Set<T>().Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            dbContext.Set<T>().Update(entity);
            await SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<T?> GetByIdOptionalTracking(int id, bool tracking = true)
        {
            var entity = await dbContext.Set<T>().FindAsync(id);
            if (!tracking)
            {
                dbContext.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }
    }
}
