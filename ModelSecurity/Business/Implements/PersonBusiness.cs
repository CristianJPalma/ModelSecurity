using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{ 
    public class PersonBusiness : BaseBusiness<Person, PersonDto>, IPersonBusiness
    {
        protected readonly IPersonData _personData;
        public PersonBusiness(IPersonData personData, IMapper mapper, ILogger<PersonBusiness> logger)
            : base(personData, mapper, logger)
        {
            _personData = personData;
        }
    }
}