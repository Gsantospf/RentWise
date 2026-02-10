using RentWise.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentWise.Core.Interfaces
{
    public interface IReservaRepository
    {
        Task<Reserva?> ObterPorIdAsync(int id);
        Task<IEnumerable<Reserva>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim);
        Task AdicionarAsync(Reserva reserva);
        Task AtualizarAsync(Reserva reserva);
        Task<bool> ExisteConflitoAsync(int salaId, DateTime inicio, DateTime fim);
    }
}
