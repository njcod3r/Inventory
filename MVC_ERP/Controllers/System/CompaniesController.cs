using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_ERP.Models;
using MVC_ERP.Controllers;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.Entity.SqlServer;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading;
using MVC_ERP.Controllers.General;
using System.IO;


namespace MVC_ERP.Controllers
{
    public class CompaniesController : BaseController
    {
       
    
      
        [Authorize]

        public ActionResult Index(int? Id, string submitButton, Sys_Companies companies, int? CompanyListID, IEnumerable<HttpPostedFileBase> files)
       {
            //set UserId and Language;
           PrepareUserDat();

           //HttpPostedFileBase filee = Request.Files[0];
           switch(submitButton) {

            case "Save":

               //New or edit according Id pass if 0 new else edit
               if( ModelState.IsValid)
               {
                   if (Insert(companies)) return RedirectToAction("Index", new { Id = companies.Id });
               }
               //Model not valid
               return View(companies);

            case "Discard":

               //Cancel return to Index view
               RouteData.Values.Remove("Id");
               return RedirectToAction("Index");

            case "Delete":

               //Delete return to previous
               if (companies.Id == null || companies.Id == 0)
               {
                   //Message no data to delete
                   TempData["MessageType"] = "info";
                   TempData["ReturnMessage"] = "No data to delete";
                   return View(companies);
               }
               if (ModelState.IsValid)
               {
                 RouteData.Values.Remove("Id");
                 if  (Delete(companies)) return RedirectToAction("Index");
         
               }
               //Model not valid
               return View(companies);

            case "Reuse":
               if (companies.Id == null || companies.Id == 0)
               {
                   //Message no data to delete
                   TempData["MessageType"] = "info";
                   TempData["ReturnMessage"] = "No data to reuse";
                   return View(companies);
               }
               //New or edit according Id pass if 0 new else edit
               if (ModelState.IsValid)
               {
                   RouteData.Values.Remove("Id");
                   if (Reuse(companies)) return RedirectToAction("Index");
               }
               //Model not valid
               return View(companies);

            case "Post":

               if (companies.Id == null || companies.Id == 0)
               {
                   //Message no data to delete
                   TempData["MessageType"] = "info";
                   TempData["ReturnMessage"] = "No data to post";
                   return View(companies);
               }
               //Delete return to previous
               if (ModelState.IsValid)
               {
                   RouteData.Values.Remove("Id");
                   if (Post(companies)) return RedirectToAction("Index", new { Id = companies.Id });
               }
               //Model not valid
               return View(companies);

 
            default:

               //Read function
               if (Id == null)

               {
       
                   Id = db.Companies.Where(t => t.MemberShipId == MemberShipId  && t.Deleted != true).Max(c => (int?)c.Id);
               }

               if (Id != null)
               {
               companies = db.Companies.Find(Id);
               }
             
               ModelState.Clear();

               if (ModelState.IsValid)
               {
               return View(companies);
               }

               return View(companies);
              }



       }

        // Insert Function
        protected Boolean Insert(Sys_Companies companies) 
            {

                PrepareUserDat();

            
             
             


                if (companies.Id == 0)
                {

                    //Insert New
                  
                 if (companies.Code == null)
                 {
                     //Generate auto code


                     int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Sys_Companies  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault(); 
                     companies.Code = Max.ToString();         
                   }

                    companies.Id = (db.Companies.Max(c => (int?)c.Id)??0) +1;
                    companies.MemberShipId =MemberShipId;
                    companies.Create_Uid =UserId;
                    companies.Create_Date = DateTime.Now;
             
                    db.Companies.Add(companies);

                    db.SaveChanges();

                    TempData["MessageType"] = "success";
                    TempData["ReturnMessage"] ="Save successful"; //TempData use when render view if not render you can use viewbag
                    return true;

                }

                else
                    //Update
                {

                    if (companies.Post == true)
                    {
                        //Message no data to delete
                        TempData["MessageType"] = "danger";
                        TempData["ReturnMessage"] = "This movment was posting you can't modify it";
                        return true;
                    }


                    db.Entry(companies).State = EntityState.Modified;

                    companies.Write_Uid = UserId;
                    companies.Write_Date = DateTime.Now;

                    db.SaveChanges();
                    TempData["MessageType"] = "success";
                    TempData["ReturnMessage"] = "Modified successful"; 
                    return true;
                }

        }

        // Reuse Function
        protected Boolean Reuse(Sys_Companies companies)
        {
            PrepareUserDat();

            if (companies.Id == null || companies.Id == 0)
            {
                //Message no data to delete
                TempData["MessageType"] = "info";
                TempData["ReturnMessage"] = "No data to reuse";
                return false;
            }
          
                //Insert New
             
                    //Generate auto code

                   companies.Id = (db.Companies.Max(c => (int?)c.Id) ?? 0) + 1;
                   int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Sys_Companies  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId  + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault(); 
                
                 companies.Code = Max.ToString();

                 companies.Create_Uid = UserId;
                 companies.Create_Date = DateTime.Now;
                 companies.Write_Uid = null;
                 companies.Write_Date = null;
                 companies.Post = null;
                 companies.Post_Uid = null;
                 companies.Post_Date = null;



                 companies.MemberShipId = MemberShipId;

                 db.Companies.Add(companies);

                 db.SaveChanges();

               return true;

        }

        protected Boolean Delete(Sys_Companies companies)
        {
            PrepareUserDat();

            if (companies.Id == null || companies.Id == 0)
            {
                //Message no data to delete
                TempData["MessageType"] = "info";
                TempData["ReturnMessage"] = "No data to delete";
                return false;
            }

            if (companies.Post == true)
            {
                //Message no data to delete
                TempData["MessageType"] = "danger";
                TempData["ReturnMessage"] = "This movment was posting you can't delete it";
                return false;
            }

            db.Entry(companies).State = EntityState.Modified;

    
            companies.Deleted = true;
            companies.Delete_Uid = UserId ;
            companies.Delete_Date = DateTime.Now;

            db.SaveChanges();
            TempData["MessageType"] = "success";
            TempData["ReturnMessage"] = "Delete successful";


            return true;
          
        }

        protected Boolean Post(Sys_Companies companies)
       {
           PrepareUserDat();
           if (companies.Id == null || companies.Id == 0)
           {
               //Message no data to post
               TempData["MessageType"] = "info";
               TempData["ReturnMessage"] = "No data to post";
               return false;
           }

           if (companies.Post==true )
           {
               //Message  data  post
               TempData["MessageType"] = "info";
               TempData["ReturnMessage"] = "this movement already posting";
               return false;
           }
         
           db.Entry(companies).State = EntityState.Modified;


           companies.Post = true;
           companies.Post_Uid = UserId ;
           companies.Post_Date = DateTime.Now;

           db.SaveChanges();
           TempData["MessageType"] = "success";
           TempData["ReturnMessage"] = "Posting successful";

           return true;


       }   

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search([DataSourceRequest]DataSourceRequest request, string TxtSearchVal)
        {
            PrepareUserDat();

           if (TxtSearchVal !=null)
           {
               var m = (from Comp in db.Companies.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true && (b.Code.Contains(TxtSearchVal) || b.NameE.Contains(TxtSearchVal) || b.NameA.Contains(TxtSearchVal) || b.RegisterNo.Contains(TxtSearchVal)))
                        select new
                        {
                            Id = Comp.Id,
                            Code = Comp.Code,
                            NameA = Comp.NameA,
                            NameE = Comp.NameE,
                            Notes = Comp.Notes,
                            RegisterNo = Comp.RegisterNo
                        }).Take(20).OrderBy(o=>o.Id).ToList();   
              
               
               //IQueryable<Sys_Companies> mm = m;
               DataSourceResult result = m.ToDataSourceResult(request);
               return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
           }
           else
           {
               var m = (from Comp in db.Companies.OrderByDescending(b => b.Id).Where(b => b.MemberShipId == MemberShipId && b.Deleted != true)
                       select new 
                       {
                           Id=Comp.Id,
                           Code=Comp.Code,
                           NameA = Comp.NameA,
                           NameE = Comp.NameE,
                           Notes=Comp.Notes,
                           RegisterNo = Comp.RegisterNo
                       }).Take(20).OrderBy(o => o.Id).ToList();   
              
               
               
              
               DataSourceResult result = m.ToDataSourceResult(request);
               return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);

           }
          
            //}
        }


        [HttpPost]
        public ActionResult SaveImage(IEnumerable<HttpPostedFileBase> files,int? Id)
        {
            // The Name of the Upload component is "files"
            Sys_Companies MainTbl = db.Companies.Find(Id);
            var ff = Request["Id"];
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    byte[] imageSize = new byte[file.ContentLength];
                    file.InputStream.Read(imageSize, 0, (int)file.ContentLength);

                    MainTbl.Logo = imageSize;


                    db.Entry(MainTbl).State = EntityState.Modified;
                    db.SaveChanges();
                    // The files are not actually saved in this demo
                    // file.SaveAs(physicalPath);
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult RemoveImage(string[] fileNames, int? Id)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        // System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

    }
}
