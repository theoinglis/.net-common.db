using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Common.Db.Repository;

namespace Common.Db.EntityFramework
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IEfContext _context;
        private readonly DbSet<TEntity> _table;

        public Repository(IEfContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }


        public virtual TEntity GetById(object id)
        {
            return _table.Find(id);
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual void Insert(TEntity entity)
        {
            _table.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _table.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _table.Attach(entityToDelete);
            }
            _table.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _table.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
