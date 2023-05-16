using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeautyQueenApi.Data;
using BeautyQueenApi.Models;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Interfaces;

namespace BeautyQueenApi.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeService _employeeService;

        public EmployeesController(ApplicationDbContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployee()
        {
            try
            {
                return Ok(await _employeeService.Get());
            } catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}/services")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetEmployee(int id)
        {
            try
            {
                return Ok(await _employeeService.GetServicesById(id));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, [FromForm] RequestPutEmployeeDto employee)
        {
            try
            {
                await _employeeService.Put(id, employee);
                return NoContent();
            } catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee([FromForm] RequestEmployeeDto employeeDto)
        {
            try
            {
                EmployeeDto employee = await _employeeService.Post(employeeDto);
                return CreatedAtAction(nameof(PostEmployee), new { id = employee.Id }, employee);
            } catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _employeeService.Delete(id);
                return NoContent();
            } catch(Exception e) { 
                return NotFound(e.Message); 
            }
        }
    }
}
