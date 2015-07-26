using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using LabProject.DAL.Security;
using LabProject.Models.Auth;
using Newtonsoft.Json;
using System.Web.Security;

namespace LabProject.Modules
{
    public class CustomAuthenticateModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PostAuthenticateRequest += PostAuthenticateRequest_Handler;
        }

        public void Dispose()
        {
        }


        private void PostAuthenticateRequest_Handler(object sender, EventArgs e)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                newUser.UserId = serializeModel.UserId;
                newUser.Email = serializeModel.Email;
                newUser.Role = serializeModel.Role;

                HttpContext.Current.User = newUser;
            }
        }
    }
}