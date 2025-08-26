using Business;
using Business.Implements;
using Data.Implements;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Exceptions;

namespace Web.Controllers.Implements
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
public class MenuController : ControllerBase
{
    private readonly UserBusiness _userBusiness;
    private readonly UserData _userData;

    public MenuController(UserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

    [HttpGet("user/{id}")]
    public IActionResult GetMenu(int id)
    {
        try
        {
            var menu = _userData.GetMenuByUserId(id);
            return Ok(menu);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}



}