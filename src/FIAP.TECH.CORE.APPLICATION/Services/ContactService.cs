using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.CORE.DOMAIN.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace FIAP.TECH.CORE.APPLICATION.Services
{
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
        public async Task CreateAsync(ContactDTO contactDTO)
        {
            Contact contact = _mapper.Map<Contact>(contactDTO);

            //Valida se os dados estao corretos  
            // Define the cancellation token.
            var resulValidation = new ContactValidation(_regionRepository);
            ValidationResult results = await resulValidation.ValidateAsync(contact);

            if (results.Errors.Any())
                throw new ValidationException(results.Errors);

            Region region = await _regionRepository.Search(x => x.DDD == contact.DDD);
            contact.RegionId = region.Id;

            await _contactRepository.Create(contact);

        }
    }
}
