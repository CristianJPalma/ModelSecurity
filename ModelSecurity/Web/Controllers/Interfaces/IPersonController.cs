using Entity.DTOs;
using Entity.Model;

namespace Web.Controllers.Interfaces
{
    public interface IPersonController : IBaseController<Person, PersonDto>
    {
    }
}