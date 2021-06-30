using EmployeesService.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesService.Controllers
{
    [Route("services/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet("v1/getAll/")]
        public ActionResult employees()
        {
            var emp = new EmployeeService().GetEmployesJson();
            return StatusCode(200, emp);
        }
    }


}
