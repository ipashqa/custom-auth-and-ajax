﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabProject.Models.Auth
{
    public class CustomPrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}