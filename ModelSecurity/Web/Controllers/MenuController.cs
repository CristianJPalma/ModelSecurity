using Business;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
public class MenuController : ControllerBase
{
    private readonly UserBusiness _userBusiness;

    public MenuController(UserBusiness userBusiness)
    {
        _userBusiness = userBusiness;
    }

    [HttpGet("user/{id}")]
    public IActionResult GetMenu(int id)
    {
        try
        {
            var menu = _userBusiness.GetMenuByUserId(id);
            return Ok(menu);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}



}