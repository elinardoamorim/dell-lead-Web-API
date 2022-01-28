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

        private readonly Mock<IEmployeeBusiness> _mockEmployeeBusiness;

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
                        Cep = 63800000
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
                        Cep = 63800000
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
                    Cep = 63800000

                }

            };
            
            _mockEmployeeBusiness.Setup(x => x.FindByCpf(It.Is<long>(item => item.Equals(employeeVO.Cpf)))).Returns(employeeVO);

            var employeeController = EmployeeController(_mockEmployeeBusiness);

            ActionResult<EmployeeVO> response = employeeController.FindByCpf(employeeVO.Cpf);
            OkObjectResult result = (OkObjectResult)response.Result;

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(employeeVO, result.Value);
        }

        [Fact]
        public void FindInvalidCpf()
        {
            var employeeVO = new EmployeeVO()
            {
                NameFull = "Elinardo Amorim",
                Cpf = 02610260025,
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
                    Cep = 63800000

                }

            };

            _mockEmployeeBusiness.Setup(x => x.FindByCpf(It.Is<long>(item => item.Equals(employeeVO.Cpf)))).Throws(new CpfInvalidException("CPF Inválido"));

            var employeeController = EmployeeController(_mockEmployeeBusiness);

            ActionResult<EmployeeVO> response = employeeController.FindByCpf(employeeVO.Cpf);
            BadRequestObjectResult result = (BadRequestObjectResult)response.Result;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("CPF Inválido", result.Value);
        }

        [Fact]
        public void NoExistFindCpf()
        {
            var employeeVO = new EmployeeVO()
            {
                NameFull = "Elinardo Amorim",
                Cpf = 02610260025,
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
                    Cep = 63800000

                }

            };

            _mockEmployeeBusiness.Setup(x => x.FindByCpf(It.Is<long>(item => !item.Equals(employeeVO.Cpf)))).Throws(new ExistCpfException("CPF não cadastrado"));

            var employeeController = EmployeeController(_mockEmployeeBusiness);

            ActionResult<EmployeeVO> response = employeeController.FindByCpf(1425368);
            BadRequestObjectResult result = (BadRequestObjectResult)response.Result;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("CPF não cadastrado", result.Value);
        }

        [Fact]
        public void ExistFindByCpf()
        {
            var employeeVO = new EmployeeVO()
            {
                NameFull = "Elinardo Amorim",
                Cpf = 02610260025,
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
                    Cep = 63800000

                }

            };

            _mockEmployeeBusiness.Setup(x => x.FindByCpf(It.Is<long>(item => item.Equals(employeeVO.Cpf)))).Throws(new ExistCpfException("CPF já esta cadastrado"));

            var employeeController = EmployeeController(_mockEmployeeBusiness);

            ActionResult<EmployeeVO> response = employeeController.FindByCpf(employeeVO.Cpf);
            BadRequestObjectResult result = (BadRequestObjectResult)response.Result;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("CPF já esta cadastrado", result.Value);
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
                    Cep = 63800000

                }

            };

            _mockEmployeeBusiness.Setup(x => x.Create(It.IsAny<EmployeeVO>())).Returns(employeeVO);

            var employeeController = EmployeeController(_mockEmployeeBusiness);
            ActionResult<EmployeeVO> response = employeeController.Create(employeeVO);
            OkObjectResult result = (OkObjectResult)response.Result;

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(employeeVO, result.Value);
        }

        [Fact]
        public void SucessPut()
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
                    Cep = 63800000

                }

            };

            _mockEmployeeBusiness.Setup(p => p.Update(It.IsAny<EmployeeVO>())).Returns(employeeVO);

            var employeeController = EmployeeController(_mockEmployeeBusiness);

            ActionResult<EmployeeVO> response = employeeController.Put(employeeVO);
            OkObjectResult result = (OkObjectResult)response.Result;

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(employeeVO, result.Value);

        }

        [Fact]
        public void SuccessDelete()
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
                    Cep = 63800000

                }

            };

            _mockEmployeeBusiness.Setup(x => x.Delete(It.Is<long>(item => item.Equals(employeeVO.Cpf))));

            var employeeController = EmployeeController(_mockEmployeeBusiness);

            ActionResult<EmployeeVO> response = employeeController.Delete(employeeVO.Cpf);
            NoContentResult result = (NoContentResult)response.Result;

            Assert.Equal(204, result.StatusCode);
        }


    }
}
