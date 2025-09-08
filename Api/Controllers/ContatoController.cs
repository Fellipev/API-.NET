using Api.Dtos;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("contato")]
public class ContatosController(IContatoService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> Get() => Ok(await service.ListAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) => (await service.GetAsync(id)) is { } dto ? Ok(dto) : NotFound();

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContatoDto input)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var dto = await service.CreateAsync(input);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateContatoDto input)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        return await service.UpdateAsync(id, input) ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) => await service.DeleteAsync(id) ? NoContent() : NotFound();

    [HttpPut("{id:int}/vincular-endereco/{enderecoId:int}")]
    public async Task<IActionResult> VincularEndereco(int id, int enderecoId)
        => await service.LinkEnderecoAsync(id, enderecoId) ? NoContent() : NotFound();

    [HttpDelete("{id:int}/desvincular-endereco")]
    public async Task<IActionResult> DesvincularEndereco(int id)
        => await service.UnlinkEnderecoAsync(id) ? NoContent() : NotFound();
}
