using Domain.Entities;
using FluentValidation;

namespace Application.Validators.Models
{
    public class UserRegisterValidator : AbstractValidator<UserRegistered>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("o Nome é obrigatório.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.");
            RuleFor(x => x.IdentityUserId)
                .NotEmpty().WithMessage("O identityUserId do usuário é obrigatório.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("O identityUserId do usuário deve ser um GUID válido.");

        }
    }
}
