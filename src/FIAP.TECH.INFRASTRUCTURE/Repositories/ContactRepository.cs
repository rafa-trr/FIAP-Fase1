using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.INFRASTRUCTURE.Contexts;

namespace FIAP.TECH.INFRASTRUCTURE.Repositories;

public class ContactRepository : Repository<Contact>, IContactRepository
{
    public ContactRepository(AppDbContext appDbContext) : base(appDbContext) { }
}
