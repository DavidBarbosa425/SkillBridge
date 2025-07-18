using Application.DTOs;
using FluentValidation;

namespace Application.Validators.Models
{
    public class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
    {
        public ConfirmEmailDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty).WithMessage("ID do usuário é obrigatório");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token de verificação é obrigatório");
        }
    }
}
