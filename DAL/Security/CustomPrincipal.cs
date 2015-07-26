using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Principal;

namespace LabProject.DAL.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (this.Role == role)
                return true;
            else
                return false;
        }

        public CustomPrincipal(string Email)
        {
            this.Identity = new GenericIdentity(Email);
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}