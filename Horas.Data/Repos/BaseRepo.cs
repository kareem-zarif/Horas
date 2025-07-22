namespace Horas.Data.Repos
{
    public class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : BaseEnt
    {
        #region readonly
        //using readonly so that in multi threads , no thread change the instance of connection (so all classes inheits from BaseRepo will use same _dbContext,_dbset )
        #endregion
        protected readonly HorasDBContext _dbContext; //represent connection instance
        protected readonly DbSet<TEntity> _dbset; //reprsent database table in memory
        public BaseRepo(HorasDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = dbContext.Set<TEntity>();
        }


        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            #region FindAsync(),Tracking().FirstOrDefaultAsync
            // AsNoTracking().FirstOrDefaultAsync => for read-only // better peformance.
            //FindAsync(id) => for write(create-update) as it track entity changes to save it in database when call SaveChnages();
            #endregion  

            var found = await _dbset.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (found == null)
                throw new Exception($"Not old Entity with id : {id}");

            return found;
        }

        public virtual async Task<TEntity> FindAsync(Guid id)
        {
            var found = await _dbset.FindAsync(id);

            if (found == null)
                throw new Exception($"Not old Entity with id : {id}");

            return found;
        }
        #region FilterMehtod vs Expression<func<>>
        //for repeated filters : ex orderedByName  :: Make method in repo sothat if wanna change logic will change only the repo

        //for notRepeated filters  : use Expression<func<>>
        #endregion

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return await _dbset.AsNoTracking().ToListAsync();
            else return await _dbset.Where(predicate).ToListAsync();
        }


        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var created = await _dbset.AddAsync(entity);
            return created.Entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var old = await FindAsync(entity.Id);

            _dbContext.Entry(old).State = EntityState.Detached; //detach old in dbcontext (database)

            _dbset.Attach(entity); //attach coming in dbset(memory)

            _dbset.Entry(entity).State = EntityState.Modified;//to force update when saveChanges

            return entity;
        }

        public virtual async Task<TEntity> DeleteAsync(Guid id)
        {
            var found = await FindAsync(id);
            _dbset.Remove(found);

            return found;
        }

        #region Eager Loading (Include)
        //IncludeNavProperties will be override by childRepo and make include => so any method use IncludeNavProperties in BaseRepo will return Entity included with its nav properrties
        #endregion
        protected virtual IQueryable<TEntity> IncludeNavProperties(DbSet<TEntity> NavProperty)
        {
            return NavProperty;
        }
        public virtual async Task<TEntity> GetAsyncInclude(Guid id)
        {
            var found = await IncludeNavProperties(_dbset).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (found == null)
                throw new Exception($"Not Found Entity with id : {id}");

            return found;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsyncInclude(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return await IncludeNavProperties(_dbset).AsNoTracking().ToListAsync();

            return await IncludeNavProperties(_dbset).Where(predicate).ToListAsync();

        }

        public virtual async Task<TEntity> CreateAsyncInclude(TEntity entity)
        {
            var created = await CreateAsync(entity);

            return await GetAsyncInclude(created.Id);
        }

        public virtual async Task<TEntity> UpdateAsyncInclude(TEntity entity)
        {
            var updated = await UpdateAsync(entity);

            return await GetAsyncInclude(updated.Id);
        }

        public virtual async Task<TEntity> DeleteAsyncInclude(Guid id)
        {
            var deleted = await DeleteAsync(id);

            return await DeleteAsync(deleted.Id);
        }
    }
}
