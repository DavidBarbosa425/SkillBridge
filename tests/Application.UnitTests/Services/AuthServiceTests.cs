using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Factories;
using Application.Interfaces.Mappers;
using Application.Services;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task RegisterUserAsync_ShouldReturnSuccess_WhenUserIsCreated()
        {
            // Arrange
            var applicationMapperMock = new Mock<IApplicationMapper>();
            var validatorServiceMock = new Mock<IValidatorService>();
            var emailServiceMock = new Mock<IEmailService>();
            var identityUserServiceMock = new Mock<IIdentityUserService>();
            var urlServiceMock = new Mock<IUrlService>();
            var emailTemplateFactory = new Mock<IAccountEmailTemplateFactory>();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = "Test@test.com"
            };

            applicationMapperMock
                .Setup(x => x.User.ToUser(It.IsAny<RegisterUserDto>()))
                .Returns(user);

            identityUserServiceMock
                .Setup(x => x.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(Result<User>.Ok(user));

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            applicationMapperMock
                .Setup(x => x.User.ToUserDto(It.IsAny<User>()))
                .Returns(userDto);

            var authService = new AuthService(
                applicationMapperMock.Object,
                validatorServiceMock.Object,
                emailServiceMock.Object,
                identityUserServiceMock.Object,
                urlServiceMock.Object,
                emailTemplateFactory.Object
            );

            var sendEmail = new SendEmail()
            {
                Name = user.Name,
                Email = user.Email,
                Subject = "Confirmação de E-mail",
                Body = "Por favor, confirme seu e-mail."
            };

            var dto = new RegisterUserDto
            {
                Name = "Teste",
                Email = "teste@teste.com",
                Password = "123456"
            };

            // Act
            var result = await authService.RegisterUserAsync(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para sua caixa de entrada.", result.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnError_WhenUserIsNotCreated()
        {
            // Arrange
            var applicationMapperMock = new Mock<IApplicationMapper>();
            var validatorServiceMock = new Mock<IValidatorService>();
            var emailServiceMock = new Mock<IEmailService>();
            var identityUserServiceMock = new Mock<IIdentityUserService>();
            var urlServiceMock = new Mock<IUrlService>();
            var emailTemplateFactory = new Mock<IAccountEmailTemplateFactory>();


            applicationMapperMock
                .Setup(x => x.User.ToUser(It.IsAny<RegisterUserDto>()))
                .Returns(new User() { Name = "Test" });

            var authService = new AuthService(
                applicationMapperMock.Object,
                validatorServiceMock.Object,
                emailServiceMock.Object,
                identityUserServiceMock.Object,
                urlServiceMock.Object,
                emailTemplateFactory.Object
            );

            var dto = new RegisterUserDto
            {
                Name = "Teste",
                Email = "teste@teste.com",
                Password = "123456"
            };

            // Act
            var result = await authService.RegisterUserAsync(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Erro ao criar usuário.", result.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnError_WhenEmailConfirmationIsNotSend()
        {
            // Arrange
            var applicationMapperMock = new Mock<IApplicationMapper>();
            var validatorServiceMock = new Mock<IValidatorService>();
            var emailServiceMock = new Mock<IEmailService>();
            var identityUserServiceMock = new Mock<IIdentityUserService>();
            var urlServiceMock = new Mock<IUrlService>();
            var emailTemplateFactory = new Mock<IAccountEmailTemplateFactory>();

            applicationMapperMock
                .Setup(x => x.User.ToUserDto(It.IsAny<User>()))
                .Returns(new UserDto() { Name = "Test" });

            applicationMapperMock
                .Setup(x => x.User.ToUser(It.IsAny<RegisterUserDto>()))
                .Returns(new User() { Name = "Test" });

            var authService = new AuthService(
                applicationMapperMock.Object,
                validatorServiceMock.Object,
                emailServiceMock.Object,
                identityUserServiceMock.Object,
                urlServiceMock.Object,
                emailTemplateFactory.Object
            );

            var dto = new RegisterUserDto
            {
                Name = "Teste",
                Email = "teste@teste.com",
                Password = "123456"
            };

            // Act
            var result = await authService.RegisterUserAsync(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Erro ao gerar e enviar e-mail de confirmação.", result.Message);
        }

    }

}
