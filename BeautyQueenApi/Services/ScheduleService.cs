using AutoMapper;
using BeautyQueenApi.Data;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Interfaces;
using BeautyQueenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyQueenApi.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ScheduleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleDto>> Get(int id, int? year, int? month)
        {
            if(_context.Schedule == null)
            {
                throw new Exception("Context Scheudle is null");
            }

            IEnumerable<Schedule> schedules = await _context.Schedule
                .ToListAsync();
            
            if(year != null && month != null)
            {
                schedules = schedules.Where(x => x.EmployeeId == id && x.Date.Year == year && x.Date.Month == month);
            } else
            {
                var curDate = DateOnly.FromDateTime(DateTime.Today);
                var date = DateOnly.FromDateTime(DateTime.Today.AddDays(10));

                schedules = schedules.Where(x => x.EmployeeId == id && 
                    x.Date >= curDate && x.Date <= date);
            }

            return _mapper.Map<IEnumerable<ScheduleDto>>(schedules);
        }
        public async Task<ScheduleDto> Post(RequestScheduleDto scheduleDto)
        {
            if (_context.Schedule == null)
            {
                throw new Exception("Context Scheudle is null");
            }

            Schedule schedule = _mapper.Map<Schedule>(scheduleDto);

            _context.Schedule.Add(schedule);

            await _context.SaveChangesAsync();

            if (!ServiceExists(schedule.Id))
            {
                throw new Exception("Service does not add");
            }

            return _mapper.Map<ScheduleDto>(schedule);
        }

        public async Task Delete(int id)
        {
            Schedule? schedule = await Find(id);

            if (schedule == null)
            {
                throw new Exception("Schedule is not found");
            }

            _context.Schedule.Remove(schedule);

            await _context.SaveChangesAsync();
        }

        public async Task<Schedule?> Find(int id)
        {
            return await _context.Schedule.FindAsync(id);
        }

        public bool ServiceExists(int id)
        {
            return (_context.Schedule?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IEnumerable<Schedule>> GetByEmployee(int id)
        {
            return await _context.Schedule.Where(x => x.EmployeeId == id).ToListAsync();
        }
    }
}
