using Microsoft.EntityFrameworkCore;
using OrbitBook.Application.Interfaces.Repositories;
using OrbitBook.Domain.Entities;
using OrbitBook.Infrastructure.Data;

namespace OrbitBook.Infrastructure.Repositories
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly OrbitBookDbContext _context;

        public DestinationRepository(OrbitBookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            return await _context.Destinations
                .Include(d => d.Type)
                .ToListAsync();
        }

        public async Task<Destination?> GetByIdAsync(int id)
        {
            return await _context.Destinations
                .Include(d => d.Type)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Destination> AddAsync(Destination destination)
        {
            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();
            return destination;
        }

        public async Task UpdateAsync(Destination destination)
        {
            _context.Destinations.Update(destination);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Destination destination)
        {
            _context.Destinations.Remove(destination);
            await _context.SaveChangesAsync();
        }
    }
}