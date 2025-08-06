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
            RuleFor(x => x.IdentityId)
                .NotEmpty().WithMessage("O id de identificação é obrigatório.");

        }
    }
}
