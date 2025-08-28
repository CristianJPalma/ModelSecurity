

using Data.Interfaces;
using Entity.Context;
using Entity.Model;

namespace Data.Implements
{ 
    public class FormModuleData : DataBase<FormModule>, IFormModuleData
    {
        public FormModuleData(IApplicationDbContext context) : base(context)
        {
        }
    }
}