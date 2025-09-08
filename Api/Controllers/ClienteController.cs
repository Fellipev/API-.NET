using Api.Dtos;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("cliente")]
public class ClientesController(IClienteService service) : ControllerBase
{
    // /cliente/listar?nome=&email=&cpf=
    [HttpGet("listar")]
    public async Task<IActionResult> Listar([FromQuery] string? nome, [FromQuery] string? email, [FromQuery] string? cpf)
    {
        var (items, total) = await service.SearchAsync(nome, email, cpf);
        return Ok(new { total, items });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id) => (await service.GetAsync(id)) is { } dto ? Ok(dto) : NotFound();

    [HttpPost("criar")]
    public async Task<IActionResult> Criar([FromBody] CreateClienteDto input)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var dto = await service.CreateAsync(input);
        return CreatedAtAction(nameof(ObterPorId), new { id = dto.Id }, dto);
    }

    [HttpPut("atualizar/{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] UpdateClienteDto input)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        return await service.UpdateAsync(id, input) ? NoContent() : NotFound();
    }

    [HttpDelete("remover/{id:int}")]
    public async Task<IActionResult> Remover(int id) => await service.DeleteAsync(id) ? NoContent() : NotFound();

    // vínculos Contato
    [HttpPut("{id:int}/vincular-contato/{contatoId:int}")]
    public async Task<IActionResult> VincularContato(int id, int contatoId)
        => await service.LinkContatoAsync(id, contatoId) ? NoContent() : NotFound();

    [HttpDelete("{id:int}/desvincular-contato")]
    public async Task<IActionResult> DesvincularContato(int id)
        => await service.UnlinkContatoAsync(id) ? NoContent() : NotFound();
}
