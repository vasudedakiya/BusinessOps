using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services.Repository
{
    public class DataRepository<T> : IDataRepository<T>, IDisposable
        where T : class
    {
        protected DataRepository(DbContext context)
        {
            this.context = context;
        }

        private DbContext context { get; }

        public async Task<bool> Exists(Expression<Func<T, bool>> expression) =>
           await this.context.Set<T>().AsNoTracking().Where(expression).AnyAsync();

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate) => await this.context.Set<T>().AsNoTracking().Where(predicate)
            .FirstOrDefaultAsync().ConfigureAwait(false);

        public async Task<IEnumerable<T>> GetAllAsync() =>
           await this.context.Set<T>().AsNoTracking().ToListAsync();

        public IEnumerable<T> GetAll() =>
           this.context.Set<T>().AsNoTracking().ToList();

        public IQueryable<T> GetAllQueryable() => this.context.Set<T>().AsNoTracking();

        public IQueryable<T> Find(Expression<Func<T, bool>> expression) =>
            this.context.Set<T>().AsNoTracking().Where(expression);

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression) =>
            await this.context.Set<T>().AsNoTracking().Where(expression).ToListAsync();

        public void Add(T entity) =>
            this.context.Set<T>().Add(entity);

        public void AddRange(List<T> entity) =>
            this.context.Set<T>().AddRange(entity);

        public void Update(T entity)
        {
            this.context.Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity) =>
            this.context.Set<T>().Remove(entity);

        public void DeleteRange(List<T> entity) =>
            this.context.Set<T>().RemoveRange(entity);

        public async Task SaveAsync() =>
                await this.context.SaveChangesAsync().ConfigureAwait(false);

        public void UpdateEntity(T entity)
        {
            this.context.Entry(entity).State = EntityState.Detached;
            this.context.Set<T>().Update(entity);
        }

        public void CreateEntity(T entity) =>
            this.context.Set<T>().Add(entity);

        public void CreateMultipleEntity(List<T> entity) =>
            this.context.Set<T>().AddRange(entity);

        public void UpdateMultipleEntities(List<T> entities)
        {
            foreach (var entity in entities)
            {
                this.context.Set<T>().Attach(entity);
                this.context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void DeleteMultipleEntity(List<T> entity) =>
            this.context.Set<T>().RemoveRange(entity);

        public async Task<IEnumerable<TEntity>> ExecuteStoredProcedureListAsync<TEntity>(string sql, params object[] parameters)
        where TEntity : class
        => await this.context.Set<TEntity>().FromSqlRaw(sql, parameters).ToListAsync();

        public async Task<TEntity> ExecuteStoredProcedureSingleAsync<TEntity>(string sql, params object[] parameters)
            where TEntity : class
        {
            IEnumerable<TEntity> entities = await this.context.Set<TEntity>().FromSqlRaw(sql, parameters).ToListAsync();

            return entities?.FirstOrDefault();
        }

        public void ExecuteSqlRaw(string sql, params object[] parameters)
        => context.Database.ExecuteSqlRaw(sql, parameters);

        public async Task<TEntity> ExecuteFunctionAsync<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            IEnumerable<TEntity> entities = await context.Set<TEntity>().FromSqlRaw(sql, parameters).ToListAsync();
            return entities?.FirstOrDefault();
        }
        public void Dispose() =>
            this.context.Dispose();
    }
}
