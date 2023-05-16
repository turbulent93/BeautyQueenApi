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
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appoinmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appoinmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointment()
        {
            try
            {
                return Ok(await _appoinmentService.Get());
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> PostAppointment(RequestAppointmentDto appointmentDto)
        {
            try
            {
                AppointmentDto appointment = await _appoinmentService.Post(appointmentDto);

                return CreatedAtAction(nameof(PostAppointment), new { id = appointment.Id }, appointment);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                await _appoinmentService.Delete(id);

                return NoContent();
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
