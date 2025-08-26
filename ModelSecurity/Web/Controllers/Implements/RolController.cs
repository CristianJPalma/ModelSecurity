using Business.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implements
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : BaseController<Rol, RolDto>, IRolController
    {
        public RolController(IBaseBusiness<Rol, RolDto> business, ILogger<RolController> logger)
            : base(business, logger)
        {
        }
    }
}
