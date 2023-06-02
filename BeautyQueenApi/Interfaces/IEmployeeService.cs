using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;

namespace BeautyQueenApi.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> Get(string? search);
        Task<EmployeeDto> Post(RequestEmployeeDto employeeDto);
        Task Put(int id, RequestPutEmployeeDto employeeDto);
        Task Delete(int id);
        bool EmployeeExists(int id);
        Task<Employee?> Find(int id);
        Task<string> AddImage(IFormFile image);
        Task<IEnumerable<EmployeeDto>> GetByService(int id, string? search);
        Task SetServicesByIds(Employee employee, List<int> serviceIds);
    }
}
