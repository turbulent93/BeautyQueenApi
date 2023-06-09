﻿using AutoMapper;
using BeautyQueenApi.Data;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Interfaces;
using BeautyQueenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyQueenApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceService _serviceService;
        private readonly IImageService _imageService;
        public EmployeeService(
            ApplicationDbContext context, 
            IMapper mapper, 
            IServiceService serviceService, 
            IImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _serviceService = serviceService;
            _imageService = imageService;
        }

        public async Task<IEnumerable<EmployeeDto>> Get(string? search)
        {
            if(_context.Employee == null)
            {
                throw new Exception("Context Employee is null");
            }

            IEnumerable<EmployeeDto> employees = _mapper.Map<List<EmployeeDto>>(
                await _context.Employee
                    .Include(x => x.Specialization)
                    .Include(x => x.Services)
                    .ToListAsync());

            if(search == null || search.Length == 0)
            {
                return employees;
            }

            return employees.Where(x => x.Name.ToLower().Contains(search.ToLower()) || 
                x.Surname.ToLower().Contains(search.ToLower()));
        }

        public async Task<IEnumerable<EmployeeDto>> GetByService(int id, string? search)
        {
            var service = await _context.Service.FindAsync(id);

            if(service == null)
            {
                throw new Exception("Service is not found");
            }

            var employees = _context.Employee
                .Include(x => x.Specialization)
                .Where(x => x.Services.Contains(service));

            if (search != null || search?.Length > 0)
                employees = employees.Where(x => x.Name.ToLower().Contains(search.ToLower()) ||
                    x.Surname.ToLower().Contains(search.ToLower()));

            return _mapper.Map<List<EmployeeDto>>(employees);

        }

        public async Task<Employee> GetById(int id)
        {
            var employee = await _context.Employee
                .Include(x => x.Services)
                .Include(x => x.Specialization)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(employee == null)
            {
                throw new Exception("Employee is not found");
            }

            return employee;
        }

        public async Task<EmployeeDto> Post(RequestEmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);

            _context.Employee.Add(employee);

            if (_serviceService == null)
            {
                throw new Exception("Service service is null");
            }
            
            _context.Entry(employee).Reference(x => x.Specialization).Load();
            _context.Entry(employee).Reference(x => x.User).Load();

            await _context.SaveChangesAsync();

            _context.Entry(employee).Collection(x => x.Services).Load();

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

            _context.Entry(employee).Collection(x => x.Services).Load();
            _context.Entry(employee).Reference(x => x.Specialization).Load();
            _context.Entry(employee).Reference(x => x.User).Load();

            await SetServicesByIds(employee, employeeDto.ServiceIds);

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

        public async Task<string> AddImage(IFormFile image)
        {
            return await _imageService.UploadImage("files/employees", image);
        }

        public async Task SetServicesByIds(Employee employee, List<int> serviceIds)
        {
            if (_serviceService == null)
            {
                throw new Exception("Service service is null");
            }

            employee.Services.Clear();

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
