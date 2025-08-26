using Business.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implements
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : BaseController<Module, ModuleDto>, IModuleController
    {
        public ModuleController(IBaseBusiness<Module, ModuleDto> business, ILogger<ModuleController> logger)
            : base(business, logger)
        {
        }
    }
}
