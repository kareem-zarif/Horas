

public interface IBaseRepo<TEntity> where TEntity : BaseEnt
{
    Task<TEntity> GetAsync(Guid id);
    #region IEnumerable,Expression
    //using IEnumerable  => as light (readonly) , lighter ICollection has add/remove method and lighter than IList that has indexing -- but it make  filteration in Memory
    //using IQueryable as it collect all filters and make one sql command and run it , but it not async so can not use it here
    //usning Expression =>to make filteration in database not in memory 
    //coming lamda is bool so Func<TEntity,bool>
    #endregion
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(Guid id);


    //Eager Loading
    Task<TEntity> GetAsyncInclude(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsyncInclude(Expression<Func<TEntity, bool>> predicate = null);
    Task<TEntity> CreateAsyncInclude(TEntity entity);
    Task<TEntity> UpdateAsyncInclude(TEntity entity);
    Task<TEntity> DeleteAsyncInclude(Guid id);

    //+ any heavily Used  Method ex:GetBestSellerProduct =>so that logic in one place 
}
