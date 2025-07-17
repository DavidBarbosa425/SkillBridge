using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Emails;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserEmailConfirmationService _userEmailConfirmationService;

        public UsersController(
            IUserService userService,
            IUserEmailConfirmationService userEmailConfirmationService)
        {
            _userService = userService;
            _userEmailConfirmationService = userEmailConfirmationService;
        }

    }
}
