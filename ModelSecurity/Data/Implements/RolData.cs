using Data.Interfaces;
using Entity.Context;
using Entity.Model;

namespace Data.Implements
{ 
    public class RolData : DataBase<Rol>, IRolData
    {
        public RolData(IApplicationDbContext context) : base(context)
        {
        }
    }
}