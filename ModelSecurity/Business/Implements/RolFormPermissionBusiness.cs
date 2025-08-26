using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{ 
    public class RolFormPermissionBusiness : BaseBusiness<RolFormPermission, RolFormPermissionDto>, IRolFormPermissionBusiness
    {
        protected readonly IRolFormPermissionData _rolFormPermissionData;
        public RolFormPermissionBusiness(IRolFormPermissionData rolFormPermissionData, IMapper mapper, ILogger<RolFormPermissionBusiness> logger)
            : base(rolFormPermissionData, mapper, logger)
        {
            _rolFormPermissionData = rolFormPermissionData;
        }
    }
}