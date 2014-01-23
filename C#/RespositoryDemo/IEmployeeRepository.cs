using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositoryDemo
{
    /// <summary>
    /// 特定仓库的接口
    /// </summary>
  public  interface IEmployeeRepository : IRespository<Employee>
    {
      void UpdateStatus(Employee e);
    }
}
