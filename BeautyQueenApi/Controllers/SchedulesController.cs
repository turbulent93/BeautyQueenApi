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
    [Route("api/sschedules")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedule()
        {
            try
            {
                return Ok(await _scheduleService.Get());
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Schedule>> PostSchedule(RequestScheduleDto scheduleDto)
        {
            try
            {
                ScheduleDto schedule = await _scheduleService.Post(scheduleDto);

                return CreatedAtAction(nameof(PostSchedule), new { id = schedule.Id }, schedule);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            try
            {
                await _scheduleService.Delete(id);

                return NoContent();
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
