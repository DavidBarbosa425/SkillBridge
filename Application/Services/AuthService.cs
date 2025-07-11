﻿using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationMapper _mapper;
        private readonly IValidatorService _validatorService;
        private readonly IEmailService _emailService;
        private readonly IEmailConfirmationService _emailConfirmationService;

        public AuthService(
            IUserRepository userRepository,
            IApplicationMapper mapper,
            IValidatorService validatorService,
            IEmailService emailService,
            IEmailConfirmationService emailConfirmationService
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validatorService = validatorService;
            _emailService = emailService;
            _emailConfirmationService = emailConfirmationService;
        }

        public async Task<Result> RegisterUserAsync(RegisterUserDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var user = _mapper.User.ToUser(dto);

            var createdUser = await _userRepository.AddAsync(user);

            if (!createdUser.Success)
                return Result.Failure("Erro ao criar usuário.");

            var userDto = _mapper.User.ToUserDto(user);

            var sendEmail = await _emailConfirmationService.GenerateEmailConfirmation(userDto);

            if (!sendEmail.Success)
                return Result.Failure("Erro ao gerar e enviar e-mail de confirmação.");

            await _emailService.SendEmailAsync(sendEmail.Data!);

            return Result.Ok("Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para sua caixa de entrada.");

        }

        public async Task LoginAsync(LoginDto dto)
        {
            await _validatorService.ValidateAsync(dto);
        }
    }
}
