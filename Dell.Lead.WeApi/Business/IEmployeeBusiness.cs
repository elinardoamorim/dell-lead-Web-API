using Dell.Lead.WeApi.Data.VO;
using System.Collections.Generic;

namespace Dell.Lead.WeApi.Business
{
    public interface IEmployeeBusiness
    {
        EmployeeVO FindByCpf(int cpf);
        List<EmployeeVO> FindAll();
        EmployeeVO Create(EmployeeVO employee);
        EmployeeVO Update(EmployeeVO employee);
        void Delete(int cpf);

    }
}
