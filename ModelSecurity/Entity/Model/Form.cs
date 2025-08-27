using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Form : BaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? DeleteAt { get; set; }

        public ICollection<FormModule> FormModules { get; set; }
        public ICollection<RolFormPermission> RolFormPermissions { get; set; }
    }
}
