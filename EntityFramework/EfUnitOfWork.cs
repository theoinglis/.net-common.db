using System;
using System.Data.Entity;
using Common.Db.Repository;
using Common.Db.UnitOfWork;

namespace Common.Db.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly IEfContext _dbContext;

        public EfUnitOfWork(IEfContext dbContext)
        {
            _dbContext = dbContext;
        }
   
        public IRepository<TModel> CreateRepository<TModel>() where TModel : class
        {
            return new Repository<TModel>(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
