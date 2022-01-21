using Dell.Lead.WeApi.Business;
using Dell.Lead.WeApi.Data.VO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dell.Lead.WeApi.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:ApiVersion}/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBusiness _employeeBusiness;
        public EmployeeController(IEmployeeBusiness employeeBusiness)
        {
            _employeeBusiness = employeeBusiness;
        }

        [HttpGet]
        public ActionResult<List<EmployeeVO>> FindAll()
        {
            var employees = _employeeBusiness.FindAll();
            return Ok(employees);
        }

        [HttpGet("{cpf}")]
        public ActionResult<EmployeeVO> FindByCpf(long cpf)
        {
            var employee = _employeeBusiness.FindByCpf(cpf);
            if (employee == null) return NotFound("Invalid employee");
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult Create([FromBody] EmployeeVO employee)
        {
            var newEmployee = _employeeBusiness.Create(employee);
            if (newEmployee == null) return BadRequest("Error when registering employee");
            return Ok(newEmployee);
        }

        [HttpPut]
        public ActionResult Put([FromBody] EmployeeVO employee)
        {
            var changerEmployee = _employeeBusiness.Update(employee);
            if (changerEmployee == null) return BadRequest("Error updating employee");
            return Ok(changerEmployee);
        }

        [HttpDelete("{cpf}")]
        public ActionResult Delete(long cpf)
        {
            var employee = _employeeBusiness.FindByCpf(cpf);
            if (employee == null) return NotFound("Invalid employee");
            _employeeBusiness.Delete(cpf);
            return Ok("Record deleted successfully");
        }
    }
}
