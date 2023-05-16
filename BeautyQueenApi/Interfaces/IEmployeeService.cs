using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;

namespace BeautyQueenApi.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> Get();
        Task<EmployeeDto> Post(RequestEmployeeDto employeeDto);
        Task Put(int id, RequestPutEmployeeDto employeeDto);
        Task Delete(int id);
        bool EmployeeExists(int id);
        Task<Employee?> Find(int id);
        Task AddImage(IFormFile image);
        Task<List<ServiceDto>> GetServicesById(int id);
        Task SetServicesByIds(Employee employee, List<int> serviceIds);
    }
}
