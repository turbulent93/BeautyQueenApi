using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;

namespace BeautyQueenApi.Interfaces
{
    public interface ISpecializationService
    {
        Task<Specialization?> Find(int id);
        Task<IEnumerable<Specialization>> Get();
        Task Put(int id, Specialization specialization);
        Task<Specialization> Post(RequestSpecializationDto specializationDto);
        Task Delete(int id);
        bool ServiceExists(int id);
    }
}
