using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HandsOn3.Models;
using HandsOn3.Filters;

namespace HandsOn3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthFilter]
    public class EmployeeController : ControllerBase
    {
        private Employee[] emp = new Employee[]
{
        new Employee { Id=1 , Name="Nitesh" , Salary=20000 , Permanent=true, Department="CS",Skills="Dotnet" ,DateOfBirth=new DateTime(1998,08,07) },
        new Employee { Id=2 , Name="Akash" , Salary=15000 , Permanent=true, Department="Electronics",Skills="java" ,DateOfBirth=new DateTime(1999,11,09) } ,
        new Employee { Id=3 , Name="Shivam" , Salary=10000 , Permanent=false, Department="Mech",Skills="php" ,DateOfBirth=new DateTime(1997,02,12) }
};


        private IEnumerable<Employee> GetStandardEmployeeList()
        {
            return emp;
        }


        // GET: api/<ValuesController1>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return GetStandardEmployeeList();
        }

        // GET api/<ValuesController1>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var prod = emp.FirstOrDefault((p) => p.Id == id);
            if (prod == null)
            {
                return NotFound();
            }
            return Ok(prod);
        }


        // POST: api/Employee
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
