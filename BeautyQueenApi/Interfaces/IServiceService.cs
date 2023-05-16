using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;

namespace BeautyQueenApi.Interfaces
{
    public interface IServiceService
    {
        Task<Service?> Find(int id);
        Task<ServiceDto> GetById(int id);
        Task<IEnumerable<ServiceDto>> Get();
        Task Put(int id, ServiceDto serviceDto);
        Task<ServiceDto> Post(RequestServiceDto serviceDto);
        Task Delete(int id);
        bool ServiceExists(int id);
    }
}
