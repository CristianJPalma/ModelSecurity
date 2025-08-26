using Business.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implements
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User, UserDto>, IUserController
    {
        public UserController(IBaseBusiness<User, UserDto> business, ILogger<UserController> logger)
            : base(business, logger)
        {
        }
    }
}
