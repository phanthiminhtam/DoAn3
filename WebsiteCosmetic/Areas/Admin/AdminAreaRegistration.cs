using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteCosmetic.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home2", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}