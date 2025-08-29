using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Models.Auth
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("Identificação do usuário é obrigatório.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token de verificação é obrigatório");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Uma nova senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
        }

    }
}
