using Dell.Lead.WeApi.Data.Converter.Contract;
using Dell.Lead.WeApi.Data.VO;
using Dell.Lead.WeApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace Dell.Lead.WeApi.Data.Converter.Converter
{
    public class EmployeeConverter : IParse<Employee, EmployeeVO>, IParse<EmployeeVO, Employee>
    {
        public Employee Parse(EmployeeVO origin)
        {
            if(origin == null) return null;
            return new Employee
            {
                Id = origin.Id,
                NameFull = origin.NameFull,
                Cpf = origin.Cpf,
                Phone = origin.Phone,
                BirthDate = origin.BirthDate,
                Gender = origin.Gender,
                Address = new AddressConverter().Parse(origin.Address)
            };
        }

        public List<Employee> Parse(List<EmployeeVO> origins)
        {
            if(origins == null) return null;
            return origins.Select(item => Parse(item)).ToList();
        }

        public EmployeeVO Parse(Employee origin)
        {
            if(origin == null) return null;
            return new EmployeeVO
            {
                Id = origin.Id,
                NameFull = origin.NameFull,
                Cpf = origin.Cpf,
                Phone = origin.Phone,
                BirthDate = origin.BirthDate,
                Gender = origin.Gender,
                Address = new AddressConverter().Parse(origin.Address)
            };
        }

        public List<EmployeeVO> Parse(List<Employee> origins)
        {
            if (origins == null) return null;
            return origins.Select(item => Parse(item)).ToList();
        }
    }
}
