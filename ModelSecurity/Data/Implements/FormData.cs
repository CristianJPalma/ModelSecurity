using Data.Interfaces;
using Entity.Context;
using Entity.Model;

namespace Data.Implements
{
    public class FormData : DataBase<Form>, IFormData
    {
        public FormData(IApplicationDbContext context) : base(context)
        {
        }
    }

}