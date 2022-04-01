using api.Models;
using System.Collections.Generic;

namespace api.Repository
{
   public interface IEmployeeRepository
   {
      IEnumerable<Employee> Get();
      Employee Get(string nik);
      Employee Insert(Employee employee);
      Employee Update(Employee employee);
      Employee Delete(string nik);





   }
}
