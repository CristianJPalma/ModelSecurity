using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{ 
    public class RolBusiness : BaseBusiness<Rol, RolDto>, IRolBusiness
    {
        protected readonly IRolData _rolData;
        public RolBusiness(IRolData rolData, IMapper mapper, ILogger<RolBusiness> logger)
            : base(rolData, mapper, logger)
        {
            _rolData = rolData;
        }
    }
}