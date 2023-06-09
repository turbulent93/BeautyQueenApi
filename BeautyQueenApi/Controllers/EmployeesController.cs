﻿using System;
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
        private readonly IImageService _imageService;

        public EmployeesController(ApplicationDbContext context, IEmployeeService employeeService, IImageService imageService)
        {
            _context = context;
            _employeeService = employeeService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployee(string? search)
        {
            try
            {
                return Ok(await _employeeService.Get(search));
            } catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("by-service/{id}")]
        public async Task<ActionResult<EmployeeDto>> GetByService(int id, string? search)
        {
            try
            {
                return Ok(await _employeeService.GetByService(id, search));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            try
            {
                return Ok(await _employeeService.GetById(id));
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("image")]
        public async Task<ActionResult<string>> PostImage([FromForm] IFormFile image)
        {
            try
            {
                return Ok(await _imageService.UploadImage("/files/employees", image));
            } catch (Exception e)
            {
                return BadRequest(e.Message);
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
