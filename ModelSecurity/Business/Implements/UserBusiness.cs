using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{
    public class UserBusiness : BaseBusiness<User, UserDto>, IUserBusiness
    {
        protected readonly IUserData _userData;
        public UserBusiness(IUserData userData, IMapper mapper, ILogger<UserBusiness> logger)
            : base(userData, mapper, logger)
        {
            _userData = userData;
        }
        
        public List<MenuDto> GetUserMenu(int userId)
        {
            var menu = _userData.GetMenuByUserId(userId);

            return menu;
        }
    }

}