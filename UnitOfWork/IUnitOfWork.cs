using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Db.Repository;

namespace Common.Db.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TModel> CreateRepository<TModel>() where TModel : class;
        void Save();
    }
}
