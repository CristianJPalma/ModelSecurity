using Business.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implements
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseController<Person, PersonDto>, IPersonController
    {
        public PersonController(IBaseBusiness<Person, PersonDto> business, ILogger<PersonController> logger)
            : base(business, logger)
        {
        }
    }
}
