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
using MVC_ERP.Controllers.General;



namespace MVC_ERP.Controllers
{
    public class CustomersController : BaseController
    {
        
        private MSDBContext db = new MSDBContext();
        int UserId,MemberShipId;
        object  Deleted = false;

         [Authorize]
        public ActionResult Index(int? Id, string submitButton, Sys_Partners MainTbl, int? CompanyList)
        {

           Session["UserId"]  = "1";
           Session["MemberShipId"] = "1";


           MemberShipId = Convert.ToInt32(Session["MemberShipId"]);
           UserId = Convert.ToInt32(Session["UserId"]);
           int? CC;
           
           switch(submitButton) {

            case "Save":
              
               //New or edit according Id pass if 0 new else edit
               if( ModelState.IsValid)
               {
                   if (Insert(MainTbl)) return RedirectToAction("Index", new { Id = MainTbl.Id });
               }
               //Model not valid
               return View(MainTbl);

            case "Discard":
               //Cancel return to Index view
                   RouteData.Values.Remove("Id");
                   CC = Convert.ToInt32( TempData["SelectedID"].ToString());
                   Id = db.Sys_Partners.Where(t => t.MemberShipId == MemberShipId && t.CompanyId == CC && t.PartnerType==1 && t.Deleted != true).Max(c => (int?)c.Id);
                   MainTbl = db.Sys_Partners.Find(Id);
                   return RedirectToAction("Index", new { Id = Id });

            case "Delete":
             
               //Delete return to previous
                   if (MainTbl.Id == null || MainTbl.Id == 0)
               {
                   //Message no data to delete
                   TempData["MessageType"] = "info";
                   TempData["ReturnMessage"] = "No data to delete";
                   return View(MainTbl);
               }
               if (ModelState.IsValid)
               {
                 RouteData.Values.Remove("Id");
                 if (Delete(MainTbl))
                 {
                     CC = Convert.ToInt32(TempData["SelectedID"].ToString());
                     Id = db.Sys_Partners.Where(t => t.MemberShipId == MemberShipId && t.CompanyId == CC && t.PartnerType == 1 && t.Deleted != true).Max(c => (int?)c.Id);
                     MainTbl = db.Sys_Partners.Find(Id);
                     return RedirectToAction("Index", new { Id = Id });
                 }
               }
               //Model not valid
               return View(MainTbl);

            case "Reuse":

               if (MainTbl.Id == null || MainTbl.Id == 0)
               {
                   //Message no data to delete
                   TempData["MessageType"] = "info";
                   TempData["ReturnMessage"] = "No data to reuse";
                   return View(MainTbl);
               }
               //New or edit according Id pass if 0 new else edit
               if (ModelState.IsValid)
               {
                   RouteData.Values.Remove("Id");
                   if (Reuse(MainTbl))
                   {
                       CC = Convert.ToInt32(TempData["SelectedID"].ToString());
                       Id = db.Sys_Partners.Where(t => t.MemberShipId == MemberShipId && t.CompanyId == CC && t.PartnerType == 1  && t.Deleted != true).Max(c => (int?)c.Id);
                       MainTbl = db.Sys_Partners.Find(Id);
                       return RedirectToAction("Index", new { Id = Id });
                   }

               }
               //Model not valid
               return View(MainTbl);

            case "Post":
               if (MainTbl.Id == null || MainTbl.Id == 0)
               {
                   //Message no data to delete
                   TempData["MessageType"] = "info";
                   TempData["ReturnMessage"] = "No data to post";
                   return View(MainTbl);
               }
               //Delete return to previous
               if (ModelState.IsValid)
               {
                   RouteData.Values.Remove("Id");
                   if (Post(MainTbl)) return RedirectToAction("Index", new { Id = MainTbl.Id });
               }
               //Model not valid
               return View(MainTbl);

 
            default:

               //Read function
             
            
               if (Id == null && TempData["SelectedID"] ==null)
               {
                   //Insialize form load user company and first record associated to this companies to prevent load page twice
                   CC = db.Companies.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true).Min(c => (int?)c.Id);
                   Id = db.Sys_Partners.Where(t => t.MemberShipId == MemberShipId && t.CompanyId == CC && t.PartnerType == 1 && t.Deleted != true).Max(c => (int?)c.Id);
                   if (Id!= null)
                   {
                   MainTbl = db.Sys_Partners.Find(Id);
                   }
                   
               }
               else if (Id == null && TempData["SelectedID"]!=null)
               {
                   // Change to company that may be have no data
                  CC = Convert.ToInt32(TempData["SelectedID"].ToString());
                  Id = db.Sys_Partners.Where(t => t.MemberShipId == MemberShipId && t.CompanyId == CC && t.PartnerType == 1  && t.Deleted != true).Max(c => (int?)c.Id);
                  if (Id==null)
                  { MainTbl.CompanyId =CC??0; }
                  else
                  { MainTbl = db.Sys_Partners.Find(Id); }
                                    
               }
               else
               {
                   MainTbl = db.Sys_Partners.Find(Id);
               }
             
               ModelState.Clear();

               //MainTbl.RegisterDates = new UserControl_GHDate();

               //MainTbl.RegisterDates.GregiDate = new DateTime() ;
               //MainTbl.RegisterDates.HijriDate = new DateTime();

               //MainTbl.DealDates = new UserControl_GHDate();

               if (ModelState.IsValid)
               {
                 
               return View(MainTbl);
            
               }

               return View(MainTbl);
              }



       }

        // Insert Function
        protected Boolean Insert(Sys_Partners MainTbl) 
            {


                if (MainTbl.Id == 0)
                {

                    //Insert New
                  
                 if (MainTbl.Code == null)
                 {
                     //Generate auto code
                     //string Max = db.MainTbl.Where(t => t.MemberShipId == MemberShipId && t.Deleted != true).Max(c => c.Code);
                     int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Sys_Partners  WHERE (isnumeric(Code) = 1) and CompanyId=(" + MainTbl.CompanyId + ") and PartnerType=1 and   (MemberShipId = " + Session["MemberShipId"] + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault(); 
                     MainTbl.Code = Max.ToString();         
                   }
                    MainTbl.Id = (db.Sys_Partners.Max(c => (int?)c.Id) ?? 0) + 1;
                    MainTbl.MemberShipId =Convert.ToInt32(Session["MemberShipId"]);
                    MainTbl.PartnerType =  1;
                    MainTbl.Create_Uid = Convert.ToInt32(Session["UserId"]);
                    MainTbl.Create_Date = DateTime.Now;
                   MainTbl.RegisterDates = new UserControl_GHDate_Null();
                   MainTbl.DealDates = new UserControl_GHDate_Null();
                    db.Sys_Partners.Add(MainTbl);

                    db.SaveChanges();

                    TempData["MessageType"] = "success";
                    TempData["ReturnMessage"] ="Save successful"; //TempData use when render view if not render you can use viewbag
                    return true;

                }

                else
                    //Update
                {

                    if (MainTbl.Post == true)
                    {
                        //Message no data to delete
                        TempData["MessageType"] = "danger";
                        TempData["ReturnMessage"] = "This movment was posting you can't modify it";
                        return true;
                    }


                    db.Entry(MainTbl).State = EntityState.Modified;

                    MainTbl.Write_Uid = Convert.ToInt32(Session["UserId"]);
                    MainTbl.Write_Date = DateTime.Now;

                    MainTbl.RegisterDates = new UserControl_GHDate_Null();
                    MainTbl.DealDates = new UserControl_GHDate_Null();

                    db.SaveChanges();
                    TempData["MessageType"] = "success";
                    TempData["ReturnMessage"] = "Modified successful"; 
                    return true;
                }

        }

        // Reuse Function
        protected Boolean Reuse(Sys_Partners MainTbl)
        {
            MemberShipId = Convert.ToInt32(Session["MemberShipId"]);

            if (MainTbl.Id == null || MainTbl.Id == 0)
            {
                //Message no data to delete
                TempData["MessageType"] = "info";
                TempData["ReturnMessage"] = "No data to reuse";
                return false;
            }
          
                //Insert New
             
                 //Generate auto code
                MainTbl.Id = (db.Sys_Partners.Max(c => (int?)c.Id) ?? 0) + 1;
                int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Sys_Partners  WHERE (isnumeric(Code) = 1) and CompanyId=(" + MainTbl.CompanyId + ") and PartnerType=1 and (MemberShipId = " + Session["MemberShipId"] + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault(); 
                
                 MainTbl.Code = Max.ToString();

                 MainTbl.Create_Uid = Convert.ToInt32(Session["UserId"]);
                 MainTbl.Create_Date = DateTime.Now;
                 MainTbl.Write_Uid = null;
                 MainTbl.Write_Date = null;
                 MainTbl.Post = null;
                 MainTbl.Post_Uid = null;
                 MainTbl.Post_Date = null;

               MainTbl.MemberShipId = MemberShipId;

               db.Sys_Partners.Add(MainTbl);

               db.SaveChanges();

               return true;

        }

        protected Boolean Delete(Sys_Partners MainTbl)
        {
            if (MainTbl.Id == null || MainTbl.Id == 0)
            {
                //Message no data to delete
                TempData["MessageType"] = "info";
                TempData["ReturnMessage"] = "No data to delete";
                return false;
            }

            if (MainTbl.Post == true)
            {
                //Message no data to delete
                TempData["MessageType"] = "danger";
                TempData["ReturnMessage"] = "This movment was posting you can't delete it";
                return false;
            }

            db.Entry(MainTbl).State = EntityState.Modified;

    
            MainTbl.Deleted = true;
            MainTbl.Delete_Uid = Convert.ToInt32(Session["UserId"]);
            MainTbl.Delete_Date = DateTime.Now;

            db.SaveChanges();
            TempData["MessageType"] = "success";
            TempData["ReturnMessage"] = "Delete successful";


            return true;
          
        }

        protected Boolean Post(Sys_Partners MainTbl)
       {
           if (MainTbl.Id == null || MainTbl.Id == 0)
           {
               //Message no data to post
               TempData["MessageType"] = "info";
               TempData["ReturnMessage"] = "No data to post";
               return false;
           }

           if (MainTbl.Post==true )
           {
               //Message  data  post
               TempData["MessageType"] = "info";
               TempData["ReturnMessage"] = "this movement already posting";
               return false;
           }
         
           db.Entry(MainTbl).State = EntityState.Modified;


           MainTbl.Post = true;
           MainTbl.Post_Uid = Convert.ToInt32(Session["UserId"]);
           MainTbl.Post_Date = DateTime.Now;

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

         [Authorize]
        public ActionResult Search([DataSourceRequest]DataSourceRequest request, string TxtSearchVal, int? CompanySearchId)
        {
            TempData["SelectedID"] = CompanySearchId;
            MemberShipId = Convert.ToInt32(Session["MemberShipId"]);
           if (TxtSearchVal !=null)
           {
               var m = db.Sys_Partners.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanySearchId && b.PartnerType == 1 && b.Deleted != true && (b.Code.Contains(TxtSearchVal) || b.NameE.Contains(TxtSearchVal) || b.NameA.Contains(TxtSearchVal) || b.Email.Contains(TxtSearchVal))).Take(20);
             IQueryable<Sys_Partners> mm = m;
             DataSourceResult result = mm.ToDataSourceResult(request);
             return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
           }
           else
           {
               var m = db.Sys_Partners.OrderByDescending(b => b.Id).Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanySearchId && b.PartnerType == 1 && b.Deleted != true).Take(20);
               IQueryable<Sys_Partners> mm = m;
               DataSourceResult result = mm.ToDataSourceResult(request);
               return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);

           }
          
            //}
        }


        public ActionResult CompanyChanged(int? CompanyList)
        {
            MemberShipId = Convert.ToInt32(Session["MemberShipId"]);
            Sys_Partners tbl = new Sys_Partners();
            int? Id = db.Sys_Partners.Where(t => t.MemberShipId == MemberShipId && t.CompanyId == CompanyList && t.PartnerType == 1 && t.Deleted != true).Max(c => (int?)c.Id);

            //return RedirectToAction("index", new {Id = Id });
            TempData["SelectedID"] = CompanyList;
            return Json(new {Id=Id, CompanyList= CompanyList});//Pass 2 value


        }

        
    }
}
