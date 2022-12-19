using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ClassRegistrar.Models;
using ClassRegistrar.Requests;
using ClassRegistrar.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace ClassRegistrar.Controllers
{
    #region Helpful Links
    /*
     * https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-7.0&tabs=visual-studio-mac
     * https://dev.to/sonyarianto/how-to-spin-mongodb-server-with-docker-and-docker-compose-2lef
     * https://www.bmc.com/blogs/mongodb-docker-container/
     * https://www.mongodb.com/try/download/shell
     * https://medium.com/@kristaps.strals/docker-mongodb-net-core-a-good-time-e21f1acb4b7b
     * https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-7.0
     * https://stackoverflow.com/questions/29216534/how-do-i-set-multiple-headers-using-postasync-in-c
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationDto registration)
        {
            if (!ModelState.IsValid
                || registration.Courses == default
                || !registration.Courses.Any()
                || registration.Courses.Count() < 4)
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
