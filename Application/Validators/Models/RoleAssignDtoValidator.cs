using Application.DTOs;
using FluentValidation;

namespace Application.Validators.Models
{
    public class RoleAssignDtoValidator : AbstractValidator<RoleAssignDto>
    {
        public RoleAssignDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.");
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role é obrigatório.");
        }
    }
}
