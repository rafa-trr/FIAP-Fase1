using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.CORE.DOMAIN.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace FIAP.TECH.CORE.APPLICATION.Services.Contacts;

public class ContactService : IContactService
{
    private readonly IMapper _mapper;
    private readonly IRegionRepository _regionRepository;
    private readonly IContactRepository _contactRepository;

    public ContactService(IMapper mapper,
            IRegionRepository regionRepository,
            IContactRepository contactRepository)
    {
        _mapper = mapper;
        _regionRepository = regionRepository;
        _contactRepository = contactRepository;
    }

    public async Task Create(ContactInsertDto contactInsertDTO)
    {
        Contact contact = _mapper.Map<Contact>(contactInsertDTO);

        //Valida se os dados estao corretos
        var resultValidation = new ContactInsertValidation(_regionRepository);
        ValidationResult results = await resultValidation.ValidateAsync(contact);

        if (results.Errors.Any())
            throw new ValidationException(results.Errors);

        var region = await _regionRepository.Search(x => x.DDD == contact.DDD);
        contact.RegionId = region!.Id;

        await _contactRepository.Create(contact);
    }

    public async Task Delete(int id)
    {
        var contact = await _contactRepository.GetById(id);

        if (contact is null)
            throw new InvalidOperationException("Contato com o ID informado não existe.");

        await _contactRepository.Delete(contact);
    }

    public async Task<IEnumerable<ContactDto>> GetAll()
    {
        return _mapper.Map<IEnumerable<ContactDto>>(await _contactRepository.GetAll());
    }

    public async Task<ContactDto> GetById(int id)
    {
        return _mapper.Map<ContactDto>(await _contactRepository.GetById(id));
    }

    public async Task<IEnumerable<ContactDetailsDto>> GetByDdd(string ddd)
    {
        return _mapper.Map<IEnumerable<ContactDetailsDto>>(await _contactRepository.GetByDdd(ddd));
    }
}
