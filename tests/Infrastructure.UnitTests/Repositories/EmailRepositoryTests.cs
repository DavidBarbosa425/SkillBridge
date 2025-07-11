using Domain.Entities;
using Infrastructure.Identity.Models;
using Infrastructure.Interfaces.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

namespace Infrastructure.UnitTests.Repositories
{
    public class EmailRepositoryTests
    {
        public static Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mock = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);
            return mock;
        }

        [Fact]
        public async Task GenerateEmailConfirmationTokenAsync_ShouldReturnToken_WhenIsCreated()
        {
            // Arrange
            var email = "test@test.com";
            var userManagerMock = GetMockUserManager();
            var ApplicationDbContextMock = new Mock<IApplicationDbContext>();

            userManagerMock
                .Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(new ApplicationUser { Email = email, UserName = "testuser" });

            userManagerMock
                .Setup(um => um.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync("token");

            var emailRepository = new EmailRepository(
                ApplicationDbContextMock.Object,
                userManagerMock.Object
            );

            // Act
            var result = await emailRepository.GenerateEmailConfirmationTokenAsync(email);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("token", result.Data);

        }

        [Fact]
        public async Task GenerateEmailConfirmationTokenAsync_ShouldReturnError_WhenTokenIsNotCreated()
        {
            // Arrange
            var email = "test@test.com";
            var userManagerMock = GetMockUserManager();
            var ApplicationDbContextMock = new Mock<IApplicationDbContext>();

            userManagerMock
                .Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(new ApplicationUser { Email = email, UserName = "testuser" });

            userManagerMock
                .Setup(um => um.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync("");

            var emailRepository = new EmailRepository(
                ApplicationDbContextMock.Object,
                userManagerMock.Object
            );

            // Act
            var result = await emailRepository.GenerateEmailConfirmationTokenAsync(email);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Falha ao gerar token de confirmação de e-mail.", result.Message);

        }

        [Fact]
        public async Task SaveTokenEmailConfirmationAsync_ShouldReturnSuccess_WhenEmailConfirmationIsSaved()
        {
            // Arrange
            var email = "test@test.com";
            var token = "token";
            var userManagerMock = GetMockUserManager();
            var ApplicationDbContextMock = new Mock<IApplicationDbContext>();

            userManagerMock
                .Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(new ApplicationUser { Email = email, UserName = "testuser", Id = "user-id" });

            ApplicationDbContextMock
                .Setup(c => c.EmailConfirmationTokens.Add(It.IsAny<EmailConfirmationToken>()))
                .Verifiable();

            ApplicationDbContextMock
                .Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var emailRepository = new EmailRepository(
                ApplicationDbContextMock.Object,
                userManagerMock.Object
                );

            // Act
            var result = await emailRepository.SaveTokenEmailConfirmationAsync(email, token);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Operação realizada com sucesso", result.Message);

        }

        [Fact]
        public async Task SaveTokenEmailConfirmationAsync_ShouldReturnError_WhenUserIsNotFound()
        {
            // Arrange
            var email = "test@test.com";
            var token = "token";
            var userManagerMock = GetMockUserManager();
            var ApplicationDbContextMock = new Mock<IApplicationDbContext>();

            userManagerMock
                .Setup(um => um.FindByEmailAsync(email));

            var emailRepository = new EmailRepository(
                ApplicationDbContextMock.Object,
                userManagerMock.Object
                );

            // Act
            var result = await emailRepository.SaveTokenEmailConfirmationAsync(email, token);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Erro ao buscar usuário para salvar token de confirmação", result.Message);

        }

        [Fact]
        public async Task SaveTokenEmailConfirmationAsync_ShouldReturnError_WhenTokenEmailConfirmationIsNotSaved()
        {
            // Arrange
            var email = "test@test.com";
            var token = "token";
            var userManagerMock = GetMockUserManager();
            var ApplicationDbContextMock = new Mock<IApplicationDbContext>();

            userManagerMock
                .Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(new ApplicationUser { Email = email, UserName = "testuser", Id = "user-id" });

            ApplicationDbContextMock
                .Setup(c => c.EmailConfirmationTokens.Add(It.IsAny<EmailConfirmationToken>()))
                .Verifiable();

            ApplicationDbContextMock
                .Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            var emailRepository = new EmailRepository(
                ApplicationDbContextMock.Object,
                userManagerMock.Object
                );

            // Act
            var result = await emailRepository.SaveTokenEmailConfirmationAsync(email, token);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Erro ao salvar token de confirmação de e-mail.", result.Message);

        }

    }
}
