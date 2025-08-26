using Data.Interfaces;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Implements
{
    public class UserData : DataBase<User>, IUserData
    {
        public UserData(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<User?> GetUserWithPersonByEmailAsync(string email)
        {
            return await _context.User
                .Include(u => u.Person)
                .FirstOrDefaultAsync(u => u.Person.Email == email);
        }
         public List<MenuDto> GetMenuByUserId(int userId)
        {
            var query = from ru in _context.RolUser
                        join r in _context.Rol on ru.RolId equals r.Id
                        join rfp in _context.RolFormPermission on r.Id equals rfp.RolId
                        join f in _context.Form on rfp.FormId equals f.Id
                        join p in _context.Permission on rfp.PermissionId equals p.Id
                        join fm in _context.FormModule on f.Id equals fm.FormId
                        join m in _context.Module on fm.ModuleId equals m.Id
                        where ru.UserId == userId
                        select new MenuDto
                        {
                            Modulo = m.Name,
                            Formulario = f.Name,
                            Permiso = p.Name,
                            FormCode = f.Code
                        };

            return query.Distinct().ToList();
        }

    }
}