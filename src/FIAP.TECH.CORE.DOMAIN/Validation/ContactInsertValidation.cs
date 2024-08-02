using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FluentValidation;

namespace FIAP.TECH.CORE.DOMAIN.Validation;

public class ContactInsertValidation : AbstractValidator<Contact>
{
    public ContactInsertValidation(IRegionRepository _regionRepository)
    {
        #region Atributos
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("O campo {PropertyName} precisa ser fornecido.");

        RuleFor(c => c.Email)
            .EmailAddress()
            .NotEmpty()
            .WithMessage("O campo {PropertyName} deve ser preenchido corretamente.");

        RuleFor(c => c.PhoneNumber)
            .NotEmpty()
            .NotNull().WithMessage("O campo {PropertyName} é obrigatório.")
            .Length(8, 9)
            .WithMessage("O campo {PropertyName} precisa ser fornecido entre 8 e 9 dígitos.");

        RuleFor(x => x.DDD).MustAsync( async (DDD, CancellationToken) =>
        {
            return await _regionRepository.Exists(x => x.DDD == DDD);
        }).WithMessage("DDD inválido.");
        #endregion
    }
}