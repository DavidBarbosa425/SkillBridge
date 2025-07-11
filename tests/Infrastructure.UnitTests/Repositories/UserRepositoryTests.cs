using Domain.Entities;
using Infrastructure.Identity.Models;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Infrastructure.UnitTests.Repositories
{
    public class UserRepositoryTests
    {
        private UserManager<ApplicationUser> GetMockUserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null
            );

            return userManager.Object;
        }

        [Fact]
        public async Task AddAsync_ShouldReturnUser_WhenUserIsCreated()
        {
            // Arrange
            var userManagerMock = GetMockUserManager();
            var infrastructureMapperMock = new Mock<IInfrastructureMapper>();

            infrastructureMapperMock
                .Setup(m => m.User.ToApplicationUser(It.IsAny<User>()))
                .Returns(new ApplicationUser { UserName = "testuser", Email = "" });

            Mock.Get(userManagerMock)
              .Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            infrastructureMapperMock
                .Setup(m => m.User.ToUser(It.IsAny<ApplicationUser>()))
                .Returns(new User { Name = "testuser", Email = "" });

            var userRepository = new UserRepository(
                userManagerMock,
                infrastructureMapperMock.Object
                );

            var user = new User()
            {
                Name = "testuser",
                Email = ""
            };

            // Act

            var result = await userRepository.AddAsync(user);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("testuser", result.Data.Name);

        }
    }
}
