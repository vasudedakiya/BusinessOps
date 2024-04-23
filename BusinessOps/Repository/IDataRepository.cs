using System.Linq.Expressions;

namespace Services.Repository
{
    public interface IDataRepository<T>
         where T : class
    {
        Task<bool> Exists(Expression<Func<T, bool>> expression);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> GetAllAsync();
        
        IEnumerable<T> GetAll();

        IQueryable<T> GetAllQueryable();

        IQueryable<T> Find(Expression<Func<T, bool>> expression);

        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);

        void Add(T entity);

        void AddRange(List<T> entity);

        void Update(T entity);

        void Delete(T entity);

        void DeleteRange(List<T> entity);

        Task SaveAsync();

        Task<IEnumerable<TEntity>> ExecuteStoredProcedureListAsync<TEntity>(string sql, params object[] parameters) where TEntity : class;

        Task<TEntity> ExecuteStoredProcedureSingleAsync<TEntity>(string sql, params object[] parameters)
          where TEntity : class;

        void ExecuteSqlRaw(string sql, params object[] parameters);

        Task<TEntity> ExecuteFunctionAsync<TEntity>(string sql, params object[] parameters) where TEntity : class;
    }
}
