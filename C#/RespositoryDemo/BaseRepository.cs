using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositoryDemo
{
    /// <summary>
    /// 所有仓库中相同逻辑实现的地方
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> : IRespository<T>
    {
        public virtual void Add(T item)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(T item)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T item)
        {
            throw new NotImplementedException();
        }

        public virtual T FindByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
