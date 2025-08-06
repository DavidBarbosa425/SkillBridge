using Application.DTOs;
using FluentValidation;

namespace Application.Validators.Models
{
    public class RoleAssignDtoValidator : AbstractValidator<RoleAssignDto>
    {
        public RoleAssignDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O e-mail é obrigatório.");
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role é obrigatório.");
        }
    }
}
