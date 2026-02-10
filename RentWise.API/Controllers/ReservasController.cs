using Microsoft.AspNetCore.Mvc;
using RentWise.Core.Entidades;
using RentWise.Core.Interfaces;

namespace RentWise.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly ISalaRepository _salaRepository;

        public ReservasController(IReservaRepository reservaRepository, ISalaRepository salaRepository)
        {
            _reservaRepository = reservaRepository;
            _salaRepository = salaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPorPeriodo(DateTime inicio, DateTime fim)
        {
            var reservas = await _reservaRepository.ObterPorPeriodoAsync(inicio, fim);
            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            var reserva = await _reservaRepository.ObterPorIdAsync(id);
            if (reserva == null) return NotFound();
            return Ok(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Reserva reserva)
        {
            if (reserva.Inicio >= reserva.Fim)
            {
                return BadRequest("A data de início deve ser anterior à data de fim.");
            }

            var sala = await _salaRepository.ObterPorIdAsync(reserva.SalaId);
            if (sala == null)
            {
                return NotFound("A sala informada não existe.");
            }

            var conflito = await _reservaRepository.ExisteConflitoAsync(reserva.SalaId, reserva.Inicio, reserva.Fim);
            if (conflito)
            {
                return BadRequest("Este horário já está ocupado para esta sala.");
            }

            reserva.Confirmar(sala.PrecoPorHora);

            await _reservaRepository.AdicionarAsync(reserva);

            return CreatedAtAction(nameof(GetPorId), new { id = reserva.Id }, reserva);
        }

        [HttpPatch("{id}/cancelar")]
        public async Task<IActionResult> Cancelar(int id)
        {
            var reserva = await _reservaRepository.ObterPorIdAsync(id);

            if (reserva == null)
            {
                return NotFound("Reserva não encontrada.");
            }

            try
            {
                reserva.CancelarReserva();

                await _reservaRepository.AtualizarAsync(reserva);

                return NoContent(); // Sucesso
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("disponibilidade/{salaId}")]
        public async Task<IActionResult> VerificarDisponibilidade(int salaId, DateTime inicio, DateTime fim)
        {
            var ocupado = await _reservaRepository.ExisteConflitoAsync(salaId, inicio, fim);
            return Ok(new { disponivel = !ocupado });
        }

    }
}