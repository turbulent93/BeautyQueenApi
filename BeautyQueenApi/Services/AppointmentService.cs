using AutoMapper;
using BeautyQueenApi.Data;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyQueenApi.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AppointmentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDto>> Get()
        {
            IEnumerable<Appointment> appointments = await _context.Appointment.ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<AppointmentDto> Post(RequestAppointmentDto appointmentDto)
        {
            if(_context.Appointment == null)
            {
                throw new Exception("Context Appointment is null");
            }

            Appointment appointment = _mapper.Map<Appointment>(appointmentDto);

            _context.Appointment.Add(appointment);

            await _context.SaveChangesAsync();

            return _mapper.Map<AppointmentDto>(appointment);
        }
        public async Task Delete(int id)
        {
            if (_context.Appointment == null)
            {
                throw new Exception("Context Appointment is null");
            }

            Appointment? appointment = await Find(id);

            if(appointment == null)
            {
                throw new Exception("Appointment is not found");
            }

            _context.Appointment.Remove(appointment);

            await _context.SaveChangesAsync();
        }

        public async Task<Appointment?> Find(int id)
        {
            return await _context.Appointment.FindAsync(id);
        }

    }
}
