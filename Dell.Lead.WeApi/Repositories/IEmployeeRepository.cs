using Dell.Lead.WeApi.Models;
using System.Collections.Generic;

namespace Dell.Lead.WeApi.Repositories
{
    public interface IEmployeeRepository
    {
        Employee FindByCpf(long cpf);
        List<Employee> FindAll();
        Employee Create(Employee employee);
        Employee Update(Employee employee);
        void Delete(long cpf);
        Employee FindById(long id);
    }
}
