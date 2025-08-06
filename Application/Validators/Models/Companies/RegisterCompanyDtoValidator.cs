using Application.DTOs;
using FluentValidation;

namespace Application.Validators.Models.Companies
{
    public class RegisterCompanyDtoValidator : AbstractValidator<RegisterCompanyDto>
    {
        RegisterCompanyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome da empresa é obrigatório.")
                .MaximumLength(100)
                .WithMessage("O nome da empresa não pode exceder 100 caracteres.");
            RuleFor(x => x.CNPJ)
                .NotEmpty()
                .WithMessage("CNPJ é obrigatório.");
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("Identificação do usuário é obrigatório.");
        }
    }
}
