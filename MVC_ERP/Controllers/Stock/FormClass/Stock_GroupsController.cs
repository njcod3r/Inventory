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
using MVC_ERP.Controllers.General ;




namespace MVC_ERP.Controllers
{
    public class Stock_GroupsController : BaseController
    {
        
         [Authorize]
        public ActionResult Index(int? Id, Stock_ProductGroups MainTbl,int? CompanyListID)
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

        // Insert Function
         [Authorize]
         public ActionResult Insert(int Id, int CompanyId, string Code, string NameE, string NameA, int? ParentId, bool HasChildren, string Notes)

        {
            PrepareUserDat();
            if (!CheckUserPermission((Id == 0 ? PermissionType.Insert : PermissionType.Modifay))) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });
           
             //Check if  new insert under record has parent 
            Stock_ProductGroups tblCheck = db.Stock_ProductGroups.Find(ParentId);
            if (tblCheck != null)
            {
                if (tblCheck.HasChildren == false)
                {
                    return Json(new { Id = Id, MessageType = "danger", ReturnMessage = "Sorry you can't insert children under this record, make it to allow childern first" });
                }
            }
            //prevent update parent and set has sub = 0 where it has childeren
            if (Id != 0 && HasChildren == false)
            {
                Stock_ProductGroups mm = db.Stock_ProductGroups.Where(t => t.ParentId == Id && t.Deleted != true).FirstOrDefault();
                if (mm != null)
                {
                    if (mm.Id != null)
                    {
                        return Json(new { Id = Id, MessageType = "danger", ReturnMessage = "Sorry you can't remove has childern attribute from this record cause it's already has childeren" });

                    }
                }
            }

            //prevent set parent to itself

              if (Id ==ParentId)
              {
                  return Json(new { Id = Id, MessageType = "danger", ReturnMessage = "Sorry you can't set this parent for current item the item and parent is similar" });
              }

              //prevent change parent of first level
              if (Id != 0) { 
              int? PreviousParentId = db.Stock_ProductGroups.Find(Id).ParentId.GetValueOrDefault();
              if (PreviousParentId==0 && ParentId !=null)
                {
                  return Json(new { Id = Id, MessageType = "danger", ReturnMessage = "Sorry you can't change parent of first level" });
                }
              }

            //Insert New
            Stock_ProductGroups MainTbl = new Stock_ProductGroups();
            if (Id == 0)
            {

                   MainTbl.Id = (db.Stock_ProductGroups.Max(c => (int?)c.Id) ?? 0) + 1;
                    MainTbl.MemberShipId = MemberShipId;
                    MainTbl.CompanyId = CompanyId;
                    int CurrLevel;
                    if (ParentId == null)
                    {
                        CurrLevel = 1;
                    }
                    else
                    {
                        CurrLevel = tblCheck.Levels + 1; //ParentId level +1
                    }
                    if (Code == null || Code == "")
                    {
                        //Generate auto code

                        string getcode = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0) from dbo.Stock_ProductGroups  WHERE (isnumeric(Code) = 1) and (ISNULL(ParentId, 0) = " + (ParentId == null ? 0 : ParentId) + ") and (MemberShipId = " + MemberShipId + ") and  (CompanyId = " + CompanyId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault().ToString();
                       if (getcode == "0" )
                        {
                        getcode = "1";
                        }
                        else
                        {
                         getcode =(Convert.ToInt64(getcode) + 1).ToString();
                        }
                       string getparentcode = db.Database.SqlQuery<string>("select Code from dbo.Stock_ProductGroups  WHERE (Id = " + (ParentId == null ? 0 : ParentId) + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();

              
                      string MakeCode;
                      if (CurrLevel>1) 
                        {
                      if (getcode == "1") 
                        MakeCode = getparentcode + 0 + getcode;
                       else
                        MakeCode = getcode;
                   
                       }
                      else
                        {
                      if (getcode == "1")
                        MakeCode = getparentcode + getcode;
                       else
                        MakeCode = getcode;
                  

                       }

                   MainTbl.Code = MakeCode;
                    }
                    else
                    {
                        MainTbl.Code = Code;
                    }
                  
                    MainTbl.NameE = NameE;
                    MainTbl.NameA = NameA;
                    MainTbl.ParentId = ParentId;
                    MainTbl.HasChildren = HasChildren;
                    MainTbl.Levels = CurrLevel;

                    MainTbl.Notes = Notes;
                
                    MainTbl.Create_Uid = UserId;
                    MainTbl.Create_Date = DateTime.Now;


                }

                else
                    //Update
                {



                    MainTbl = db.Stock_ProductGroups.Find(Id);
                    if (MainTbl.Post == true)
                    {
                        return Json(new { MessageType = "info", ReturnMessage = "This movment was posting you can't modify it" });
                    }


                    MainTbl.MemberShipId = MemberShipId;
                    int CurrLevel;
                    if (ParentId == null)
                    {
                        CurrLevel = 1;
                    }
                    else
                    {
                        CurrLevel = tblCheck.Levels + 1; //ParentId level +1
                    }
                    if (Code == null || Code == "")
                    {
                        //Generate auto code

                        string getcode = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0) from dbo.Stock_ProductGroups  WHERE (isnumeric(Code) = 1) and (ISNULL(ParentId, 0) = " + (ParentId == null ? 0 : ParentId) + ") and (MemberShipId = " + MemberShipId + ") and  (CompanyId = " + CompanyId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault().ToString();
                        if (getcode == "0")
                        {
                            getcode = "1";
                        }
                        else
                        {
                            getcode = (Convert.ToInt64(getcode) + 1).ToString();
                        }
                        string getparentcode = db.Database.SqlQuery<string>("select Code from dbo.Stock_ProductGroups  WHERE (Id = " + (ParentId == null ? 0 : ParentId) + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();


                        string MakeCode;
                        if (CurrLevel > 1)
                        {
                            if (getcode == "1")
                                MakeCode = getparentcode + 0 + getcode;
                            else
                                MakeCode = getcode;

                        }
                        else
                        {
                            if (getcode == "1")
                                MakeCode = getparentcode + getcode;
                            else
                                MakeCode = getcode;


                        }

                        MainTbl.Code = MakeCode;
                    }
                    else
                    {
                        MainTbl.Code = Code;
                    }

                    MainTbl.NameE = NameE;
                    MainTbl.NameA = NameA;
                    MainTbl.ParentId = ParentId;
                    MainTbl.HasChildren = HasChildren;
                    MainTbl.Levels = CurrLevel;
                    MainTbl.Notes = Notes;
                

                    MainTbl.Write_Uid = UserId;
                    MainTbl.Write_Date = DateTime.Now;

                 

                }


         
            if (Id == 0)
            {
                //New
                db.Stock_ProductGroups.Add(MainTbl);
            }
            else
            {
                //Update
                db.Entry(MainTbl).State = EntityState.Modified;
            }


            db.SaveChanges();

            //string NodePath="";
            // Stock_ProductGroups TBLPath;
            // int? CID=MainTbl.ParentId;
            //for (int i = 1; i <= MainTbl.Levels; i++)
            //{
                
            //    TBLPath=new Stock_ProductGroups();
            //    TBLPath = db.Stock_ProductGroups.Find(CID);
            //    NodePath = TBLPath.Id.ToString() + "," + NodePath;

            //     CID = TBLPath.ParentId;
            //}

            return Json(new { Id = MainTbl.Id, MessageType = "success", ReturnMessage = "Save successful" });

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

             Stock_ProductGroups MainTbl = new Stock_ProductGroups();

            if (ReadType == Convert.ToInt16(ReadTypes.Empty) )
            {
                //Leave Table empty
            }
            else if (ReadType == Convert.ToInt16(ReadTypes.Current))
            {
                //Read Current

                if (Id==0 || Id==null)
                {
                    MainTbl = new Stock_ProductGroups();
                }
                else
                {
                    MainTbl = db.Stock_ProductGroups.Find(Id);
                }
            }
            else if (ReadType == Convert.ToInt16(ReadTypes.Next))
            {
                //Read Next
                Id = (db.Stock_ProductGroups.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true && c.Id > Id).Min(c => (int?)c.Id) ?? 0);


                if (Id == 0)
                {
                    Id = (db.Stock_ProductGroups.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);

                    MainTbl = db.Stock_ProductGroups.Find(Id);
                }
                else
                {
                    MainTbl = db.Stock_ProductGroups.Find(Id);
                }

            }

            else if (ReadType == Convert.ToInt16(ReadTypes.Previous))
            {
                //Read Previous
                Id = (db.Stock_ProductGroups.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true && c.Id < Id).Max(c => (int?)c.Id) ?? 0);
                if (Id == 0)
                {
                    Id = (db.Stock_ProductGroups.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
                    MainTbl = db.Stock_ProductGroups.Find(Id);
                }
                else
                {
                    MainTbl = db.Stock_ProductGroups.Find(Id);
                }

            }
           else if (ReadType == Convert.ToInt16(ReadTypes.Last))
           {
               //Read Last
               Id = (db.Stock_ProductGroups.Where(c => c.MemberShipId == MemberShipId && c.CompanyId==CompanyId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);
               if (Id == 0 || Id == null)
               {
                   MainTbl = new Stock_ProductGroups();
               }
               else
               {
                   MainTbl = db.Stock_ProductGroups.Find(Id);
               }

           }

           else if (ReadType == Convert.ToInt16(ReadTypes.First))
           {
               //Read First
               Id = (db.Stock_ProductGroups.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
               if (Id == 0 || Id == null)
               {
                   MainTbl = new Stock_ProductGroups();
               }
               else
               {
                   MainTbl = db.Stock_ProductGroups.Find(Id);
               }
           }


            string NodePath = "";
            Stock_ProductGroups TBLPath;
            int? CID = MainTbl.ParentId;
            for (int i = 1; i <= MainTbl.Levels-1; i++)
            {

                TBLPath = new Stock_ProductGroups();
                TBLPath = db.Stock_ProductGroups.Find(CID);
                NodePath = TBLPath.Id.ToString() + "," + NodePath;

                CID = TBLPath.ParentId;
            }
            if (NodePath.HasValue())
            {

                NodePath = "[" + NodePath + MainTbl.Id + "]";

            }
            else
            {
                NodePath = "[" + MainTbl.Id + "]";
            }

            if (MainTbl.Id == 0)
            {
                System.Web.HttpContext.Current.Session["MainIdReturnBackToDetailPage"] = 0;
                System.Web.HttpContext.Current.Session["MainNameReturnBackToDetailPage"] = "";
            }
            else
            {
                System.Web.HttpContext.Current.Session["MainIdReturnBackToDetailPage"] = MainTbl.Id;
                System.Web.HttpContext.Current.Session["MainNameReturnBackToDetailPage"] = MainTbl.NameE;
            }

            return Json(new { TBL = MainTbl, NodePath= NodePath });
       }
         [Authorize]
         public JsonResult LoadProductGroups(int? Id, int? CompanyId)
         {
             PrepareUserDat();

            

             var CostCenters = from e in db.Stock_ProductGroups
                          where (e.MemberShipId == MemberShipId && e.CompanyId == CompanyId && e.Deleted != true && (Id.HasValue ? e.ParentId == Id : e.ParentId == null))
                          select new
                          {
                              id = e.Id,
                              Name = e.Code + "     " + e.NameE,
                              hasChildren = e.HasChildren
                          };

             return Json(CostCenters, JsonRequestBehavior.AllowGet);
         }
         [Authorize]
         public ActionResult TreeViewChanged(int SelectedId)
         {
             Stock_ProductGroups tbl = db.Stock_ProductGroups.Find(SelectedId);


             if (tbl.Id == 0)
             {
                 System.Web.HttpContext.Current.Session["MainIdReturnBackToDetailPage"] = 0;
                 System.Web.HttpContext.Current.Session["MainNameReturnBackToDetailPage"] = "";
             }
             else
             {
                 System.Web.HttpContext.Current.Session["MainIdReturnBackToDetailPage"] = tbl.Id;
                 System.Web.HttpContext.Current.Session["MainNameReturnBackToDetailPage"] = tbl.NameE;
             }

             return Json(tbl, JsonRequestBehavior.AllowGet);


         }

        // Reuse Function
        [Authorize]
        public ActionResult Reuse(int Id)
        {
            PrepareUserDat();
            if (!CheckUserPermission(PermissionType.Reuse)) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });

            Stock_ProductGroups MainTbl = new Stock_ProductGroups();
            if (Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = "No data to reuse" });
            }

            
                //Insert New
            List<Stock_ProductGroups> listMain = db.Stock_ProductGroups.Where(c => c.Id == Id).ToList();

            foreach (var U in listMain)
            {
                MainTbl.Id = (db.Stock_ProductGroups.Max(c => (int?)c.Id) ?? 0) + 1;
                MainTbl.MemberShipId = MemberShipId;
                MainTbl.CompanyId = U.CompanyId;
                //Generate auto code

                string getcode = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0) from dbo.Stock_ProductGroups  WHERE (isnumeric(Code) = 1) and (ISNULL(ParentId, 0) = " + (U.ParentId == null ? 0 : U.ParentId) + ") and (MemberShipId = " + MemberShipId + ") and  (CompanyId = " + U.CompanyId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault().ToString();
                if (getcode == "0")
                {
                    getcode = "1";
                }
                else
                {
                    getcode = (Convert.ToInt64(getcode) + 1).ToString();
                }
                string getparentcode = db.Database.SqlQuery<string>("select Code from dbo.Stock_ProductGroups  WHERE (Id = " + (U.ParentId == null ? 0 : U.ParentId) + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();


                string MakeCode;
                if (U.Levels > 1)
                {
                    if (getcode == "1")
                        MakeCode = getparentcode + 0 + getcode;
                    else
                        MakeCode = getcode;

                }
                else
                {
                    if (getcode == "1")
                        MakeCode = getparentcode + getcode;
                    else
                        MakeCode = getcode;


                }

                MainTbl.Code = MakeCode;
                MainTbl.NameE = U.NameE + MakeCode;
                MainTbl.NameA = U.NameA + MakeCode;
                MainTbl.ParentId = U.ParentId;
                MainTbl.HasChildren = U.HasChildren;
                MainTbl.Levels = U.Levels;
                MainTbl.Notes = U.Notes;


                MainTbl.Create_Uid = UserId;
                MainTbl.Create_Date = DateTime.Now;
               


               
            }



            db.Stock_ProductGroups.Add(MainTbl);
            db.SaveChanges();

            return Json(new { Id = MainTbl.Id, MessageType = "success", ReturnMessage = "Save successful" });
        }
         [Authorize]
        public ActionResult Delete(int Id)
        {
            PrepareUserDat();
            if (!CheckUserPermission(PermissionType.Delete)) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });

            //prevent delet parent  sub childeren
            if (Id != 0)
            {
                Stock_ProductGroups mm = db.Stock_ProductGroups.Where(t => t.ParentId == Id && t.Deleted != true).FirstOrDefault();
                if (mm != null)
                {
                    if (mm.Id != null)
                    {

                        return Json(new { MessageType = "danger", ReturnMessage = "Sorry you can't delete this record cause it's  has childeren" });
                    }
                }
            }

            if (Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = "No data to delete" });
            }

            Stock_ProductGroups MainTbl = db.Stock_ProductGroups.Find(Id);

          

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

            Stock_ProductGroups MainTbl = db.Stock_ProductGroups.Find(Id);
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
               var m = db.Stock_ProductGroups.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true && (b.Code.Contains(TxtSearchVal) || b.NameE.Contains(TxtSearchVal) || b.NameA.Contains(TxtSearchVal) || b.Notes.Contains(TxtSearchVal))).Take(20);
               IQueryable<Stock_ProductGroups> mm = m;
             DataSourceResult result = mm.ToDataSourceResult(request);
             return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
           }
           else
           {
               var m = db.Stock_ProductGroups.OrderByDescending(b => b.Id).Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true).Take(20);
               IQueryable<Stock_ProductGroups> mm = m;
               DataSourceResult result = mm.ToDataSourceResult(request);
               return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);

           }
          
            //}
        }




    }
}
