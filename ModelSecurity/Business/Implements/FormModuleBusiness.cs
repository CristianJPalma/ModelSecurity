using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{ 
    public class FormModuleBusiness : BaseBusiness<FormModule, FormModuleDto>, IFormModuleBusiness
    {
        protected readonly IFormModuleData _formModuleData;
        public FormModuleBusiness(IFormModuleData formModuleData, IMapper mapper, ILogger<FormModuleBusiness> logger)
            : base(formModuleData, mapper, logger)
        {
            _formModuleData = formModuleData;
        }
    }
}