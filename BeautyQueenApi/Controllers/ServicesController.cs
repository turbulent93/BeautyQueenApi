﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeautyQueenApi.Data;
using BeautyQueenApi.Models;
using AutoMapper;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Interfaces;

namespace BeautyQueenApi.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetService(string? search)
        {
            try
            {
                return Ok(await _serviceService.Get(search));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("by-employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetByEmployee(int employeeId)
        {
            try
            {
                return Ok(await _serviceService.GetByEmployee(employeeId));
            } catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("by-promo/{promoId}")]
        public async Task<ActionResult<ServiceDto>> GetByService(int promoId)
        {
            try
            {
                return Ok(await _serviceService.GetByPromo(promoId));
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
            try
            {
                return Ok(await _serviceService.GetById(id));
            } catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, ServiceDto serviceDto)
        {
            try
            {
                await _serviceService.Put(id, serviceDto);
                return NoContent();
            } catch(Exception) 
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Service>> PostService(RequestServiceDto serviceDto)
        {
            try
            {
                ServiceDto service = await _serviceService.Post(serviceDto);
                return CreatedAtAction(nameof(PostService), new { id = service.Id }, service);
            } catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                await _serviceService.Delete(id);
                return NoContent();
            } catch(Exception)
            {
                return NotFound();
            }
        }
    }
}
