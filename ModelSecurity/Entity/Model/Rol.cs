﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Rol
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description{ get; set; }
        public bool Active { get;set; }
        public DateTime CreateAt { get;set; }
        public DateTime? DeleteAt { get; set; }


    }
}
