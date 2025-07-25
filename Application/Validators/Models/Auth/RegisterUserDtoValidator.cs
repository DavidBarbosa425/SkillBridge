﻿using Application.DTOs;
using Domain.Constants;
using FluentValidation;

namespace Application.Validators.Models.Auth
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Um tipo de usuário é obrigatória.")
                .NotEqual(Roles.Admin).WithMessage("Não é possível adicionar esse tipo de acesso");
        }
    }
}
