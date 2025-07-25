//using Application.DTOs;
//using Application.Interfaces;
//using Application.Interfaces.Emails;
//using Application.Interfaces.Factories;
//using Application.Interfaces.Mappers;
//using Application.Services;
//using Domain.Common;
//using Domain.Entities;
//using Domain.Interfaces;
//using Moq;

//namespace Application.UnitTests.Services
//{
//    public class AuthServiceTests
//    {
//        [Fact]
//        public async Task RegisterAsync_ShouldReturnSuccess_WhenUserIsCreated()
//        {
//            // Arrange
//            var dto = new RegisterUserDto { Email = "teste@email.com", Name = "Teste" };
//            var user = new User {Name = dto.Name, Email = dto.Email };
//            var userDto = new UserDto {Name = dto.Name, Email = dto.Email };

//            var applicationMapperMock = new Mock<IApplicationMapper>();
//            var validatorServiceMock = new Mock<IValidatorService>();
//            var emailAccountServiceMock = new Mock<IEmailAccountService>();
//            var identityUserServiceMock = new Mock<IIdentityUserService>();

//            // mocks
//            applicationMapperMock.Setup(x => x.User.ToUser(dto)).Returns(user);

//            identityUserServiceMock.Setup(x => x.AddAsync(user))
//                .ReturnsAsync(Result<User>.Ok(user));

//            applicationMapperMock.Setup(x => x.User.ToUserDto(user)).Returns(userDto);

//            emailAccountServiceMock.Setup(x => x.SendConfirmationEmailAsync(userDto))
//                .ReturnsAsync(Result.Ok("Email enviado"));

//            var service = new AuthService(
//                applicationMapperMock.Object,
//                validatorServiceMock.Object,
//                emailAccountServiceMock.Object,
//                identityUserServiceMock.Object
//            );

//            // Act
//            var result = await service.RegisterAsync(dto);

//            // Assert
//            Assert.NotNull(result);
//            Assert.True(result.Success);
//            Assert.Equal($"Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para {dto.Email}.", result.Message);

//        }

//        [Fact]
//        public async Task RegisterAsync_ShouldReturnError_WhenUserIsNotCreated()
//        {
//            // Arrange
//            var dto = new RegisterUserDto { Email = "teste@email.com", Name = "Teste" };
//            var user = new User { Name = dto.Name, Email = dto.Email };
//            var userDto = new UserDto { Name = dto.Name, Email = dto.Email };

//            var applicationMapperMock = new Mock<IApplicationMapper>();
//            var validatorServiceMock = new Mock<IValidatorService>();
//            var emailAccountServiceMock = new Mock<IEmailAccountService>();
//            var identityUserServiceMock = new Mock<IIdentityUserService>();

//            // mocks
//            applicationMapperMock.Setup(x => x.User.ToUser(dto)).Returns(user);

//            identityUserServiceMock.Setup(x => x.AddAsync(user))
//                .ReturnsAsync(Result<User>.Failure("Erro ao criar usuário"));

//            var service = new AuthService(
//                applicationMapperMock.Object,
//                validatorServiceMock.Object,
//                emailAccountServiceMock.Object,
//                identityUserServiceMock.Object
//            );

//            // Act
//            var result = await service.RegisterAsync(dto);

//            // Assert
//            Assert.NotNull(result);
//            Assert.False(result.Success);
//            Assert.Equal("Erro ao criar usuário", result.Message);
//        }

//        [Fact]
//        public async Task RegisterUserAsync_ShouldReturnError_WhenEmailConfirmationIsNotSend()
//        {
//            // Arrange
//            var dto = new RegisterUserDto { Email = "teste@email.com", Name = "Teste" };
//            var user = new User { Name = dto.Name, Email = dto.Email };
//            var userDto = new UserDto { Name = dto.Name, Email = dto.Email };

//            var applicationMapperMock = new Mock<IApplicationMapper>();
//            var validatorServiceMock = new Mock<IValidatorService>();
//            var emailAccountServiceMock = new Mock<IEmailAccountService>();
//            var identityUserServiceMock = new Mock<IIdentityUserService>();

//            // mocks
//            applicationMapperMock.Setup(x => x.User.ToUser(dto)).Returns(user);

//            identityUserServiceMock.Setup(x => x.AddAsync(user))
//                .ReturnsAsync(Result<User>.Ok(user));

//            applicationMapperMock.Setup(x => x.User.ToUserDto(user)).Returns(userDto);

//            emailAccountServiceMock.Setup(x => x.SendConfirmationEmailAsync(userDto))
//                .ReturnsAsync(Result.Failure("Erro ao enviar email de confirmação"));

//            var service = new AuthService(
//                applicationMapperMock.Object,
//                validatorServiceMock.Object,
//                emailAccountServiceMock.Object,
//                identityUserServiceMock.Object
//            );

//            // Act
//            var result = await service.RegisterAsync(dto);

//            // Assert
//            Assert.NotNull(result);
//            Assert.False(result.Success);
//            Assert.Equal("Erro ao enviar email de confirmação", result.Message);
//        }

//    }

//}
