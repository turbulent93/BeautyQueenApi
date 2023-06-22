using AutoMapper;
using BeautyQueenApi.Data;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Interfaces;
using BeautyQueenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautyQueenApi.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ServiceService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Delete(int id)
        {
            if(_context.Service == null)
            {
                throw new Exception("Context Services is null");
            }

            var service = await Find(id);
            if (service == null)
            {
                throw new Exception("Service is not found");
            }

            _context.Service.Remove(service);
            await _context.SaveChangesAsync();
        }

        public async Task<Service?> Find(int id)
        {
            return await _context.Service.FindAsync(id);
        }

        public async Task<IEnumerable<ServiceDto>> Get(string? search)
        {
            if (_context.Service == null)
            {
                throw new Exception("Context Services is null");
            }

            IEnumerable<Service> services = await _context.Service.ToListAsync();

            if(search != null || search?.Length > 0)
                services = services.Where(x => x.Name.ToLower().Contains(search.ToLower()));

            return _mapper.Map<List<ServiceDto>>(services).ToList();
        }

        [HttpGet("{promoId}")]
        public async Task<IEnumerable<ServiceDto>> GetByPromo(int promoId)
        {
            var promo = _context.Promo.Find(promoId);

            if(promo == null)
            {
                throw new Exception("Promo is not found");
            }

            IEnumerable<Service> services = await _context.Service
                .Include(x => x.Promotions)
                .Where(x => x.Promotions.Contains(promo))
                .ToListAsync();

            return _mapper.Map<List<ServiceDto>>(services).ToList();
        }

        [HttpGet("{employeeId}")]
        public async Task<IEnumerable<ServiceDto>> GetByEmployee(int employeeId)
        {
            var employee = await _context.Employee.FindAsync(employeeId);

            if (employee == null)
            {
                throw new Exception("Employee is not found");
            }

            var services = await _context.Service
                .Include(x => x.Employees)
                .Where(x => x.Employees.Contains(employee))
                .ToListAsync();

            return _mapper.Map<List<ServiceDto>>(services);
        }

        public async Task<ServiceDto> GetById(int id)
        {
            if (_context.Service == null)
            {
                throw new Exception("Context Services is null");
            }
            var service = await Find(id);

            if (service == null)
            {
                throw new Exception("Service is not found");
            }

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto> Post(RequestServiceDto serviceDto)
        {
            if (_context.Service == null)
            {
                throw new Exception("Context Services is null");
            }
            Service service = _mapper.Map<Service>(serviceDto);

            _context.Service.Add(service);

            await _context.SaveChangesAsync();

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task Put(int id, ServiceDto serviceDto)
        {
            Service service = _mapper.Map<Service>(serviceDto);

            if (id != service.Id)
            {
                throw new Exception("Id is not equal to service id");
            }

            _context.Entry(service).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            if (!ServiceExists(id))
            {
                throw new Exception("Service does not change");
            }
        }

        public bool ServiceExists(int id)
        {
            return (_context.Service?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
