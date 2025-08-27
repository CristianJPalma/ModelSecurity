using Entity.DTOs;
namespace Business.Interfaces
{
    public interface ILoginBusiness
    {
        Task<object> LoginAsync(LoginDto loginDto);
    }
}