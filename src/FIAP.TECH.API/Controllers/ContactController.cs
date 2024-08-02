using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.APPLICATION.Services.Contacts;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TECH.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _contactService.GetAll());
    }

    [HttpGet("get-by-region/{ddd}")]
    public async Task<IActionResult> GetByDdd([FromRoute] string ddd)
    {
        return Ok(await _contactService.GetByDdd(ddd));
    }

    [HttpPost]
    public async Task<IActionResult> Post(ContactInsertDto contactDTO)
    {
        try
        {
            await _contactService.Create(contactDTO);
            return Ok(new {message = "Contato criado com sucesso."});
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { errors = vex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _contactService.Delete(id);
            return Ok(new { message = "Contato deletado com sucesso." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
