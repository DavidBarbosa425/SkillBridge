using Application.DTOs;
using FluentValidation;

namespace Application.Validators.Models.Auth
{
    public class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
    {
        public ConfirmEmailDtoValidator()
        {
            RuleFor(x => x.IdentityId)
                .NotEmpty().WithMessage("ID do usuário é obrigatório");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token de verificação é obrigatório");
        }
    }
}
