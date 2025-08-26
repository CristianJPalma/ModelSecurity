using Data.Interfaces;
using Entity.Context;
using Entity.Model;

namespace Data.Implements
{ 
    public class RolFormPermissionData : DataBase<RolFormPermission>, IRolFormPermissionData
    {
        public RolFormPermissionData(ApplicationDbContext context) : base(context)
        {
        }
    }
}