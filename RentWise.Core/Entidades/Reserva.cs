using System;
using System.Text.Json.Serialization;

namespace RentWise.Core.Entidades
{
    public class Reserva : EntidadeBase
    {
        protected Reserva() { }

        public Reserva(int salaId, int usuarioId, DateTime inicio, DateTime fim)
        {
            if (inicio < DateTime.Now)
                throw new ArgumentException("A data de início não pode ser no passado.");

            if (fim <= inicio)
                throw new ArgumentException("A data de término deve ser posterior à data de início.");

            SalaId = salaId;
            UsuarioId = usuarioId;
            Inicio = inicio;
            Fim = fim;
            Status = "Pendente";
        }

        public int SalaId { get; private set; }
        public int UsuarioId { get; private set; }
        public DateTime Inicio { get; private set; }
        public DateTime Fim { get; private set; }
        public decimal ValorTotal { get; private set; }
        public string Status { get; private set; }
       
        [JsonIgnore]
        public virtual Sala? Sala { get; private set; }
        
        // --- MÉTODOS DE NEGÓCIO ---

        private decimal CalcularValorFinal(decimal precoPorHora)
        {
            var duracao = Fim - Inicio;
            var horasTotais = (decimal)Math.Ceiling(duracao.TotalHours);
            return horasTotais * precoPorHora;
        }

        // Método público que o Controller chama
        public void Confirmar(decimal precoPorHora)
        {
            ValorTotal = CalcularValorFinal(precoPorHora);
            Status = "Confirmada";
        }

        public void CancelarReserva()
        {
            if (Inicio < DateTime.Now.AddHours(2))
                throw new InvalidOperationException("Reservas só podem ser canceladas com 2h de antecedência.");

            Status = "Cancelada";
            Desativar();
        }
    }
}