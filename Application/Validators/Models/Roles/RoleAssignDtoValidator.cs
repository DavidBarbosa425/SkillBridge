using Application.DTOs;
using FluentValidation;

namespace Application.Validators.Models.Roles
{
    public class RoleAssignDtoValidator : AbstractValidator<RoleAssignDto>
    {
        public RoleAssignDtoValidator()
        {
            RuleFor(x => x.IdentityUserId)
                .NotEmpty().WithMessage("Identificação do usuário é obrigatório.");
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role é obrigatório.");
        }
    }
}
