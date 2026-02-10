using Microsoft.EntityFrameworkCore;
using RentWise.Core.Entidades;
using RentWise.Core.Interfaces;
using RentWise.Infrastructure.Context;

namespace RentWise.Infrastructure.Repositories
{
    public class SalaRepository : ISalaRepository
    {
        private readonly RentWiseDbContext _context;

        public SalaRepository(RentWiseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sala>> ObterTodasAsync()
        {
            return await _context.Salas.AsNoTracking().ToListAsync();
        }

        public async Task<Sala?> ObterPorIdAsync(int id)
        {
            return await _context.Salas.FindAsync(id);
        }

        public async Task AdicionarAsync(Sala sala)
        {
            await _context.Salas.AddAsync(sala);
            await _context.SaveChangesAsync();
        }

        public void Atualizar(Sala sala)
        {
            _context.Salas.Update(sala);
            _context.SaveChanges();
        }

        public void Remover(Sala sala)
        {
            _context.Salas.Remove(sala);
            _context.SaveChanges();
        }
    }

}