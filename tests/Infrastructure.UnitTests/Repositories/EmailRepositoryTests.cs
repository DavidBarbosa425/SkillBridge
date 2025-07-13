using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Identity.Models;
using Infrastructure.Mappers;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

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

            // Create an in-memory database context for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            // Create Repository instance
            var emailRepository = new EmailRepository(
                dbContext,
                userManagerMock.Object
            );

            userManagerMock
                .Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(new ApplicationUser { Email = email, UserName = "testuser" });

            userManagerMock
                .Setup(um => um.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync("token");

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
            var infrastructureMapperMock = new Mock<InfrastructureMapper>();

            // Create an in-memory database context for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            // Create Repository instance
            var emailRepository = new EmailRepository(
                dbContext,
                userManagerMock.Object,
                infrastructureMapperMock.Object
            );

            userManagerMock
                .Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(new ApplicationUser { Email = email, UserName = "testuser" });

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

            // Create an in-memory database context for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            // Create Repository instance
            var emailRepository = new EmailRepository(
                dbContext,
                userManagerMock.Object
                );

            userManagerMock
                .Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(new ApplicationUser { Email = email, UserName = "testuser", Id = "user-id" });

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

            // Create an in-memory database context for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            // Create Repository instance
            var emailRepository = new EmailRepository(
                        dbContext,
                        userManagerMock.Object
                        );


            // Act
            var result = await emailRepository.SaveTokenEmailConfirmationAsync(email, token);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Erro ao buscar usuário para salvar token de confirmação", result.Message);

        }


        [Fact]
        public async Task GetEmailConfirmationTokenGuidAsync_ShouldReturnTokenGuid_WhenTokenIsFound()
        {
            // Arrange
            var email = "test@test.com";
            var tokenGuid = Guid.NewGuid();
            var userManagerMock = GetMockUserManager();

            // Create an in-memory database context for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            // Create Repository instance
            var emailRepository = new EmailRepository(
                        dbContext,
                        userManagerMock.Object
                        );


            // Add a token to the in-memory database
            dbContext.EmailConfirmationTokens.Add(new EmailConfirmationToken
                {
                    Id = tokenGuid,
                    Email = "test@test.com",
                    Token = "abc123",
                    UserId = "user-id"
                });

            await dbContext.SaveChangesAsync();
            
            // Act
            var result = await emailRepository.GetEmailConfirmationTokenGuidAsync(email);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(tokenGuid, result.Data);
            Assert.Equal("Operação realizada com sucesso", result.Message);

        }

        [Fact]
        public async Task GetEmailConfirmationTokenGuidAsync_ShouldReturnTokenError_WhenTokenIsNotFound()
        {
            // Arrange
            var email = "test@test.com";
            var tokenGuid = Guid.NewGuid();
            var userManagerMock = GetMockUserManager();

            // Create an in-memory database context for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            // Create Repository instance
            var emailRepository = new EmailRepository(
                        dbContext,
                        userManagerMock.Object
                        );


            // Act
            var result = await emailRepository.GetEmailConfirmationTokenGuidAsync(email);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Token de confirmação de e-mail não encontrado.", result.Message);

        }

        [Fact]
        public async Task GeneratePasswordResetTokenAsync_ShouldReturnToken_WhenTokenIsGeneratad()
        {
            // Arrange
            var email = "test@test.com";
            var userManagerMock = GetMockUserManager();

            // Create an in-memory database context for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            // Create Repository instance
            var emailRepository = new EmailRepository(
                        dbContext,
                        userManagerMock.Object
                        );

            // Mock the UserManager to return a user when FindByEmailAsync is called
            userManagerMock
                .Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(new ApplicationUser { Email = email, UserName = "testuser", Id = "user-id" });

            userManagerMock
                .Setup(um => um.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync("reset-token");


            // Act
            var result = await emailRepository.GeneratePasswordResetTokenAsync(email);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("reset-token", result.Data);
            Assert.Equal("Operação realizada com sucesso", result.Message);

        }

        [Fact]
        public async Task GeneratePasswordResetTokenAsync_ShouldReturnError_WhenUserIsNotFound()
        {
            // Arrange
            var email = "test@test.com";
            var userManagerMock = GetMockUserManager();

            // Create an in-memory database context for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            // Create Repository instance
            var emailRepository = new EmailRepository(
                        dbContext,
                        userManagerMock.Object
                        );


            // Act
            var result = await emailRepository.GeneratePasswordResetTokenAsync(email);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Usuário não encontrado.", result.Message);

        }

        [Fact]
        public async Task GeneratePasswordResetTokenAsync_ShouldReturnError_WhenTokenIsNotGeneratad()
        {
            // Arrange
            var email = "test@test.com";
            var userManagerMock = GetMockUserManager();

            // Create an in-memory database context for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            // Create Repository instance
            var emailRepository = new EmailRepository(
                        dbContext,
                        userManagerMock.Object
                        );

            // Mock the UserManager to return a user when FindByEmailAsync is called
            userManagerMock
                .Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(new ApplicationUser { Email = email, UserName = "testuser", Id = "user-id" });

            // Act
            var result = await emailRepository.GeneratePasswordResetTokenAsync(email);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Falha ao gerar token de redefinição de senha.", result.Message);

        }

    }
}
