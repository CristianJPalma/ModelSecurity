using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{ 
    public class ModuleBusiness : BaseBusiness<Module, ModuleDto>, IModuleBusiness
    {
        protected readonly IModuleData _moduleData;
        public ModuleBusiness(IModuleData moduleData, IMapper mapper, ILogger<ModuleBusiness> logger)
            : base(moduleData, mapper, logger)
        {
            _moduleData = moduleData;
        }
    }
}