using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class ModuleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public ModuleDTO(int id, string name, bool active)
        {
            Id = id;
            Name = name;
            Active = active;
        }
    }
}
