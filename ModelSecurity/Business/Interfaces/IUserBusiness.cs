using Entity.DTOs;
using Entity.Model;

namespace Business.Interfaces
{
    public interface IUserBusiness : IBaseBusiness<User, UserDto>
    {
        List<MenuDto> GetUserMenu(int userId);
    }
}