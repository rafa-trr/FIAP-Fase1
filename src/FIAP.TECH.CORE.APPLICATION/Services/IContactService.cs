using FIAP.TECH.CORE.APPLICATION.DTO;

namespace FIAP.TECH.CORE.APPLICATION.Services
{
    public interface IContactService
    {
        Task CreateAsync(ContactDTO contactDTO);
    }
}
