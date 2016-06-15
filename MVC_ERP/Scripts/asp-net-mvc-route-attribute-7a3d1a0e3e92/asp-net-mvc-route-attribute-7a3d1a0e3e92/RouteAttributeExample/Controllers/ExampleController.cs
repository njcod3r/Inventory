using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflow.Helpers;

namespace RouteAttributeExample.Controllers
{
    public class ExampleController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("user/{id:INT}")]
        public ActionResult UserById(int id)
        {
            return new ContentResult { Content = "user #" + id };
        }

        [Route("{magicGuid:GUID}")]
        public ActionResult Magic(Guid magicGuid)
        {
            return new ContentResult { Content = "guid of magic: " + magicGuid };
        }

        // Demonstrating a limitiation in our parsing logic, you can't nest a {n}
        [Route(@"phone-number/{number:(\d\d\d\-\d\d\d\-\d\d\d\d)}/{forName?}")]
        public ActionResult PhoneNumber(string number, string forName)
        {
            return new ContentResult { Content = "got a number: " + number + (forName.HasValue() ? "\r\n and a name: " + forName : "") };
        }
    }
}
