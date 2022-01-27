using Dell.Lead.WeApi.Business;
using Dell.Lead.WeApi.Controllers;
using Dell.Lead.WeApi.Data.VO;
using Dell.Lead.WeApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dell.Lead.WeApi.Test.Controllers
{
    public class EmployeeControllerTest
    {

        private Mock<IEmployeeBusiness> _mockEmployeeBusiness;

        public EmployeeControllerTest()
        {
            _mockEmployeeBusiness = new Mock<IEmployeeBusiness>();

        }

        private EmployeeController EmployeeController(Mock<IEmployeeBusiness> mockEmployeeBusiness)
        {
            return new EmployeeController(mockEmployeeBusiness.Object);

        }

        [Fact]
        public void FindAll()
        {
            List<EmployeeVO> employees = new List<EmployeeVO>()
            {
                new EmployeeVO()
                {
                    NameFull = "Elinardo Amorim",
                    Cpf = 02610260032,
                    BirthDate = Convert.ToDateTime("1987-12-21T00:00:00"),
                    Gender = "Heterossexual",
                    Phone = 88992157596,
                    Address = new AddressVO()
                    {
                        Street = "Rua Dona Elisa Elpidio",
                        Number = 541,
                        District = "Maravilha",
                        City = "Quixeramobim",
                        State = "Ceará",
                        Cep = "63800-000"
                    }
                },
                new EmployeeVO()
                {
                    NameFull = "Elinardo Amorim",
                    Cpf = 02610260032,
                    BirthDate = Convert.ToDateTime("1987-12-21T00:00:00"),
                    Gender = "Heterossexual",
                    Phone = 88992157596,
                    Address = new AddressVO()
                    {
                        Street = "Rua Dona Elisa Elpidio",
                        Number = 541,
                        District = "Maravilha",
                        City = "Quixeramobim",
                        State = "Ceará",
                        Cep = "63800-000"
                    }
                }
            };

            _mockEmployeeBusiness.Setup(x => x.FindAll()).Returns(employees);

            var employeeController = EmployeeController(_mockEmployeeBusiness);
            ActionResult<List<EmployeeVO>> response = employeeController.FindAll();
            OkObjectResult result = (OkObjectResult)response.Result;

            Assert.Equal(200, result.StatusCode);

        }

        [Fact]
        public void FindByCpf()
        {
            var employeeVO = new EmployeeVO()
            {
                NameFull = "Elinardo Amorim",
                Cpf = 02610260032,
                BirthDate = Convert.ToDateTime("1987-12-21T00:00:00"),
                Gender = "Heterossexual",
                Phone = 88992157596,
                Address = new AddressVO()
                {
                    Street = "Rua Dona Elisa Elpidio",
                    Number = 541,
                    District = "Maravilha",
                    City = "Quixeramobim",
                    State = "Ceará",
                    Cep = "63800-000"

                }

            };
            
            _mockEmployeeBusiness.Setup(x => x.FindByCpf(It.Is<long>(item => item.Equals(employeeVO.Cpf)))).Returns(employeeVO);

            var employeeController = EmployeeController(_mockEmployeeBusiness);

            ActionResult<EmployeeVO> response = employeeController.FindByCpf(employeeVO.Cpf);
            OkObjectResult result = (OkObjectResult)response.Result;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Create()
        {
            var employeeVO = new EmployeeVO()
            {
                NameFull = "Elinardo Amorim",
                Cpf = 02610260032,
                BirthDate = Convert.ToDateTime("1987-12-21T00:00:00"),
                Gender = "Heterossexual",
                Phone = 88992157596,
                Address = new AddressVO()
                {
                    Street = "Rua Dona Elisa Elpidio",
                    Number = 541,
                    District = "Maravilha",
                    City = "Quixeramobim",
                    State = "Ceará",
                    Cep = "63800-000"

                }

            };

            _mockEmployeeBusiness.Setup(x => x.Create(It.IsAny<EmployeeVO>())).Returns(employeeVO);

            var employeeController = EmployeeController(_mockEmployeeBusiness);
            ActionResult<EmployeeVO> response = employeeController.Create(employeeVO);
            OkObjectResult result = (OkObjectResult)response.Result;

            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void FailCreateEmployeeNull()
        {
            EmployeeVO employeeVO = null;

            _mockEmployeeBusiness.Setup(x => x.Create(It.IsAny<EmployeeVO>())).Returns(employeeVO);

            var employeeController = EmployeeController(_mockEmployeeBusiness);

            ActionResult<EmployeeVO> response = employeeController.Create(employeeVO);
            BadRequestObjectResult result = (BadRequestObjectResult)response.Result;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Funcionário inválido", result.Value);
        }

        [Fact]
        public void FailCreateEmployeeExist()
        {
            var employeeVO = new EmployeeVO()
            {
                NameFull = "Ruan Bezerra",
                Cpf = 97377401060,
                BirthDate = Convert.ToDateTime("1998-12-15T00:00:00"),
                Gender = "Heterossexual",
                Phone = 88992157678,
                Address = new AddressVO()
                {
                    Street = "Rua 14 de Agosto",
                    Number = 25,
                    District = "Centro",
                    City = "Quixeramobim",
                    State = "Ceará",
                    Cep = "63800-000"

                }

            };

            _mockEmployeeBusiness.Setup(x => x.Create(It.IsAny<EmployeeVO>())).Throws(new ExistCpfException("Já existe um funcionário com este CPF cadastrado"));


            var employeeController = EmployeeController(_mockEmployeeBusiness);

            ActionResult<EmployeeVO> response = employeeController.Create(employeeVO);

            BadRequestObjectResult result = (BadRequestObjectResult)response.Result;

            Assert.Equal(400, result.StatusCode);

            Assert.Equal("Já existe um funcionário com este CPF cadastrado", result.Value);
        }

        [Fact]
        public void FailCreateEmployeeCpfInvalid()
        {
            var employeeVO = new EmployeeVO()
            {
                NameFull = "Ruan Bezerra",
                Cpf = 97377401060,
                BirthDate = Convert.ToDateTime("1998-12-15T00:00:00"),
                Gender = "Heterossexual",
                Phone = 88992157678,
                Address = new AddressVO()
                {
                    Street = "Rua 14 de Agosto",
                    Number = 25,
                    District = "Centro",
                    City = "Quixeramobim",
                    State = "Ceará",
                    Cep = "63800-000"

                }

            };

            _mockEmployeeBusiness.Setup(x => x.Create(It.IsAny<EmployeeVO>())).Throws(new CpfInvalidException("Já existe um funcionário com este CPF cadastrado"));


            var employeeController = EmployeeController(_mockEmployeeBusiness);

            ActionResult<EmployeeVO> response = employeeController.Create(employeeVO);

            BadRequestObjectResult result = (BadRequestObjectResult)response.Result;

            Assert.Equal(400, result.StatusCode);

            Assert.Equal("Já existe um funcionário com este CPF cadastrado", result.Value);
        }


    }
}
