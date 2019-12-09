using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ADP_Bookings
{
    //This class is generic (not tied to the ADP System at hand)
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        protected DbSet<TEntity> allEntities;

        public Repository(DbContext context)
        {
            Context = context;
            allEntities = Context.Set<TEntity>();
        }

        //Retrieve record(s) from set
        public TEntity Get(int id) => allEntities.Find(id);
        public IEnumerable<TEntity> GetAll() => allEntities.ToList();
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => allEntities.Where(predicate);

        //Add to set
        public void Add(TEntity entity) => allEntities.Add(entity);
        public void AddRange(IEnumerable<TEntity> entities) => allEntities.AddRange(entities);

        //Remove from set
        public void Remove(TEntity entity) => allEntities.Remove(entity);
        public void RemoveRange(IEnumerable<TEntity> entities) => allEntities.RemoveRange(entities);
    }
}
