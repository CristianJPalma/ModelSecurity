using Business;
using Business.Implements;
using Data.Interfaces;
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
    private readonly IUserData _userData;

    public MenuController(IUserData userData)
        {
            _userData = userData;
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