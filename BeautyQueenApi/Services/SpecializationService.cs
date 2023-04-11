using AutoMapper;
using BeautyQueenApi.Data;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyQueenApi.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SpecializationService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Delete(int id)
        {
            if(_context.Specialization == null)
            {
                throw new Exception("Context Services is null");
            }

            var specialization = await Find(id);
            if (specialization == null)
            {
                throw new Exception("Specialization is not found");
            }

            _context.Specialization.Remove(specialization);
            await _context.SaveChangesAsync();
        }

        public async Task<Specialization?> Find(int id)
        {
            return await _context.Specialization.FindAsync(id);
        }

        public async Task<IEnumerable<Specialization>> Get()
        {
            if (_context.Specialization == null)
            {
                throw new Exception("Context Specializations is null");
            }

            IEnumerable<Specialization> specializations = await _context.Specialization.ToListAsync();

            return _mapper.Map<List<Specialization>>(specializations).ToList();
        }

        public async Task<Specialization> Post(RequestSpecializationDto specializationDto)
        {
            if (_context.Specialization == null)
            {
                throw new Exception("Context Specializations is null");
            }
            Specialization specialization = _mapper.Map<Specialization>(specializationDto);

            _context.Specialization.Add(specialization);

            await _context.SaveChangesAsync();

            return _mapper.Map<Specialization>(specialization);
        }

        public async Task Put(int id, Specialization specializationDto)
        {
            Specialization specialization = _mapper.Map<Specialization>(specializationDto);

            if (id != specialization.Id)
            {
                throw new Exception("Id is not equal to specialization id");
            }

            _context.Entry(specialization).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            if (!ServiceExists(id))
            {
                throw new Exception("Specialization does not change");
            }
        }

        public bool ServiceExists(int id)
        {
            return (_context.Specialization?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
