using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Security;
using LabProject.Models.Auth;
using LabProject.DAL;
using Newtonsoft.Json;

namespace LabProject.DAL.Security
{
    public static class AuthenticationHelper
    {
        public static string GetAuthTicketAsString(User user, bool isPersistent)
        {
            if (user == null) throw new ArgumentNullException("user");


            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();

            serializeModel.UserId = user.Id;
            serializeModel.Email = user.Email;
            serializeModel.Role = user.Role.Name;

            string userData = JsonConvert.SerializeObject(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1,
                        user.Email,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(15),
                        isPersistent,
                        userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);

            return encTicket;
        }
    }
}