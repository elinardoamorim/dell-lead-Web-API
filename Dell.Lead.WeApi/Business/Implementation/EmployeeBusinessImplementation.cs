using Dell.Lead.WeApi.Data.Converter.Converter;
using Dell.Lead.WeApi.Data.VO;
using Dell.Lead.WeApi.Repositories;
using System.Collections.Generic;

namespace Dell.Lead.WeApi.Business.Implementation
{
    public class EmployeeBusinessImplementation : IEmployeeBusiness
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeConverter _converter;

        public EmployeeBusinessImplementation(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _converter = new EmployeeConverter();
        }
        public EmployeeVO Create(EmployeeVO employee)
        {
            var employeeEntity = _converter.Parse(employee);
            employeeEntity = _employeeRepository.Create(employeeEntity);
            return _converter.Parse(employeeEntity);
        }

        public void Delete(long cpf)
        {
            _employeeRepository.Delete(cpf);
        }

        public List<EmployeeVO> FindAll()
        {
            return _converter.Parse(_employeeRepository.FindAll());
        }

        public EmployeeVO FindByCpf(long cpf)
        {
            return _converter.Parse(_employeeRepository.FindByCpf(cpf));
        }

        public EmployeeVO Update(EmployeeVO employee)
        {
            var employeeEntity = _converter.Parse(employee);
            employeeEntity = _employeeRepository.Update(employeeEntity);
            return _converter.Parse(employeeEntity);
        }
    }
}
