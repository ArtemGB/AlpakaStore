﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public UserStatus Status { get; set; }

    }
}
