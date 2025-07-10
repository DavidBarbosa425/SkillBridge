using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Factories;
using Application.Services.Emails;
using Domain.Common;
using Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Services.Emails
{
    public class EmailConfirmationServiceTests
    {
        [Fact]
        public async Task GenerateEmailConfirmation_ShouldReturnSendEmail_WhenSuccessful()
        {
            // Arrange
            var emailRepositoryMock = new Mock<IEmailRepository>();
            var urlServiceMock = new Mock<IUrlService>();
            var emailTemplateFactoryMock = new Mock<IEmailTemplateFactory>();
            var validatorServiceMock = new Mock<IValidatorService>();

            emailRepositoryMock
                .Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(Result<string>.Ok("test-token"));

            emailRepositoryMock
                .Setup(x => x.SaveTokenEmailConfirmationAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(Result.Ok());

            emailRepositoryMock
                .Setup(x => x.GetEmailConfirmationTokenGuidAsync(It.IsAny<string>()))
                .ReturnsAsync(Result<Guid>.Ok(Guid.NewGuid()));

            urlServiceMock
                .Setup(x => x.GenerateApiUrl(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string?>>()))
                .Returns("https://localhost:44319/api/auth/confirmationUserEmail?Guid=test-guid");

            emailTemplateFactoryMock
                .Setup(x => x.GenerateConfirmationEmailHtml(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("<p>Confirm your email</p>");

            var service = new EmailConfirmationService(
                emailRepositoryMock.Object,
                urlServiceMock.Object,
                emailTemplateFactoryMock.Object,
                validatorServiceMock.Object
                );

            var userDto = new UserDto
            {
                Name = "Test User",
                Email = "teste@teste.com.br"
            };

            // Act
            var result = await service.GenerateEmailConfirmation(userDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test User", result.Data!.Name);

        }
    }
}
