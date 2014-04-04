using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Db.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Retrieve

        TEntity GetById(object id);
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        #endregion // Retrieve

        #region Modify

        void Insert(TEntity entity);

        void Update(TEntity entityToUpdate);

        void Delete(object id);
        void Delete(TEntity entityToDelete);

        #endregion // Modify
    }
}
