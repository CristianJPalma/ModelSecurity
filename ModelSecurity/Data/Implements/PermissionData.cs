using Data.Interfaces;
using Entity.Context;
using Entity.Model;

namespace Data.Implements
{ 
    public class PermissionData : DataBase<Permission>, IPermissionData
    {
        public PermissionData(ApplicationDbContext context) : base(context)
        {
        }
    }
}