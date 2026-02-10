using System;
using System.Collections.Generic;
using System.Text;

namespace RentWise.Application.DTOs
{
    public record ReservaRequest(int SalaId, int UsuarioId, DateTime DataInicio, DateTime DataFim);
}
