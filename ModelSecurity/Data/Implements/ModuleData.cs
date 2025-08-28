using Data.Interfaces;
using Entity.Context;
using Entity.Model;

namespace Data.Implements
{ 
    public class ModuleData : DataBase<Module>, IModuleData
    {
        public ModuleData(IApplicationDbContext context) : base(context)
        {
        }
    }
}