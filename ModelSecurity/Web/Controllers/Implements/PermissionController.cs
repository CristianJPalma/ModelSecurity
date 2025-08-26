using Business.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implements
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseController<Permission, PermissionDto>, IPermissionController
    {
        public PermissionController(IBaseBusiness<Permission, PermissionDto> business, ILogger<PermissionController> logger)
            : base(business, logger)
        {
        }
    }
}
