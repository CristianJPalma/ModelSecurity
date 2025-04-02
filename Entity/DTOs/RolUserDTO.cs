﻿using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class RolUserDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; } = new UserDto();
        public int RolId { get; set; }
        public RolDto RolDto { get; set; } = new RolDto();

    }
}
