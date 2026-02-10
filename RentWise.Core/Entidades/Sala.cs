using System;
using System.Collections.Generic;
using System.Text;

namespace RentWise.Core.Entidades
{
    public class Sala : EntidadeBase
    {
        public Sala(string nome, string descricao, decimal precoPorHora)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome da sala não pode ser vazio.");

            if (precoPorHora <= 0)
                throw new ArgumentException("O preço por hora deve ser maior que zero.");

            Nome = nome;
            Descricao = descricao;
            PrecoPorHora = precoPorHora;
            Disponivel = true;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal PrecoPorHora { get; private set; }
        public bool Disponivel { get; private set; }

        public virtual ICollection<Reserva> Reservas { get; private set; } = new List<Reserva>();
        public void Atualizar(string nome, string descricao, decimal precoPorHora)
        {
            Nome = nome;
            Descricao = descricao;
            PrecoPorHora = precoPorHora;
        }
    }
}
