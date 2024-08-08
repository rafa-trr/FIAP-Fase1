using FIAP.TECH.CORE.APPLICATION.DTO;

namespace FIAP.TECH.CORE.APPLICATION.Services.Contacts;

public interface IContactService
{
    Task Create(ContactDto contactDTO);
    Task Update(int id, ContactDto contactDTO);
    Task Delete(int id);
    Task<IEnumerable<ContactDto>> GetAll();
    Task<ContactDto> GetById(int id);
    Task<IEnumerable<ContactDetailsDto>> GetByDdd(string ddd);
}
