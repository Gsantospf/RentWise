using Microsoft.EntityFrameworkCore;
using RentWise.Core.Entidades;
using RentWise.Core.Interfaces;
using RentWise.Infrastructure.Context;

namespace RentWise.Infrastructure.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly RentWiseDbContext _context;

        public ReservaRepository(RentWiseDbContext context)
        {
            _context = context;
        }
        public async Task<Reserva?> ObterPorIdAsync(int id)
        {
            return await _context.Reservas
                .Include(r => r.Sala) // Traz os dados da sala junto
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<IEnumerable<Reserva>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim)
        {
            return await _context.Reservas
                .Where(r => r.Inicio >= inicio && r.Fim <= fim)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task AdicionarAsync(Reserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Reserva reserva)
        {
            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExisteConflitoAsync(int salaId, DateTime inicio, DateTime fim)
        {
            return await _context.Reservas
                .AnyAsync(r => r.SalaId == salaId &&
                               r.Status != "Cancelada" && 
                               inicio < r.Fim &&
                               fim > r.Inicio);
        }
    }
}