using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRegistrar.Models;
using ClassRegistrar.Requests;
using ClassRegistrar.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassRegistrar.Controllers
{
    #region
    /*
     * https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-7.0&tabs=visual-studio-mac
     * https://dev.to/sonyarianto/how-to-spin-mongodb-server-with-docker-and-docker-compose-2lef
     * https://www.bmc.com/blogs/mongodb-docker-container/
     * https://www.mongodb.com/try/download/shell
     * https://medium.com/@kristaps.strals/docker-mongodb-net-core-a-good-time-e21f1acb4b7b
     */
    #endregion
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IStudentRegistrationService registrationService;
        public RegistrationController(IStudentRegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationDto registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "One or More Values Where InValid"});
            }
            (bool accessed, IEnumerable<string> errors) results = await registrationService.RegisterAsync((Student)registration);
            if (!results.accessed)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = results.errors });
            }
            return Ok();
        }
    }
}
