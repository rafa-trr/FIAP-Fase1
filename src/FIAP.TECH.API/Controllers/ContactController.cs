using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.APPLICATION.Services;
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

    [HttpPost]
    public async Task<IActionResult> Post(ContactDto contactDTO)
    {
        try
        {
            await _contactService.CreateAsync(contactDTO);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _contactService.GetAll());
    }

    [HttpGet("{ddd}")]
    public async Task<IActionResult> GetByDdd([FromRoute] string ddd)
    {
        return Ok(await _contactService.GetByDdd(ddd));
    }
}
