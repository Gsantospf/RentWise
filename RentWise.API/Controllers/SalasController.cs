using Microsoft.AspNetCore.Mvc;
using RentWise.Core.Entidades;
using RentWise.Core.Interfaces;

namespace RentWise.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalasController : ControllerBase
    {
        private readonly ISalaRepository _repository;

        // O .NET injeta o repositório aqui automaticamente graças ao AddScoped que fizemos!
        public SalasController(ISalaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var salas = await _repository.ObterTodasAsync();
            return Ok(salas);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Sala sala)
        {
            if (sala == null) return BadRequest();

            await _repository.AdicionarAsync(sala);
            return CreatedAtAction(nameof(Get), new { id = sala.Id }, sala);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            var sala = await _repository.ObterPorIdAsync(id);

            if (sala == null)
            {
                return NotFound("Sala não encontrada.");
            }

            return Ok(sala);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Sala salaInput)
        {
            // 1. Verifica se o ID da URL bate com o do objeto (segurança básica)
            if (id != salaInput.Id)
            {
                return BadRequest("O ID da URL é diferente do ID do corpo da requisição.");
            }

            // 2. Verifica se a sala existe no banco antes de tentar atualizar
            var salaExistente = await _repository.ObterPorIdAsync(id);
            if (salaExistente == null)
            {
                return NotFound("Sala não encontrada para atualização.");
            }
            salaExistente.Atualizar(salaInput.Nome, salaInput.Descricao, salaInput.PrecoPorHora);

            _repository.Atualizar(salaExistente);

            return NoContent(); // 204 = Deu certo, mas não tenho nada para te devolver
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sala = await _repository.ObterPorIdAsync(id);

            if (sala == null)
            {
                return NotFound();
            }

            _repository.Remover(sala);

            return NoContent();
        }
    }
}