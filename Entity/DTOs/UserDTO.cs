﻿using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    class UserDTO
    {
        public int Id { get; set; }
        public PersonDTO PersonId { get; set; }
        public string UserName { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }

        public UserDTO(int id, PersonDTO personId, string userName, string code, bool active)
        {
            Id = id;
            PersonId = personId;
            UserName = userName;
            Code = code;
            Active = active;
        }
    }
}
