using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RespositoryDemo
{
    /// <summary>
    /// 仓库模式的根接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
 public   interface IRespository<T>
    {
        void Add(T item);
        void Remove(T item);
        void Update(T item);
        T FindByID(Guid id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindAll();
    }
}
