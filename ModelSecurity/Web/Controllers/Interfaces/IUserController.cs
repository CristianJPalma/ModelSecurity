using Entity.DTOs;
using Entity.Model;

namespace Web.Controllers.Interfaces
{
    public interface IUserController : IBaseController<User, UserDto>
    {
    }
}