using Application.DTOs;
using FluentValidation;

namespace Application.Validators.Models.Auth
{
    public class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
    {
        public ConfirmEmailDtoValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("Identificação do usuário é obrigatória");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token de verificação é obrigatório");
        }
    }
}
