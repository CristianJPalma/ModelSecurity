using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{ 
    public class PermissionBusiness : BaseBusiness<Permission, PermissionDto>, IPermissionBusiness
    {
        protected readonly IPermissionData _permissionData;
        public PermissionBusiness(IPermissionData permissionData, IMapper mapper, ILogger<PermissionBusiness> logger)
            : base(permissionData, mapper, logger)
        {
            _permissionData = permissionData;
        }
    }
}