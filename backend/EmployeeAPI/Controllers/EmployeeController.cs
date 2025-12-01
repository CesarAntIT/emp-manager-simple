using EmployeeAPI.Data.Entities;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var empList = _service.Get();
            if (!empList.success)
            {
                return NotFound(empList);
            }
            return Ok(empList);
        }

        [HttpGet]
        [Route("deps")]
        public IActionResult GetDepartments()
        {
            var list = _service.GetDepartments();
            return Ok(list);
        }

        [HttpGet]
        [Route("byId")]
        public IActionResult GetEmployeeId(Guid id)
        {
            var emp = _service.GetById(id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee emp)
        {
            var newEmp = _service.Add(emp);
            if (!newEmp.success)
                return BadRequest(newEmp);
            return Ok(newEmp);
        }

        [HttpDelete]
        public IActionResult RemoveEmployee(Guid Id)
        {
            var result = _service.Remove(Id);
            if (result.success == false)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult EditEmployee(Employee emp)
        {
            var UpdEmp = _service.Edit(emp.ID,emp);
            if (UpdEmp == null)
                return BadRequest();
            return Ok(UpdEmp);
        }
    }
}
