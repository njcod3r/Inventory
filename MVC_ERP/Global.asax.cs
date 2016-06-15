using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using MVC_ERP.Models;
using System.Web.Http;
namespace MVC_ERP
{
    public class MvcApplication : System.Web.HttpApplication
    {
   
        protected void Application_Start()
        {

         

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
          
           //To initialize Menues for first use
           Database.SetInitializer(new MenueContextSeedInitializer());
      
       
        }

        protected void Session_Start(Object sender, EventArgs e)
        {

            //These sesion resposible for add new value when i'm in combo box without close form
            HttpContext.Current.Session.Add("IsReturnBackToDetailPageActive", "");
            HttpContext.Current.Session.Add("MainIdReturnBackToDetailPage", "");
            HttpContext.Current.Session.Add("MainNameReturnBackToDetailPage", "");
        }

    }
}
