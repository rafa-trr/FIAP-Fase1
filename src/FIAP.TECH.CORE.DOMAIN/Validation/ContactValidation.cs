using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FluentValidation;

namespace FIAP.TECH.CORE.DOMAIN.Validation
{
    public class ContactValidation : AbstractValidator<Contact>
    {
        public ContactValidation(IRegionRepository _regionRepository)
        {
            #region Atributos
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido.");

            RuleFor(c => c.Email)
                .EmailAddress()
                .NotEmpty()
                .WithMessage("O campo {PropertyName} deve ser preenchido correto.");

            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                .Length(8, 9)
                .WithMessage("O campo {PropertyName} precisa ser fornecido entre 8 e 9.");


            RuleFor(x => x.DDD).Must((DDD) =>
            {
                return _regionRepository.Exists(x => x.DDD == DDD).Result; ;
            }).WithMessage("DDD invalido.");
            #endregion
        }
    }
}