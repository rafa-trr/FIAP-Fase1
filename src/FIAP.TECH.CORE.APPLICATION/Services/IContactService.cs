using FIAP.TECH.CORE.APPLICATION.DTO;

namespace FIAP.TECH.CORE.APPLICATION.Services;

public interface IContactService
{
    Task CreateAsync(ContactDto contactDTO);
    Task<IEnumerable<ContactDto>> GetAll();
    Task<IEnumerable<ContactDetailsDto>> GetByDdd(string ddd);
}
