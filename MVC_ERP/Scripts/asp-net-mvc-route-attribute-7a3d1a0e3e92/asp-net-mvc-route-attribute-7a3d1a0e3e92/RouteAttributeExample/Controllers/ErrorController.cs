using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflow.Helpers;

namespace RouteAttributeExample.Controllers
{
    public class ErrorController : Controller
    {
        [Route("404")]
        public ActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return new ContentResult { Content = "Not Found" };
        }
    }
}
