using RentWise.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentWise.Core.Interfaces
{
    public interface ISalaRepository
    {
        Task<IEnumerable<Sala>> ObterTodasAsync();
        Task<Sala?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Sala sala);
        void Atualizar(Sala sala);
        void Remover(Sala sala);
    }
}
