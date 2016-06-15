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
using System.Web.Script.Serialization;
using System.Xml;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using MVC_ERP.Controllers.General;




namespace MVC_ERP.Controllers
{
    public class Stock_UnitsController : BaseController
    {
        
         [Authorize]
        public ActionResult Index(int? Id, Stock_Units MainTbl,int? CompanyListID)
         {

           PrepareUserDat();
           if (!CheckUserPermission(PermissionType.Open)) return Json("", JsonRequestBehavior.AllowGet);

            

               if (Id != null)

               {

                   MainTbl = db.Stock_Units.Find(Id);
               }



               ModelState.Clear();

               if (ModelState.IsValid)
               {
               return View(MainTbl);
               }

               return View(MainTbl);
             



       }

        // Insert Function
         [Authorize]
         public ActionResult Insert(int Id, int CompanyId, string Code, string NameE, string NameA, int? ParentId, bool HasChildren, string Notes)

        {
            PrepareUserDat();
            if (!CheckUserPermission((Id == 0 ? PermissionType.Insert : PermissionType.Modifay))) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });
          

            //Insert New
            Stock_Units MainTbl = new Stock_Units();
            if (Id == 0)
            {

       

                MainTbl.Id = (db.Stock_Units.Max(c => (int?)c.Id) ?? 0) + 1;
                if (Code == null || Code == "")
                {
                    //Generate auto code
                    int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Stock_Units  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") and  (CompanyId = " + CompanyId + ")  AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();
                    MainTbl.Code = Max.ToString();
                }
                else
                {
                    MainTbl.Code = Code;
                }


                MainTbl.MemberShipId = MemberShipId;
                MainTbl.CompanyId = CompanyId;
                MainTbl.NameA = NameA;
                MainTbl.NameE = NameE;
              
             
                MainTbl.Notes = Notes;


                MainTbl.Create_Uid = UserId;
                MainTbl.Create_Date = DateTime.Now;

                db.Stock_Units.Add(MainTbl);




            }

            else
            //Update
            {

           

                MainTbl = db.Stock_Units.Find(Id);
                if (MainTbl.Post == true)
                {
                    return Json(new { MessageType = "info", ReturnMessage = "This movment was posting you can't modify it" });
                }


                db.Entry(MainTbl).State = EntityState.Modified;

                if (Code == null || Code == "")
                {
                    //Generate auto code
                    int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Stock_Units  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") and  (CompanyId = " + CompanyId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();
                    MainTbl.Code = Max.ToString();
                }
                else
                {
                    MainTbl.Code = Code;
                }

                MainTbl.CompanyId = CompanyId;
                MainTbl.NameA = NameA;
                MainTbl.NameE = NameE;
              
                MainTbl.Notes = Notes;


                MainTbl.Write_Uid = UserId;
                MainTbl.Write_Date = DateTime.Now;



            }



            db.SaveChanges();

             if (Id==0)
             {
                 return Json(new { Id = MainTbl.Id, MessageType = "success", ReturnMessage = GetMessage(MessageTypes.SaveSuccess) });

             }
             else
             {
                 return Json(new { Id = MainTbl.Id, MessageType = "success", ReturnMessage = GetMessage(MessageTypes.ModifaySuccess) });

             }

        }

        enum ReadTypes
        {
        Empty= 0,
        First= 1,
        Next= 2,
        Current= 3,
        Previous= 4,
        Last= 5
        }
         [Authorize]
        public ActionResult Read(int Id,int? CompanyId, int ReadType)
         {
             PrepareUserDat();

             Stock_Units MainTbl = new Stock_Units();

            if (ReadType == Convert.ToInt16(ReadTypes.Empty) )
            {
                //Leave Table empty
            }
            else if (ReadType == Convert.ToInt16(ReadTypes.Current))
            {
                //Read Current

                if (Id==0 || Id==null)
                {
                    MainTbl = new Stock_Units();
                }
                else
                {
                    MainTbl = db.Stock_Units.Find(Id);
                }
            }
            else if (ReadType == Convert.ToInt16(ReadTypes.Next))
            {
                //Read Next
                Id = (db.Stock_Units.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true && c.Id > Id).Min(c => (int?)c.Id) ?? 0);


                if (Id == 0)
                {
                    Id = (db.Stock_Units.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);

                    MainTbl = db.Stock_Units.Find(Id);
                }
                else
                {
                    MainTbl = db.Stock_Units.Find(Id);
                }

            }

            else if (ReadType == Convert.ToInt16(ReadTypes.Previous))
            {
                //Read Previous
                Id = (db.Stock_Units.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true && c.Id < Id).Max(c => (int?)c.Id) ?? 0);
                if (Id == 0)
                {
                    Id = (db.Stock_Units.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
                    MainTbl = db.Stock_Units.Find(Id);
                }
                else
                {
                    MainTbl = db.Stock_Units.Find(Id);
                }

            }
           else if (ReadType == Convert.ToInt16(ReadTypes.Last))
           {
               //Read Last
               Id = (db.Stock_Units.Where(c => c.MemberShipId == MemberShipId && c.CompanyId==CompanyId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);
               if (Id == 0 || Id == null)
               {
                   MainTbl = new Stock_Units();
               }
               else
               {
                   MainTbl = db.Stock_Units.Find(Id);
               }

           }

           else if (ReadType == Convert.ToInt16(ReadTypes.First))
           {
               //Read First
               Id = (db.Stock_Units.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
               if (Id == 0 || Id == null)
               {
                   MainTbl = new Stock_Units();
               }
               else
               {
                   MainTbl = db.Stock_Units.Find(Id);
               }
           }




            if (MainTbl.Id == 0 || MainTbl.Id == null)
            {
                System.Web.HttpContext.Current.Session["MainIdReturnBackToDetailPage"] = 0;
                System.Web.HttpContext.Current.Session["MainNameReturnBackToDetailPage"] = "";
            }
            else
            {
                System.Web.HttpContext.Current.Session["MainIdReturnBackToDetailPage"] = MainTbl.Id;
                System.Web.HttpContext.Current.Session["MainNameReturnBackToDetailPage"] = MainTbl.NameE;
            }

            return Json(new { TBL = MainTbl });
       }
         [Authorize]
 

        // Reuse Function
        [Authorize]
        public ActionResult Reuse(int Id)
        {
            PrepareUserDat();
            if (!CheckUserPermission(PermissionType.Reuse)) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });

            Stock_Units MainTbl = new Stock_Units();
            if (Id == null || Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = "No data to reuse" });
            }


            //Insert New
            List<Stock_Units> listMain = db.Stock_Units.Where(c => c.Id == Id).ToList();

            foreach (var U in listMain)
            {
                MainTbl.Id = (db.Stock_Units.Max(c => (int?)c.Id) ?? 0) + 1;

                int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Stock_Units  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") and  (CompanyId = " + U.CompanyId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();
                MainTbl.Code = Max.ToString();
                MainTbl.MemberShipId = MemberShipId;
                MainTbl.NameA = U.NameA + Max;
                MainTbl.NameE = U.NameE + Max;
                MainTbl.CompanyId = U.CompanyId;
                MainTbl.Notes = U.Notes;


                MainTbl.Create_Uid = UserId;
                MainTbl.Create_Date = DateTime.Now;




            }



            db.Stock_Units.Add(MainTbl);
            db.SaveChanges();

            return Json(new { Id = MainTbl.Id, MessageType = "success", ReturnMessage = GetMessage(MessageTypes.SaveSuccess) });
        }
         [Authorize]
        public ActionResult Delete(int Id)
        {
            PrepareUserDat();
            if (!CheckUserPermission(PermissionType.Delete)) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });

            //prevent delet parent  sub childeren
          

            if (Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = "No data to delete" });
            }

            Stock_Units MainTbl = db.Stock_Units.Find(Id);

          

            if (MainTbl.Post == true)
            {
                //Message for postiong
                return Json(new { MessageType = "info", ReturnMessage = "This movment was posting you can't delete it" });
            }


            db.Entry(MainTbl).State = EntityState.Modified;

    
            MainTbl.Deleted = true;
            MainTbl.Delete_Uid = UserId;
            MainTbl.Delete_Date = DateTime.Now;

            db.SaveChanges();
          

             return Json(new { Id = Id, MessageType = "success", ReturnMessage = "Delete successful" });

          
        }
         [Authorize]
        public ActionResult Post(int Id)
        {
            PrepareUserDat();
            if (Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = "No data to post" });
            }

            Stock_Units MainTbl = db.Stock_Units.Find(Id);
            if (!CheckUserPermission((MainTbl.Post == true ? PermissionType.UnPost : PermissionType.Post))) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });


            if (MainTbl.Post == true)
            {
                //UnPost
                db.Entry(MainTbl).State = EntityState.Modified;


                MainTbl.Post = false;
                MainTbl.Post_Uid = UserId;
                MainTbl.Post_Date = DateTime.Now;

                db.SaveChanges();
                MemberShipId = MemberShipId;


                return Json(new { Id = Id, MessageType = "success", ReturnMessage = "Un Post successful" });

            }

            else
            {
                //Post
                db.Entry(MainTbl).State = EntityState.Modified;


                MainTbl.Post = true;
                MainTbl.Post_Uid = UserId;
                MainTbl.Post_Date = DateTime.Now;

                db.SaveChanges();
                MemberShipId = MemberShipId;


                return Json(new { Id = Id, MessageType = "success", ReturnMessage = "Post successful" });

            }


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
        public ActionResult Search([DataSourceRequest]DataSourceRequest request, string TxtSearchVal, int CompanyId)
        {
            PrepareUserDat();
           if (TxtSearchVal !=null)
           {
               var m = db.Stock_Units.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true && (b.Code.Contains(TxtSearchVal) || b.NameE.Contains(TxtSearchVal) || b.NameA.Contains(TxtSearchVal) || b.Notes.Contains(TxtSearchVal))).Take(20);
               IQueryable<Stock_Units> mm = m;
             DataSourceResult result = mm.ToDataSourceResult(request);
             return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
           }
           else
           {
               var m = db.Stock_Units.OrderByDescending(b => b.Id).Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true).Take(20);
               IQueryable<Stock_Units> mm = m;
               DataSourceResult result = mm.ToDataSourceResult(request);
               return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);

           }
          
            //}
        }




    }
}
