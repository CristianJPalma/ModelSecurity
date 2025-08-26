using Entity.DTOs;
using Entity.Model;

namespace Data.Interfaces
{
    public interface IUserData : IBaseData<User>
    {
        Task<User?> GetUserWithPersonByEmailAsync(string email);
        List<MenuDto> GetMenuByUserId(int userId);
    }
}