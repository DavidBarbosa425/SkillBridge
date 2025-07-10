using Application.DTOs;
using Application.Interfaces;
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
            var userRepositoryMock = new Mock<IUserRepository>();
            var applicationMapperMock = new Mock<IApplicationMapper>();
            var validatorServiceMock = new Mock<IValidatorService>();
            var emailServiceMock = new Mock<IEmailService>();
            var emailConfirmationServiceMock = new Mock<IEmailConfirmationService>();

            userRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(Result<User>.Ok(new User() { Name = "Test"}));

            applicationMapperMock
                .Setup(x => x.User.ToUserDto(It.IsAny<User>()))
                .Returns(new UserDto() { Name = "Test"});

            applicationMapperMock
                .Setup(x => x.User.ToUser(It.IsAny<RegisterUserDto>()))
                .Returns(new User() { Name = "Test" });

            emailConfirmationServiceMock
                .Setup(x => x.GenerateEmailConfirmation(It.IsAny<UserDto>()))
                .ReturnsAsync(Result<SendEmail>.Ok(new SendEmail() { Email = "teste@teste.com" }));

            var authService = new AuthService(
                userRepositoryMock.Object,
                applicationMapperMock.Object,
                validatorServiceMock.Object,
                emailServiceMock.Object,
                emailConfirmationServiceMock.Object
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
            Assert.True(result.Success);
            Assert.Equal("Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para sua caixa de entrada.", result.Message);
        }
        
    }
}
