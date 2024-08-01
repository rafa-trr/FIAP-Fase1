using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.APPLICATION.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TECH.API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContactDTO contactDTO)
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
    }
}
