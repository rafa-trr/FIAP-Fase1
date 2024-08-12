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

    /// <summary>
    /// Método para listar todos os Contatos cadastrados
    /// </summary>
    /// <returns>Lista de contatos</returns>
    /// <response code="200">Retorna a lista de objetos Contact</response>
    /// <response code="400">A requisição foi mal formada</response>
    /// <response code="401">Usuário não enviou o token de acesso ou o token está expirado</response>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _contactService.GetAll());
    }

    /// <summary>
    /// Método para listar todos os Contatos cadastrados com o DDD informado
    /// </summary>
    /// <param name="ddd">DDD da região em que os contatos estão cadastrados</param>
    /// <returns>Lista de contatos e suas determinadas regiões</returns>
    /// <response code="200">Retorna a lista de objetos Contact com Region</response>
    /// <response code="400">A requisição foi mal formada</response>
    /// <response code="401">Usuário não enviou o token de acesso ou o token está expirado</response>
    [HttpGet("get-by-region/{ddd}")]
    public async Task<IActionResult> GetByDdd([FromRoute] string ddd)
    {
        return Ok(await _contactService.GetByDdd(ddd));
    }

    /// <summary>
    /// Método para incluir um novo Contato
    /// </summary>
    /// <param name="contactDTO">Objeto Contact contendo as informações do contato que deseja cadastrar</param>
    /// <returns>Mensagem de sucesso ou falha ao tentar criar um novo Contato</returns>
    /// <response code="200">Mensagem de contato criado com sucesso</response>
    /// <response code="400">A requisição foi mal formada</response>
    /// <response code="401">Usuário não enviou o token de acesso ou o token está expirado</response>
    /// <remarks>
    /// Exemplo:
    /// {
    /// "name": "Contact test",
    /// "email": "test@testmail.com",
    /// "phoneNumber": "999999999",
    /// "ddd": "11"
    ///}
    /// </remarks>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ContactDto contactDTO)
    {
        try
        {
            await _contactService.Create(contactDTO);
            return Ok(new { message = "Contato criado com sucesso." });
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { errors = vex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    /// <summary>
    /// Método para alterar um Contato existente
    /// </summary>
    /// <param name="id">ID do contato que deseja alterar</param>
    /// <param name="contactDTO">Objeto Contact contendo as informações do contato que deseja alterar</param>
    /// <returns>Mensagem de sucesso ou falha ao tentar alterar um Contato</returns>
    /// <response code="200">Mensagem de contato criado com sucesso</response>
    /// <response code="400">A requisição foi mal formada</response>
    /// <response code="401">Usuário não enviou o token de acesso ou o token está expirado</response>
    /// <remarks>
    /// Exemplo:
    /// {
    /// "name": "Contact test",
    /// "email": "test@testmail.com",
    /// "phoneNumber": "999999999",
    /// "ddd": "11"
    ///}
    /// </remarks>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ContactUpdateDto contactDTO)
    {
        try
        {
            contactDTO.Id = id;
            await _contactService.Update(contactDTO);
            return Ok(new { message = "Contato alterado com sucesso." });
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { errors = vex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    /// <summary>
    /// Método para excluir um Contato cadastrado
    /// </summary>
    /// <param name="id">ID do contato que deseja excluir</param>
    /// <returns>Mensagem de sucesso ou falha ao tentar excluir um Contato</returns>
    /// <response code="200">Mensagem de contato criado com sucesso</response>
    /// <response code="400">A requisição foi mal formada</response>
    /// <response code="401">Usuário não enviou o token de acesso ou o token está expirado</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _contactService.Delete(id);
            return Ok(new { message = "Contato removido com sucesso." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
