using Data.Interfaces;
using Entity.Context;
using Entity.Model;

namespace Data.Implements
{ 
    public class RolUserData : DataBase<RolUser>, IRolUserData
    {
        public RolUserData(ApplicationDbContext context) : base(context)
        {
        }
    }
}