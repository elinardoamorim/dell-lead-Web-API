using Dell.Lead.WeApi.Data.Converter.Converter;
using Dell.Lead.WeApi.Data.VO;
using Dell.Lead.WeApi.Exceptions;
using Dell.Lead.WeApi.Repositories;
using Dell.Lead.WeApi.Util;
using System;
using System.Collections.Generic;

namespace Dell.Lead.WeApi.Business.Implementation
{
    public class EmployeeBusinessImplementation : IEmployeeBusiness
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeConverter _converter;
        private readonly IValidateCpf _validateCpf;

        public EmployeeBusinessImplementation(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _validateCpf = new ValidateCpf();
            _converter = new EmployeeConverter();
        }
        public EmployeeVO Create(EmployeeVO employee)
        {
            var employeeEntity = _converter.Parse(employee);

            if(_employeeRepository.FindByCpf(employee.Cpf) != null)
            {
                throw new ExistCpfException("Já existe um funcionário com este CPF cadastrado");
            } 
            else if (!_validateCpf.IsCpf(employeeEntity.Cpf))
            {
                throw new CpfInvalidException("CPF inválido");
            } 
            else
            {
                employeeEntity = _employeeRepository.Create(employeeEntity);
                return _converter.Parse(employeeEntity);
            }
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
            if (!_validateCpf.IsCpf(cpf)) throw new CpfInvalidException("CPF Inválido");
            var findEmployee = _employeeRepository.FindByCpf(cpf);
            if (findEmployee == null) throw new ExistCpfException("Não existe funcionário cadastrado com este CPF");
            return _converter.Parse(findEmployee);
        }

        public EmployeeVO Update(EmployeeVO employee)
        {
            var employeeEntity = _converter.Parse(employee);
            employeeEntity.AddressId = employee.Address.Id;
            if(FindByCpf(employeeEntity.Cpf) != null)
            {
                employeeEntity = _employeeRepository.Update(employeeEntity);
                return _converter.Parse(employeeEntity);
            }
            return null;
        }
    }
}
