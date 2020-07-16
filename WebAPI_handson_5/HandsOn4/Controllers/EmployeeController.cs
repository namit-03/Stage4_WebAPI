using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HandsOn4.Models;
using System;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net.Http;
using System.Net;
using System.Web.Http.Controllers;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private static List<Employee> Employees = new List<Employee>();

        // GET: api/Employees 
        [HttpGet(Name = "GetAllEmployee")]
        [Authorize(Roles ="POC,Admin")]
        public IActionResult Get()
        {
            var header = Request.Headers.FirstOrDefault(h => h.Key.Equals("Authorization"));
            if (header.Value.Contains("Bearer"))
            {
                return new ObjectResult(Employees);
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET api/Employee/5 
        [HttpGet("{id}", Name = "GetEmployee")]
        [Authorize(Roles = "POC,Admin")]
        public IActionResult Get(int id)
        {
            var header = Request.Headers.FirstOrDefault(h => h.Key.Contains("Authorization"));
            if (header.Value.Contains("Bearer"))
            {
                return new ObjectResult(Employees.FirstOrDefault(p => p.Id == id));
            }
            else
            {
                return Unauthorized();
            }
            
        }

        // POST api/Employee 
        [HttpPost(Name = "CreateEmployee")]
        public IActionResult Post([FromBody]Employee Employee)
        {
            Employees.Add(Employee);
            return CreatedAtRoute("GetEmployee", new { id = Employee.Id }, Employee);
        }
        // PUT api/Employee/5 
        [HttpPut("{id}", Name = "UpdateEmployee")]
        public IActionResult Put(int id, [FromBody]Employee Employee)
        {
            var _Employee = Employees.FirstOrDefault(p => p.Id == id);
                if (id <= 0)
                {
                    return BadRequest("Invalid employee id");
                }
                else if (_Employee == null)
                {
                    return BadRequest("Employee id not found");
                }
                else
                {
                    Employees.FirstOrDefault(p => p.Id == id).Name = Employee.Name;
                    return CreatedAtRoute("GetEmployee", new { id = Employee.Id }, Employee);
                }
        }

        // DELETE api/Employee/5 
        [HttpDelete("{id}", Name = "DeleteEmployee")]
        public IActionResult Delete(int id)
        { 
            var _Employee = Employees.FirstOrDefault(p => p.Id == id);
            Employees.Remove(_Employee);
            return new NoContentResult();
        }
    }
}


