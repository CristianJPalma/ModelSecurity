using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{
    public class FormBusiness : BaseBusiness<Form, FormDto>, IFormBusiness
    {
        protected readonly IFormData _formData;
        public FormBusiness(IFormData formData, IMapper mapper, ILogger<FormBusiness> logger)
            : base(formData, mapper, logger)
        {
            _formData = formData;
        }
    }

}