using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Mappers;
using Application.Services.Auth;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task RegisterUserAsync_ShouldReturnError_WhenUserIsNotCreated()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var applicationMapperMock = new Mock<IApplicationMapper>();
            var validatorServiceMock = new Mock<IValidatorService>();
            var emailServiceMock = new Mock<IEmailService>();

            userRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(Result<string>.Ok("Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para sua caixa de entrada."));


            var authService = new AuthService(
                userRepositoryMock.Object,
                applicationMapperMock.Object,
                validatorServiceMock.Object,
                emailServiceMock.Object
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
            Assert.Equal("Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para sua caixa de entrada.", result.Message);
        }
    }
}
