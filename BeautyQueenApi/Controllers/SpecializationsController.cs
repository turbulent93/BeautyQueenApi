using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeautyQueenApi.Data;
using BeautyQueenApi.Models;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Interfaces;

namespace BeautyQueenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationsController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialization>>> GetSpecialization()
        {
            try
            {
                return Ok(await _specializationService.Get());
            } catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialization(int id, Specialization specialization)
        {
            try
            {
                await _specializationService.Put(id, specialization);
                return NoContent();
            } catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Specialization>> PostSpecialization(RequestSpecializationDto specializationDto)
        {
            try
            {
                Specialization specialization = await _specializationService.Post(specializationDto);
                return CreatedAtAction("GetSpecialization", new { id = specialization.Id }, specialization);
            } catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            try
            {
                await _specializationService.Delete(id);
                return NoContent();
            } catch(Exception)
            {
                return NotFound();
            }
        }
    }
}
