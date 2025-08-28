using Data.Interfaces;
using Entity.Context;
using Entity.Model;

namespace Data.Implements
{ 
    public class RolUserData : DataBase<RolUser>, IRolUserData
    {
        public RolUserData(IApplicationDbContext context) : base(context)
        {
        }
    }
}