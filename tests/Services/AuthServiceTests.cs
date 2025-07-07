using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Tests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task RegisterUserAsync_ShouldReturnError_WhenUserIsNotCreated()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var emailServiceMock = new Mock<IEmailService>();
            var emailRepositoryMock = new Mock<IEmailRepository>();
            var urlServiceMock = new Mock<IUrlService>();
            var validatorServiceMock = new Mock<IValidatorService>();

            userRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(Result<string>.Failure("Erro ao criar Usuário, tente novamente mais tarde"));

            var authService = new AuthService(
                userRepositoryMock.Object,
                emailServiceMock.Object,
                emailRepositoryMock.Object,
                urlServiceMock.Object,
                validatorServiceMock.Object
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
            Assert.Equal("Erro ao criar Usuário, tente novamente mais tarde", result.Message);
            Assert.Null(result.Data); 
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
