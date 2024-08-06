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
            .Matches("^(?:((?:9\\d|[2-9])\\d{3})\\-?(\\d{4}))$")
            .WithMessage("O campo {PropertyName} precisa ser valido.");

        RuleFor(c => c.DDD)
            .NotEmpty()
            .NotNull().WithMessage("O campo {PropertyName} é obrigatório.")
            .Matches("^(?:\\(?([1-9][0-9])\\)?\\s?)$")
            .WithMessage("DDD inválido."); ;
        #endregion
    }
}