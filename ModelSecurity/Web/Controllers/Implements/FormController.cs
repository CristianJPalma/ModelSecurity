using Business.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implements
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : BaseController<Form, FormDto>, IFormController
    {
        public FormController(IBaseBusiness<Form, FormDto> business, ILogger<FormController> logger)
            : base(business, logger)
        {
        }
    }
}
