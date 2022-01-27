using Dell.Lead.WeApi.Business;
using Dell.Lead.WeApi.Data.VO;
using Dell.Lead.WeApi.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Dell.Lead.WeApi.Controllers
{
    [ApiVersion("1")]
    [Authorize("Bearer")]
    [ApiController]
    [Route("api/v{version:ApiVersion}/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBusiness _employeeBusiness;
        public EmployeeController(IEmployeeBusiness employeeBusiness)
        {
            _employeeBusiness = employeeBusiness;
        }
        /// <summary>
        /// Lista todos os funcionários.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     GET /api/v1/employees
        ///   
        ///        {
        ///        "code": 0,
        ///        "name_full": "string",
        ///        "cpf": 0,
        ///        "birth_date": "2022-01-24T16:03:16.910Z",
        ///        "phone": 0,
        ///        "gender": "string",
        ///        "address": {
        ///          "code": 0,
        ///          "street": "string",
        ///          "number": 0,
        ///          "district": "string",
        ///          "city": "string",
        ///          "state": "string",
        ///          "cep": "string"
        ///        }
        ///      }
        ///    
        ///
        /// </remarks>
        /// <returns>Lista de funcionários</returns>
        /// <response code="200">Retorna uma lista de funcionários</response>
        [HttpGet]
        public ActionResult<List<EmployeeVO>> FindAll()
        {
            var employees = _employeeBusiness.FindAll();
            return Ok(employees);
        }

        /// <summary>
        /// Pesquisar funcionário pelo cpf.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     GET /api/v1/employees/CPF
        ///   
        ///        {
        ///        "code": 0,
        ///        "name_full": "string",
        ///        "cpf": 0,
        ///        "birth_date": "2022-01-24T16:03:16.910Z",
        ///        "phone": 0,
        ///        "gender": "string",
        ///        "address": {
        ///          "code": 0,
        ///          "street": "string",
        ///          "number": 0,
        ///          "district": "string",
        ///          "city": "string",
        ///          "state": "string",
        ///          "cep": "string"
        ///        }
        ///      }
        ///    
        ///
        /// </remarks>
        /// <param name="cpf">CPF do funcionário que deseja pesquisar</param>
        /// <returns>Retorna o funcionário pesquisado pelo cpf</returns>
        /// <response code="200">Retorna o funcionário pesquisado</response>
        /// <response code="400">Retorna CPF é inválido ou Retorna CPF não cadastrado</response>
        [HttpGet("{cpf}")]
        public ActionResult<EmployeeVO> FindByCpf(long cpf)
        {
            try
            {
                var employee = _employeeBusiness.FindByCpf(cpf);
                if(employee == null) return BadRequest();
                return Ok(employee);
            }
            catch(CpfInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ExistCpfException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Cadastrar funcionário.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /api/v1/employees/
        ///   
        ///        {
        ///        "code": 0,
        ///        "name_full": "string",
        ///        "cpf": 0,
        ///        "birth_date": "2022-01-24T16:03:16.910Z",
        ///        "phone": 0,
        ///        "gender": "string",
        ///        "address": {
        ///          "code": 0,
        ///          "street": "string",
        ///          "number": 0,
        ///          "district": "string",
        ///          "city": "string",
        ///          "state": "string",
        ///          "cep": "string"
        ///        }
        ///      }
        ///    
        ///
        /// </remarks>
        /// <returns>Retorna o funcionário cadastrado</returns>
        /// <response code="200">Retorna o novo funcionário cadastrado</response>
        /// <response code="400">Retorna CPF é inválido ou Retorna CPF não cadastrado</response>
        [HttpPost]
        public ActionResult Create([FromBody] EmployeeVO employee)
        {
            if (employee == null) return BadRequest("Funcionário inválido");
            try
            {
                var newEmployee = _employeeBusiness.Create(employee);
                return Ok(newEmployee);
            }
            catch(ExistCpfException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CpfInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualizar cadastro do funcionário.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT /api/v1/employees/
        ///   
        ///        {
        ///        "code": 0,
        ///        "name_full": "string",
        ///        "cpf": 0,
        ///        "birth_date": "2022-01-24T16:03:16.910Z",
        ///        "phone": 0,
        ///        "gender": "string",
        ///        "address": {
        ///          "code": 0,
        ///          "street": "string",
        ///          "number": 0,
        ///          "district": "string",
        ///          "city": "string",
        ///          "state": "string",
        ///          "cep": "string"
        ///        }
        ///      }
        ///    
        ///
        /// </remarks>
        /// <returns>Retorna o funcionário com as informações atualizadas</returns>
        /// <response code="200">Retorna o funcionário com as informações atualizadas</response>
        /// <response code="400">Retorna CPF é inválido ou Retorna CPF não cadastrado</response>
        [HttpPut]
        public ActionResult Put([FromBody] EmployeeVO employee)
        {
            if (employee == null) return BadRequest("Funcionário inválido");
            try
            {
                var changerEmployee = _employeeBusiness.Update(employee);
                return Ok(changerEmployee);
            }
            catch(ExistCpfException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(CpfInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Deleta cadastro do funcionário pesquisado pelo cpf.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     DELETE /api/v1/employees/CPF
        ///   
        ///        {
        ///        "code": 0,
        ///        "name_full": "string",
        ///        "cpf": 0,
        ///        "birth_date": "2022-01-24T16:03:16.910Z",
        ///        "phone": 0,
        ///        "gender": "string",
        ///        "address": {
        ///          "code": 0,
        ///          "street": "string",
        ///          "number": 0,
        ///          "district": "string",
        ///          "city": "string",
        ///          "state": "string",
        ///          "cep": "string"
        ///        }
        ///      }
        ///    
        ///
        /// </remarks>
        /// <param name="cpf">CPF do funcionário que deseja deletar o cadastro</param>
        /// <returns>Retorna que o funcionário foi deletado</returns>
        /// <response code="201">Retorna que o cadastro do funcionário foi deletado</response>
        /// <response code="400">Retorna CPF é inválido ou Retorna CPF não cadastrado</response>
        [HttpDelete("{cpf}")]
        public ActionResult Delete(long cpf)
        {

            try
            {
                var employee = _employeeBusiness.FindByCpf(cpf);
                if (employee == null) return BadRequest();
                _employeeBusiness.Delete(cpf);
                return NoContent();
            }
            catch (CpfInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(ExistCpfException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
