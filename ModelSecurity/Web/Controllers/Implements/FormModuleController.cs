using Business.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implements
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormModuleController : BaseController<FormModule, FormModuleDto>, IFormModuleController
    {
        public FormModuleController(IBaseBusiness<FormModule, FormModuleDto> business, ILogger<FormModuleController> logger)
            : base(business, logger)
        {
        }
    }

}
