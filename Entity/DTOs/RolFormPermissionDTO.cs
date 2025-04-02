using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class RolFormPermissionDto
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public RolDto Rol { get; set; } = new RolDto();
        public int PermissionId { get; set; }
        public PermissionDto Permission { get; set; } = new PermissionDto();
        public int FormId { get; set; }
        public FormDto Form { get; set; } = new FormDto();
            
    }
}
