using System;
using System.Collections.Generic;
using System.Text;

namespace RentWise.Application.DTOs
{
   public record SalaRequest(string Nome, string Descricao, decimal PrecoPorHora);
}
