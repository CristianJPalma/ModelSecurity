using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers.Interfaces
{
    public interface IBaseController<T, D>
        where T : BaseModel where D : BaseDto
    {
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Create(D dto);
        Task<IActionResult> Update(D dto);
        Task<IActionResult> Delete(int id);
    }
}