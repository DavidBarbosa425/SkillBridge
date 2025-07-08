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
                .ReturnsAsync(Result<string>.Ok("df666392-5154-4ee4-bd1e-c16d07ab4fb4"));

            var user = new User
            {
                Name = "Teste",
                Email = "teste@teste.com",
                Password = "123456"
            };

            applicationMapperMock
                .Setup(x => x.User.ToUser(It.IsAny<RegisterUserDto>()))
                .Returns(user);

            var sendEmail = new SendEmail
            {
                Name = "Teste",
                Email = "teste@teste.com",
                Subject = "Confirmação de E-mail",
                Body = @$"
                            <p>Olá Teste,</p>
                            <p>Clique no botão abaixo para confirmar seu e-mail:</p>
                            <p>
                                <a 
                                    style='
                                        padding: 10px 20px; 
                                        background-color: #4CAF50; 
                                        color: white; 
                                        text-decoration: none;
                                    ' 
                                    href='https://localhost:44319/api/auth/confirmationUserEmail?Guid=da64780e-54f1-414a-946a-098b3abbe450'>
                                    Confirmar E-mail
                                </a>
                            </p>"


            };

            emailConfirmationServiceMock
                .Setup(x => x.GenerateEmailConfirmation(It.IsAny<User>()))
                .ReturnsAsync(sendEmail);


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
