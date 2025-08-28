using Data.Interfaces;
using Entity.Context;
using Entity.Model;

namespace Data.Implements
{ 
    public class PersonData : DataBase<Person>, IPersonData
    {
        public PersonData(IApplicationDbContext context) : base(context)
        {
        }
    }
}