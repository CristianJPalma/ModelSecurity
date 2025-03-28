using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class FormDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }

        public FormDTO(int id, string name, string code, bool active)
        {
            Id = id;
            Name = name;
            Code = code;
            Active = active;
        }
    }
}
