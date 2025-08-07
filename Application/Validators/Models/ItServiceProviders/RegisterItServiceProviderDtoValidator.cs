using Application.DTOs;
using FluentValidation;

namespace Application.Validators.Models.ItServiceProviders
{
    public class RegisterItServiceProviderDtoValidator : AbstractValidator<RegisterItServiceProviderDto>
    {
        public RegisterItServiceProviderDtoValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Autenticação do usuario é obrigatória.");
            RuleFor(x => x.JobTitle).NotEmpty().WithMessage("Especialização do usuário é obrigatório");
        }
    }
}
