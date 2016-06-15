using MVC_ERP.Controllers.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace MVC_ERP.Controllers.System
{
    public class ProjectController : BaseController 
    {

        public ActionResult Index(int? Id, int? CompanyListID)
        {

            PrepareUserDat();
            if (!CheckUserPermission(PermissionType.Open)) return Json("", JsonRequestBehavior.AllowGet);



            if (Id != null)
            {

                MainTbl = db.Stock_ProductGroups.Find(Id);
            }



            ModelState.Clear();

            if (ModelState.IsValid)
            {
                return View(MainTbl);
            }

            return View(MainTbl);




        }
    }
}