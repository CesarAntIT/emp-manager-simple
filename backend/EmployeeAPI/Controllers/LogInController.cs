using EmployeeAPI.Data.Entities;
using EmployeeAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        ILoginService _service = null!;
        public LogInController(ILoginService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult LogIn(string username, string password)
        {
            var user = _service.LogIn(username, password);
            if (!user.success)
                return BadRequest(user);

            return Ok(user);
        }

        [HttpGet]
        [Route("/confirm-privilage")]

        public IActionResult CheckPrivilage(Guid ID)
        {
            var user = _service.CheckPrivilage(ID);
            if (!user.success)
                return BadRequest(user);

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Register(User newuser)
        {
            var user = _service.Register(newuser);
            if (!user.success)
                return BadRequest(user);
            return Ok(user);
        }
    }
}
