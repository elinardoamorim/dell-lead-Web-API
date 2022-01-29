using Dell.Lead.WeApi.Business.Implementation;
using Dell.Lead.WeApi.Data.Converter.Converter;
using Dell.Lead.WeApi.Data.VO;
using Dell.Lead.WeApi.Exceptions;
using Dell.Lead.WeApi.Models;
using Dell.Lead.WeApi.Repositories;
using Dell.Lead.WeApi.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dell.Lead.WeApi.Test.Business
{
    public class EmployeeBusinessTest
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IValidateCpf> _mockValidateCpf;

        public EmployeeBusinessTest()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockValidateCpf = new Mock<IValidateCpf>();
        }

        public EmployeeBusinessImplementation EmployeeBusinessImplementation(Mock<IEmployeeRepository> mockEmployeeRepository)
        {
            return new EmployeeBusinessImplementation(mockEmployeeRepository.Object);
        }

        [Fact]
        public void Create()
        {
            var employeeVO = new EmployeeVO()
            {
                NameFull = "Elinardo",
                BirthDate = DateTime.Now,
                Cpf = 82284323057,
                Gender = "heterossexual",
                Phone = 8896593652,
                Address = new AddressVO()
                {
                    Street = "Rua das Flores",
                    District = "Maravilha",
                    Number = 458,
                    City = "Quixeramobim",
                    State = "Ceará",
                    Cep = 63800000
                }
            };

            var employee = new Employee()
            {
                Id = 1,
                NameFull = employeeVO.NameFull,
                BirthDate = employeeVO.BirthDate,
                Cpf = employeeVO.Cpf,
                Gender = employeeVO.Gender,
                Phone = employeeVO.Phone,
                AddressId = employeeVO.Id,
                Address = new AddressConverter().Parse(employeeVO.Address)
            };

            _mockEmployeeRepository.Setup(c => c.Create(It.IsAny<Employee>())).Returns(employee);

            var employeeBusiness = EmployeeBusinessImplementation(_mockEmployeeRepository);
            EmployeeVO result = employeeBusiness.Create(employeeVO);

            Assert.Equal(1, result.Id);
            Assert.Equal(82284323057, result.Cpf);
            Assert.Equal("Elinardo", result.NameFull);
            Assert.Equal("Rua das Flores", result.Address.Street);
            Assert.Equal(63800000, result.Address.Cep);
        }

        [Fact]
        public void CreateCpfExist()
        {
            var employeeVO = new EmployeeVO()
            {
                NameFull = "Elinardo",
                BirthDate = DateTime.Now,
                Cpf = 82284323057,
                Gender = "heterossexual",
                Phone = 8896593652,
                Address = new AddressVO()
                {
                    Street = "Rua das Flores",
                    District = "Maravilha",
                    Number = 458,
                    City = "Quixeramobim",
                    State = "Ceará",
                    Cep = 63800000
                }
            };

            _mockEmployeeRepository.Setup(c => c.FindByCpf(It.IsAny<long>())).Throws(new ExistCpfException("Já existe um funcionário com este CPF cadastrado"));

            var employeeBusiness = EmployeeBusinessImplementation(_mockEmployeeRepository);
            Action result = () => employeeBusiness.Create(employeeVO);

            ExistCpfException exception = Assert.Throws<ExistCpfException>(result);
            Assert.Equal("Já existe um funcionário com este CPF cadastrado", exception.Message);
        }
        [Fact]
        public void CreateInvalidaCpf()
        {
            var employeeVO = new EmployeeVO()
            {
                NameFull = "Elinardo",
                BirthDate = DateTime.Now,
                Cpf = 82284323025,
                Gender = "heterossexual",
                Phone = 8896593652,
                Address = new AddressVO()
                {
                    Street = "Rua das Flores",
                    District = "Maravilha",
                    Number = 458,
                    City = "Quixeramobim",
                    State = "Ceará",
                    Cep = 63800000
                }
            };

            _mockValidateCpf.Setup(c => c.IsCpf(It.IsAny<long>())).Throws(new CpfInvalidException());

            var employeeBusiness = EmployeeBusinessImplementation(_mockEmployeeRepository);
            Action result = () => employeeBusiness.Create(employeeVO);

            CpfInvalidException exception = Assert.Throws<CpfInvalidException>(result);
            Assert.Equal("CPF inválido", exception.Message);
        }
    }
}
