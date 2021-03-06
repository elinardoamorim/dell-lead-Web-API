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
    //[Authorize("Bearer")]
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
        ///     GET /api/v1/employees/98566745060
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
        [HttpGet("find-by-cpf/{cpf}")]
        public ActionResult<EmployeeVO> FindByCpf(long cpf)
        {
            try
            {
                var employee = _employeeBusiness.FindByCpf(cpf);
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
        /// Pesquisar funcionário pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     GET /api/v1/employees/1
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
        /// <param name="id">ID do funcionário que deseja pesquisar</param>
        /// <returns>Retorna o funcionário pesquisado pelo ID</returns>
        /// <response code="200">Retorna o funcionário pesquisado</response>
        /// <response code="400">Retorna Funcionário não cadastrado</response>
        [HttpGet("find-by-id/{id}")]
        public ActionResult<EmployeeVO> FindById(long id)
        {
            var employee = _employeeBusiness.FindById(id);
            if (employee == null) return BadRequest("Funcionário não registrado");
            return Ok(employee);
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
        /// <response code="201">Retorna o novo funcionário cadastrado</response>
        /// <response code="400">Retorna CPF é inválido ou Retorna CPF não cadastrado</response>
        [HttpPost]
        public ActionResult Create([FromBody] EmployeeVO employee)
        {
            try
            {
                var newEmployee = _employeeBusiness.Create(employee);
                return CreatedAtAction("FindById", new { id = newEmployee.Id }, newEmployee);
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
        public ActionResult<EmployeeVO> Put([FromBody] EmployeeVO employee)
        {
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
