using Business.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implements
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolUserController : BaseController<RolUser, RolUserDto>, IRolUserController
    {
        public RolUserController(IBaseBusiness<RolUser, RolUserDto> business, ILogger<RolUserController> logger)
            : base(business, logger)
        {
        }
    }
}
