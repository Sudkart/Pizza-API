﻿using System;
using System.Collections.Generic;
using System.Text;

namespace STM.Core.EntityLayer
{
    public class User
    {
     
        public Int32? UserId { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }

        public String Email { get; set; }

        public DateTime LastLoginDate { get; set; }

        public int RoleId { get; set; }

        public int Active { get; set; }

        public DateTime CreatedDate { get; set; }

        public String CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public String UpdatedBy { get; set; }

        public String Salt { get; set; }

        public string NewPassword { get; set; }
    }
}
