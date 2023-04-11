﻿using AutoMapper;
using BeautyQueenApi.Data;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyQueenApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IServiceService _serviceService;
        public EmployeeService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment appEnvironment, IServiceService serviceService)
        {
            _context = context;
            _mapper = mapper;
            _appEnvironment = appEnvironment;
            _serviceService = serviceService;
        }

        public async Task<IEnumerable<EmployeeDto>> Get()
        {
            if(_context.Employee == null)
            {
                throw new Exception("Context Employee is null");
            }

            return _mapper.Map<List<EmployeeDto>>(await _context.Employee.ToListAsync());
        }

        public async Task<List<ServiceDto>> GetServicesById(int id)
        {
            Employee? employee = await _context.Employee
                .Where(e => e.Id == id)
                .Include(e => e.Services)
                .FirstOrDefaultAsync();


            if(employee == null)
            {
                throw new Exception("Employee is not found");
            }

            List<ServiceDto> services = new();

            foreach (Service service in employee.Services)
            {
                services.Add(_mapper.Map<ServiceDto>(service));
            }

            return services;
        }

        public async Task<EmployeeDto> Post(RequestEmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);

            _context.Employee.Add(employee);

            if (_serviceService == null)
            {
                throw new Exception("Service service is null");
            }

            await AddImage(employeeDto.Image);

            await _context.SaveChangesAsync();

            await SetServicesByIds(employee, employeeDto.ServiceIds);

            if (!EmployeeExists(employee.Id))
            {
                throw new Exception("Employee does not post");
            }

            return _mapper.Map<EmployeeDto>(employee);
        }
            
        public async Task Put(int id, RequestPutEmployeeDto employeeDto)
        {
            if(id != employeeDto.Id)
            {
                throw new Exception("Employee id is not equal to id");
            }
            Employee employee = _mapper.Map<Employee>(employeeDto);

            _context.Entry(employee).State = EntityState.Modified;

            await SetServicesByIds(employee, employeeDto.ServiceIds);

            await AddImage(employeeDto.Image);

            employee.Image = employeeDto.Image.FileName;
            await _context.SaveChangesAsync();

            if (!EmployeeExists(id))
            {
                throw new Exception("Employee does not change");
            }
        }

        public async Task Delete(int id)
        {
            if(_context.Employee == null)
            {
                throw new Exception("Context Employees is null");
            }

            var employee = await Find(id);
            if (employee == null)
            {
                throw new Exception("Employee is not found");
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task AddImage(IFormFile image)
        {
            string path = "/Files/" + image.FileName;

            using var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create);
            await image.CopyToAsync(fileStream);
        }

        public async Task SetServicesByIds(Employee employee, List<int> serviceIds)
        {
            if (_serviceService == null)
            {
                throw new Exception("Service service is null");
            }

            foreach (int serviceId in serviceIds)
            {
                Service? service = await _serviceService.Find(serviceId);

                if (service == null)
                {
                    throw new Exception($"Service with id {serviceId} is not found");
                }

                employee.Services.Add(service);
            }

            await _context.SaveChangesAsync();
        }
        public bool EmployeeExists(int id)
        {
            return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<Employee?> Find(int id)
        {
            return await _context.Employee.FindAsync(id);
        }
    }
}
