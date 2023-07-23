using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebsiteCosmetic.Common
{
    public class HasCredentialAttribute:AuthorizeAttribute
    {
        public string RoleID { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (UserLogin)HttpContext.Current.Session[CommonConstants.USER_SESSION];
            if(session == null)
            {
                return false;
            }    
            List<string> privilegeLevels = this.GetCredentialByLoggedInUser(session.UserName);
            if(privilegeLevels.Contains(this.RoleID))
            {
                return true;
            }
            else
            {
                return false;
            } 
        }
        private List<string> GetCredentialByLoggedInUser(string userName)
        {
            var credentials = (List<string>)HttpContext.Current.Session[CommonConstants.SESSION_CREDENTIALS];
            return credentials;
        }
    }
}