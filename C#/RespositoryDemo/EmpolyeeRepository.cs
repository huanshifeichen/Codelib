using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositoryDemo
{
    /// <summary>
    /// 最终的仓库实现，它需要继承自基础仓库实现，并实现自己这个特定仓库的接口
    /// 通过这样的分离，就保证了，如果要为所有参考都添加的功能，就在IRepository<T>中添加声明，在BaseRepository<T>中实现
    /// 如果只是为了特定的仓库添加功能，就在[I实体Repository]声明，在[实体Repository]中实现
    /// 
    /// 只是这样做需要为每个参考都添加它的[I实体Repository]。这最好通过代码生成器来做。
    /// </summary>
   public class EmpolyeeRepository : BaseRepository<Employee>,IEmployeeRepository
    {
        public void UpdateStatus(Employee e)
        {
            throw new NotImplementedException();
        }
    }
}
