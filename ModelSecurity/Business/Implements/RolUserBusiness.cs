using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{ 
    public class RolUserBusiness : BaseBusiness<RolUser, RolUserDto>, IRolUserBusiness
    {
        protected readonly IRolUserData _rolUserData;
        public RolUserBusiness(IRolUserData rolUserData, IMapper mapper, ILogger<RolUserBusiness> logger)
            : base(rolUserData, mapper, logger)
        {
            _rolUserData = rolUserData;
        }
    }
}