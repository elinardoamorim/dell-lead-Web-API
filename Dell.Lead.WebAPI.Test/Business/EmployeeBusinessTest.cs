using Dell.Lead.WeApi.Business.Implementation;
using Dell.Lead.WeApi.Data.Converter.Converter;
using Dell.Lead.WeApi.Data.VO;
using Dell.Lead.WeApi.Models;
using Dell.Lead.WeApi.Repositories;
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

        public EmployeeBusinessTest()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
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
    }
}
