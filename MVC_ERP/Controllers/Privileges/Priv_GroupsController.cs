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
    public class Priv_GroupsController : BaseController
    {
        
         [Authorize]
        public ActionResult Index(int? Id, Priv_Groups MainTbl,int? CompanyListID)
         {

           PrepareUserDat();
          
               //Read function
               if (Id == null)
               {
                   Id = db.Priv_Groups.Where(t => t.MemberShipId == MemberShipId && t.Deleted != true).Max(c => (int?)c.Id);
               }

               if (Id != null)

               {

               MainTbl = db.Priv_Groups.Find(Id);
               }

               ViewData["TVDS"] = Treeview(0, "", 0, 0, Id ?? 0);


               ModelState.Clear();

               if (ModelState.IsValid)
               {
               return View(MainTbl);
               }

               return View(MainTbl);
             



       }

        // Insert Function
         [Authorize]
        public ActionResult Insert(int Id, string Code, string NameA, string NameE, string Notes, string xmlNodes, string xmlGrid, int SelectedId, int RadioType, bool Insert, bool Modifay, bool Reserve, bool Delete, bool Reuse, bool Post, bool UnPost, bool Share, bool Search, bool Preview, bool Print, bool Export)

        {
            PrepareUserDat();

            bool IsNewGroup = false;
            bool IsNewUser = false;
            //insert or update header

            //Insert New
            Priv_Groups MainTbl = new Priv_Groups();
            if (Id == 0)
            {
                IsNewGroup = true;

                if (Code == null || Code == "")
                 {
                     //Generate auto code
                     int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Priv_Groups  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault(); 
                     MainTbl.Code = Max.ToString();         
                  }
                 else
                 {
                     MainTbl.Code = Code;
                 }

                    MainTbl.Id = ( db.Priv_Groups.Max(c => (int?)c.Id)??0) +1;
                    MainTbl.MemberShipId = MemberShipId;
                    MainTbl.NameA = NameA;
                    MainTbl.NameE = NameE;
                    MainTbl.Notes = Notes;
                
                    MainTbl.Create_Uid = UserId;
                    MainTbl.Create_Date = DateTime.Now;

                    db.Priv_Groups.Add(MainTbl);

               

             
                }

                else
                    //Update
                {
                    IsNewGroup = false;

                    MainTbl = db.Priv_Groups.Find(Id);
                    if (MainTbl.Post == true)
                    {
                        return Json(new { MessageType = "info", ReturnMessage = "This movment was posting you can't modify it" });
                    }


                    db.Entry(MainTbl).State = EntityState.Modified;

                    if (Code == null || Code=="")
                    {
                        //Generate auto code
                        int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Priv_Groups  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();
                        MainTbl.Code = Max.ToString();
                    }
                    else
                    {
                        MainTbl.Code = Code;
                    }
                
                    MainTbl.NameA = NameA;
                    MainTbl.NameE = NameE;
                    MainTbl.Notes = Notes;


                    MainTbl.Write_Uid = UserId;
                    MainTbl.Write_Date = DateTime.Now;

                 

                }



            //Detail
            //var states = (JObject)xmlNodes;
            string m = "{items:" + xmlNodes + "}";
            JavaScriptSerializer serializer1 = new JavaScriptSerializer();

            XmlDocument xml = JsonConvert.DeserializeXmlNode(m);
            XmlReader xr = new XmlNodeReader(xml);

            DataSet ds = new DataSet(xmlNodes);
            ds.ReadXml(xr);

            DataTable TblMenue = new DataTable();
            TblMenue= ds.Tables[0];
            TblMenue.Columns["Notes"].ColumnName = "DetailID";

            Priv_Groups_Rights PermissionTbl=new Priv_Groups_Rights();


        

            //Applay authentication for only selected menu
            if (RadioType == 1)
            {
               
  

                DataView view = new DataView(TblMenue);
                view.RowFilter = "Id=" + SelectedId + "";
               
                 //check previous 
                if (view[0]["DetailID"].ToString() == "0")
                {
                    //Not Found
                    PermissionTbl.Id = (db.Priv_Groups_Rights.Max(c => (int?)c.Id) ?? 0) + 1;
                    PermissionTbl.MemberShipId = MemberShipId;
                    PermissionTbl.GroupId = MainTbl.Id;
                    PermissionTbl.MenuId = Convert.ToInt16(view[0]["Id"]);
                    PermissionTbl.Open = (Convert.ToBoolean(view[0]["HasChildren"]) == true ? false : Convert.ToBoolean(view[0]["checked"]));

                    if (Convert.ToBoolean(view[0]["checked"])==true)
                    {
                    PermissionTbl.Insert = Insert;
                    PermissionTbl.Modifay = Modifay;
                    PermissionTbl.Reserve = Reserve;
                    PermissionTbl.Delete = Delete;
                    PermissionTbl.Reuse = Reuse;
                    PermissionTbl.Post = Post;
                    PermissionTbl.UnPost = UnPost;
                    PermissionTbl.Share = Share;
                    PermissionTbl.Search = Search;
                    PermissionTbl.Preview = Preview;
                    PermissionTbl.Print = Print;
                    PermissionTbl.Export = Export;
                    }
                    db.Priv_Groups_Rights.Add(PermissionTbl);

                }
                else
                {
                    //Found
                    PermissionTbl = db.Priv_Groups_Rights.Find(Convert.ToInt16(view[0]["DetailID"]));

                    db.Entry(PermissionTbl).State = EntityState.Modified;

                    PermissionTbl.Open = (Convert.ToBoolean(view[0]["HasChildren"]) == true ? false : Convert.ToBoolean(view[0]["checked"]));
                    if (Convert.ToBoolean(view[0]["checked"]) == true)
                    {
                        PermissionTbl.Insert = Insert;
                        PermissionTbl.Modifay = Modifay;
                        PermissionTbl.Reserve = Reserve;
                        PermissionTbl.Delete = Delete;
                        PermissionTbl.Reuse = Reuse;
                        PermissionTbl.Post = Post;
                        PermissionTbl.UnPost = UnPost;
                        PermissionTbl.Share = Share;
                        PermissionTbl.Search = Search;
                        PermissionTbl.Preview = Preview;
                        PermissionTbl.Print = Print;
                        PermissionTbl.Export = Export;
                    }
                    else
                    {
                        PermissionTbl.Insert = false;
                        PermissionTbl.Modifay = false;
                        PermissionTbl.Reserve = false;
                        PermissionTbl.Delete = false;
                        PermissionTbl.Reuse = false;
                        PermissionTbl.Post = false;
                        PermissionTbl.UnPost = false;
                        PermissionTbl.Share = false;
                        PermissionTbl.Search = false;
                        PermissionTbl.Preview = false;
                        PermissionTbl.Print = false;
                        PermissionTbl.Export = false;

                    }
                }
                
                         
         

            }


            //Applay authentication for Menu and it's children
            if (RadioType ==2)
            {
                //Not prepared yet it's need to repeted for every node to get it's parent
              
             

            }

      



            //Applay authentication for all checke menu
            if (RadioType == 3)
            {

             
                for (int i = 0; i <= TblMenue.Rows.Count - 1; i++)
                {

                    PermissionTbl = new Priv_Groups_Rights();
               
                //check previous 
                    if (TblMenue.Rows[i]["DetailID"].ToString() == "0" || IsNewGroup==true)
                {
                    //Not Found
                    PermissionTbl.Id = (db.Priv_Groups_Rights.Max(c => (int?)c.Id) ?? 0) + 1+i;
                    PermissionTbl.MemberShipId = MemberShipId;
                    PermissionTbl.GroupId = MainTbl.Id;
                    PermissionTbl.MenuId = Convert.ToInt16(TblMenue.Rows[i]["Id"]);
                    PermissionTbl.Open = (Convert.ToBoolean(TblMenue.Rows[i]["HasChildren"]) == true ? false : Convert.ToBoolean(TblMenue.Rows[i]["checked"]));
                    if (Convert.ToBoolean(TblMenue.Rows[i]["checked"]) == true)
                    {
                        PermissionTbl.Insert = Insert;
                        PermissionTbl.Modifay = Modifay;
                        PermissionTbl.Reserve = Reserve;
                        PermissionTbl.Delete = Delete;
                        PermissionTbl.Reuse = Reuse;
                        PermissionTbl.Post = Post;
                        PermissionTbl.UnPost = UnPost;
                        PermissionTbl.Share = Share;
                        PermissionTbl.Search = Search;
                        PermissionTbl.Preview = Preview;
                        PermissionTbl.Print = Print;
                        PermissionTbl.Export = Export;
                    }

                    db.Priv_Groups_Rights.Add(PermissionTbl);

                }
                else
                {
                    //Found
                    PermissionTbl = db.Priv_Groups_Rights.Find(Convert.ToInt16(TblMenue.Rows[i]["DetailID"]));

                    db.Entry(PermissionTbl).State = EntityState.Modified;

                    PermissionTbl.Open = (Convert.ToBoolean(TblMenue.Rows[i]["HasChildren"]) == true ? false : Convert.ToBoolean(TblMenue.Rows[i]["checked"]));
                    if (Convert.ToBoolean(TblMenue.Rows[i]["checked"]) == true)
                    {
                        PermissionTbl.Insert = Insert;
                        PermissionTbl.Modifay = Modifay;
                        PermissionTbl.Reserve = Reserve;
                        PermissionTbl.Delete = Delete;
                        PermissionTbl.Reuse = Reuse;
                        PermissionTbl.Post = Post;
                        PermissionTbl.UnPost = UnPost;
                        PermissionTbl.Share = Share;
                        PermissionTbl.Search = Search;
                        PermissionTbl.Preview = Preview;
                        PermissionTbl.Print = Print;
                        PermissionTbl.Export = Export;
                    }
                    else
                    {
                        PermissionTbl.Insert = false;
                        PermissionTbl.Modifay = false;
                        PermissionTbl.Reserve = false;
                        PermissionTbl.Delete = false;
                        PermissionTbl.Reuse = false;
                        PermissionTbl.Post = false;
                        PermissionTbl.UnPost = false;
                        PermissionTbl.Share = false;
                        PermissionTbl.Search = false;
                        PermissionTbl.Preview = false;
                        PermissionTbl.Print = false;
                        PermissionTbl.Export = false;

                    }
                }

                }
              

            }

            //Save users


            var DAllDetails = db.Priv_Users_Groups.Where(p => p.GroupId == MainTbl.Id);
            db.Priv_Users_Groups.RemoveRange(DAllDetails);

            string muser = "{Users:" + xmlGrid + "}";
            JavaScriptSerializer serializerUser = new JavaScriptSerializer();

            XmlDocument xmlUser = JsonConvert.DeserializeXmlNode(muser,"RootUsers");
            XmlReader xrUser = new XmlNodeReader(xmlUser);

            DataSet dsUser = new DataSet(xmlGrid);
            dsUser.ReadXml(xrUser);

            DataTable TblUsers = new DataTable();
            
         if (dsUser.Tables.Count>0)
         { TblUsers = dsUser.Tables[0]; 

            Priv_Users_Groups UsersList = new Priv_Users_Groups();


            for (int i = 0; i <= TblUsers.Rows.Count - 1; i++)
            {

                UsersList = new Priv_Users_Groups();

                //check previous 
                if (TblUsers.Rows[i]["Id"].ToString() == "0" || IsNewUser == true)
                {
                    //Not Found
                    UsersList.Id = (db.Priv_Users_Groups.Max(c => (int?)c.Id) ?? 0) + 1 + i;
                    UsersList.MemberShipId = MemberShipId;
                    UsersList.GroupId = MainTbl.Id;
                    UsersList.UserId = Convert.ToInt16(TblUsers.Rows[i]["UserId"]);
                    UsersList.IsAdmin = Convert.ToBoolean(TblUsers.Rows[i]["IsAdmin"]);
                    db.Priv_Users_Groups.Add(UsersList);

                }
                else
                {
                    //Found
                    UsersList = db.Priv_Users_Groups.Find(Convert.ToInt16(TblUsers.Rows[i]["Id"]));

                    db.Entry(UsersList).State = EntityState.Modified;

                    UsersList.GroupId = MainTbl.Id;
                    UsersList.UserId = Convert.ToInt16(TblUsers.Rows[i]["UserId"]);
                    UsersList.IsAdmin = Convert.ToBoolean(TblUsers.Rows[i]["IsAdmin"]);
                }

            }
         }
            db.SaveChanges();
            return Json(new { Id = MainTbl.Id, Code = MainTbl.Code, NameA = MainTbl.NameA, NameE = MainTbl.NameE, Notes = MainTbl.Notes, MessageType = "success", ReturnMessage = "Save successful" });

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
        public ActionResult Read(int Id, int ReadType)
         {
             PrepareUserDat();

           Priv_Groups MainTbl = new Priv_Groups();

           if (ReadType == Convert.ToInt16(ReadTypes.Empty) )
            {
                //Leave Table empty
            }
            else if (ReadType == Convert.ToInt16(ReadTypes.Current))
            {
                //Read Current

                if (Id==0 || Id==null)
                {
                    MainTbl = new Priv_Groups();
                }
                else
                {
                    MainTbl = db.Priv_Groups.Find(Id);
                }
            }
           else if (ReadType == Convert.ToInt16(ReadTypes.Next))
           {
               //Read Next
               Id = (db.Priv_Groups.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true && c.Id > Id).Min(c => (int?)c.Id) ?? 0);


               if (Id == 0)
               {
                   Id = (db.Priv_Groups.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);

                   MainTbl = db.Priv_Groups.Find(Id);
               }
               else
               {
                   MainTbl = db.Priv_Groups.Find(Id);
               }

           }

           else if (ReadType == Convert.ToInt16(ReadTypes.Previous))
           {
               //Read Previous
               Id = (db.Priv_Groups.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true && c.Id < Id).Max(c => (int?)c.Id) ?? 0);
               if (Id == 0)
               {
                   Id = (db.Priv_Groups.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
                   MainTbl = db.Priv_Groups.Find(Id);
               }
               else
               {
                   MainTbl = db.Priv_Groups.Find(Id);
               }

           }
           else if (ReadType == Convert.ToInt16(ReadTypes.Last))
           {
               //Read Last
               Id = (db.Priv_Groups.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);
               if (Id == 0 || Id == null)
               {
                   MainTbl = new Priv_Groups();
               }
               else
               {
                   MainTbl = db.Priv_Groups.Find(Id);
               }

           }

           else if (ReadType == Convert.ToInt16(ReadTypes.First))
           {
               //Read First
               Id = (db.Priv_Groups.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
               if (Id == 0 || Id == null)
               {
                   MainTbl = new Priv_Groups();
               }
               else
               {
                   MainTbl = db.Priv_Groups.Find(Id);
               }
           }
         
           //Read Related Detail
           string TreeDS = Treeview(0, "", 0, 0, Id );


           return Json(new { Id = MainTbl.Id, Code = MainTbl.Code, NameA = MainTbl.NameA, NameE = MainTbl.NameE, Notes = MainTbl.Notes, TreeDS = TreeDS });

       }
        public ActionResult LoadUserGrid([DataSourceRequest]DataSourceRequest request, int? Id)
        {
            PrepareUserDat();
 
            var m = db.Priv_Users_Groups.Where(b => b.GroupId == Id && b.MemberShipId==MemberShipId);
            IQueryable<Priv_Users_Groups> mm = m;
            DataSourceResult result = mm.ToDataSourceResult(request);
            return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);

        }


        //protected Boolean InsertMenueAndChildren(DataTable TblMenue, int GroupId,int DetailId,int MenueId,bool Open,bool Insert, bool Modifay, bool Reserve, bool Delete, bool Reuse, bool Post, bool UnPost, bool Share, bool Search, bool Preview, bool Print, bool Export)
        //{
        //    for (int i = 0; i <= TblMenue.Rows.Count - 1; i++)
        //    {

        //        Priv_Groups_Rights PermissionTbl = new Priv_Groups_Rights();

        //        //check previous 
        //        if (TblMenue.Rows[i]["DetailID"].ToString() == "0")
        //        {
        //            //Not Found
        //            PermissionTbl.Id = (db.Priv_Groups_Rights.Max(c => (int?)c.Id) ?? 0) + 1 + i;
        //            PermissionTbl.MemberShipId = Convert.ToInt32(Session["MemberShipId"]);
        //            PermissionTbl.GroupId = MainTbl.Id;
        //            PermissionTbl.MenuId = Convert.ToInt16(TblMenue.Rows[i]["Id"]);
        //            PermissionTbl.Open = (Convert.ToBoolean(TblMenue.Rows[i]["HasChildren"]) == true ? false : Convert.ToBoolean(TblMenue.Rows[i]["checked"]));
        //            PermissionTbl.Insert = Insert;
        //            PermissionTbl.Modifay = Modifay;
        //            PermissionTbl.Reserve = Reserve;
        //            PermissionTbl.Delete = Delete;
        //            PermissionTbl.Reuse = Reuse;
        //            PermissionTbl.Post = Post;
        //            PermissionTbl.UnPost = UnPost;
        //            PermissionTbl.Share = Share;
        //            PermissionTbl.Search = Search;
        //            PermissionTbl.Preview = Preview;
        //            PermissionTbl.Print = Print;
        //            PermissionTbl.Export = Export;

        //            db.Priv_Groups_Rights.Add(PermissionTbl);

        //        }
        //        else
        //        {
        //            //Found
        //            PermissionTbl = db.Priv_Groups_Rights.Find(Convert.ToInt16(TblMenue.Rows[i]["DetailID"]));

        //            db.Entry(PermissionTbl).State = EntityState.Modified;

        //            PermissionTbl.Open = (Convert.ToBoolean(TblMenue.Rows[i]["HasChildren"]) == true ? false : Convert.ToBoolean(TblMenue.Rows[i]["checked"]));
        //            PermissionTbl.Insert = Insert;
        //            PermissionTbl.Modifay = Modifay;
        //            PermissionTbl.Reserve = Reserve;
        //            PermissionTbl.Delete = Delete;
        //            PermissionTbl.Reuse = Reuse;
        //            PermissionTbl.Post = Post;
        //            PermissionTbl.UnPost = UnPost;
        //            PermissionTbl.Share = Share;
        //            PermissionTbl.Search = Search;
        //            PermissionTbl.Preview = Preview;
        //            PermissionTbl.Print = Print;
        //            PermissionTbl.Export = Export;
        //        }

        //    }


        //    return true;

        //}

        // Reuse Function
        [Authorize]
        public ActionResult Reuse(int Id, string Code, string NameA, string NameE, string Notes)
        {
            PrepareUserDat();

            Priv_Groups MainTbl=new Priv_Groups();
            if (Id == null || Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = "No data to reuse" });
            }

            
                //Insert New
             
                    //Generate auto code

                 MainTbl.Id = (db.Priv_Groups.Max(c => (int?)c.Id) ?? 0) + 1;
                 int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Priv_Groups  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();

                 MainTbl.Priv_Groups_Rights = db.Priv_Groups_Rights.Where(c => c.GroupId == Id).ToList();



                 MainTbl.Code = Max.ToString();
                 MainTbl.NameA = NameA + Max.ToString();
                 MainTbl.NameE = NameE + Max.ToString();
                 MainTbl.Notes = Notes;

                 MainTbl.Create_Uid = UserId;
                 MainTbl.Create_Date = DateTime.Now;
                 MainTbl.Write_Uid = null;
                 MainTbl.Write_Date = null;
                 MainTbl.Post = null;
                 MainTbl.Post_Uid = null;
                 MainTbl.Post_Date = null;

                

               MainTbl.MemberShipId = MemberShipId;

               db.Priv_Groups.Add(MainTbl);

               db.SaveChanges();



               return Json(new { Id = MainTbl.Id, Code = MainTbl.Code, NameA = MainTbl.NameA, NameE = MainTbl.NameE, Notes = MainTbl.Notes, MessageType = "success", ReturnMessage = "Save successful" });


        }
         [Authorize]
        public ActionResult Delete(int Id)
        {
            PrepareUserDat();
            if (Id == null || Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = "No data to delete" });
            }

            Priv_Groups MainTbl = db.Priv_Groups.Find(Id);

          

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
            if (Id == null || Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = "No data to post" });
            }

            Priv_Groups MainTbl = db.Priv_Groups.Find(Id);

          
            if (MainTbl.Post == true)
            {
                //Message for postiong
                return Json(new { MessageType = "info", ReturnMessage = "This movement already posting" });
            }


            db.Entry(MainTbl).State = EntityState.Modified;


            MainTbl.Post = true;
            MainTbl.Post_Uid = UserId;
            MainTbl.Post_Date = DateTime.Now;

            db.SaveChanges();
            MemberShipId = 1;


            return Json(new { Id = Id, MessageType = "success", ReturnMessage = "Save successful" });

        }
        protected Boolean Post(Priv_Groups MainTbl)
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
           MainTbl.Post_Uid = UserId;
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
        public ActionResult Search([DataSourceRequest]DataSourceRequest request, string TxtSearchVal)
        {
            PrepareUserDat();
           if (TxtSearchVal !=null)
           {
             var m = db.Priv_Groups.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true && (b.Code.Contains(TxtSearchVal) || b.NameE.Contains(TxtSearchVal) || b.NameA.Contains(TxtSearchVal) || b.Notes.Contains(TxtSearchVal))).Take(20);
             IQueryable<Priv_Groups> mm = m;
             DataSourceResult result = mm.ToDataSourceResult(request);
             return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
           }
           else
           {
               var m = db.Priv_Groups.OrderByDescending(b => b.Id).Where(b => b.MemberShipId == MemberShipId && b.Deleted != true).Take(20);
               IQueryable<Priv_Groups> mm = m;
               DataSourceResult result = mm.ToDataSourceResult(request);
               return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);

           }
          
            //}
        }

        public ActionResult TreeViewChanged(int SelectedId, int GroupId)
        {
            PrepareUserDat();
            Priv_Groups_Rights Tbl = new Priv_Groups_Rights();

            var DetailId = db.Priv_Groups_Rights.Where(c => c.MenuId == SelectedId && c.GroupId == GroupId).Select(c => c.Id).FirstOrDefault();

            if (DetailId != 0)
            {
                Tbl = db.Priv_Groups_Rights.Find(DetailId);
            }
            return Json(Tbl, JsonRequestBehavior.AllowGet);


        }
        int mainNode = 0;
        int childquantity = 0;
        int myflag;
        public string Treeview(int itemID, string mystr, int j, int flag,int GroupId)
        {
            PrepareUserDat();
            List<Priv_Menus> querylist = new List<Priv_Menus>();
            var ctx = new MSDBContext();

            if (flag == 0)
            {

            

                //Join static list menus with real menus in database
                 querylist = (from List in ctx.Priv_Menus
                             join
                                 DBase in ctx.Priv_Groups_Rights.Where(c => c.GroupId == GroupId) on List.Id equals DBase.MenuId into output
                             from Real in output.DefaultIfEmpty()

                             select new 
                             {
                                 Id = List.Id,
                                 NameE = List.NameE,
                                 NameA = List.NameA,
                                 ParentId = List.ParentId,
                                 Expanded = List.Expanded,
                                 Checked = (Real == null ? false : Real.Open),
                                 Notes = (Real == null ? "0" : SqlFunctions.StringConvert((double)Real.Id)), //Put Id for table  Priv_Groups_Rights to use it in checking
                                 Sold=List.Sold,                      
                                 HasChildren = List.HasChildren
                               
                             }

                             ).Where(List => List.ParentId == null && List.Sold==true).ToList()
                             .Select(x => new Priv_Menus { Id = x.Id, NameE = x.NameE, NameA = x.NameA, ParentId = x.ParentId,Expanded=x.Expanded, Checked=x.Checked ,Notes=x.Notes,HasChildren=x.HasChildren}).ToList(); 



                mainNode = querylist.Count;

                mystr += "[";
            }
            if (flag == 1)
            {

             
                //get parent menus 
                querylist = (from List in ctx.Priv_Menus
                             join
                                 DBase in ctx.Priv_Groups_Rights.Where(c => c.GroupId == GroupId) on List.Id equals DBase.MenuId  into output
                             from Real in output.DefaultIfEmpty()

                             select new
                             {
                                 Id = List.Id,
                                 NameE = List.NameE,
                                 NameA = List.NameA,
                                 ParentId = List.ParentId,
                                 Expanded = List.Expanded,
                                 Checked = (Real == null ? false : Real.Open),
                                 Notes = (Real == null ? "0" : SqlFunctions.StringConvert((double)Real.Id)), //Put Id for table  Priv_Groups_Rights to use it in checking
                                 Sold = List.Sold,
                                  HasChildren = List.HasChildren
                             }

                            ).Where(List => List.ParentId == itemID && List.Sold==true).ToList()
                            .Select(x => new Priv_Menus { Id = x.Id, NameE = x.NameE, NameA = x.NameA, ParentId = x.ParentId, Expanded = x.Expanded, Checked = x.Checked,Notes=x.Notes,HasChildren=x.HasChildren }).ToList(); 



                mainNode = querylist.Count;
                mystr += "items:[";
            }

            //Below line shows an example of how to make parent node with his child
            //[{ id: "1", text: "P1", items: [{ id: "5", text: "P2" }] }]


            int i = 1;
            foreach (var item in querylist)
            {
                myflag = 0;
                //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Open ? "true" : "false") + "";
                mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + item.NameA + "\",NameE:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",checked:" + (item.Checked ? "true" : "false") + ",expanded:" + (item.Expanded ? "true" : "false") + "" + ",HasChildren:" + (item.HasChildren ? "true" : "false") + ",text:\"" + item.NameE + "\",UrlLink:\"" + item.UrlLink + "\",ImageUrlLink:\"" + item.ImageUrlLink + "\",Target:\"" + item.Target + "\",HtmlAttributes:\"" + item.HtmlAttributes + "\",OnlyForProgrammer:" + (item.OnlyForProgrammer ? "true" : "false") + ",Notes:\"" + item.Notes + "\"";

                List<Priv_Menus> querylistParent = new List<Priv_Menus>();
                //Check this parent has child or not , if yes how many?

            

                querylistParent = (from List in ctx.Priv_Menus
                             join
                                 DBase in ctx.Priv_Groups_Rights.Where(c => c.GroupId == GroupId) on List.Id equals DBase.MenuId into output
                             from Real in output.DefaultIfEmpty()

                             select new
                             {
                                 Id = List.Id,
                                 NameE = List.NameE,
                                 NameA = List.NameA,
                                 ParentId = List.ParentId,
                                 Expanded = List.Expanded,
                                 Checked = (Real == null ? false : Real.Open),
                                 Notes = (Real == null ? "0" : SqlFunctions.StringConvert((double)Real.Id)), //Put Id for table  Priv_Groups_Rights to use it in checking
                                 Sold = List.Sold,
                                 HasChildren = List.HasChildren
                             }

                               ).Where(List => List.ParentId == item.Id && List.Sold == true).ToList()
                               .Select(x => new Priv_Menus { Id = x.Id, NameE = x.NameE, NameA = x.NameA, ParentId = x.ParentId, Expanded = x.Expanded, Checked = x.Checked,Notes=x.Notes,HasChildren=x.HasChildren }).ToList(); 






                childquantity = querylistParent.Count;
                //If Parent Has Child again call Treeview with new parameter
                if (childquantity > 0)
                {
                    mystr = Treeview(item.Id, mystr, i, 1,GroupId);

                }
                //No Child and No Last Node
                else if (childquantity == 0 && i < querylist.Count)
                {
                    mystr += "},";
                }
                //No Child and Last Node
                else if (childquantity == 0 && i == querylist.Count)
                {

                    int fcheck2 = 0;
                    int fcheck3 = 0;
                    int counter = 0;
                    int flagbreak = 0;

                    int currentparent;
                    List<Priv_Menus> parentquery;
                    List<Priv_Menus> childlistquery;
                    TempData["counter"] = 0;
                    currentparent = Convert.ToInt16(item.ParentId);
                    int coun;
                    while (currentparent != 0)
                    {
                        //count parent of parent

                        fcheck2 = 0;
                        fcheck3 = 0;
                        parentquery = new List<Priv_Menus>();

                     

                        parentquery = (from List in ctx.Priv_Menus
                                     join
                                         DBase in ctx.Priv_Groups_Rights.Where(c => c.GroupId == GroupId) on List.Id equals DBase.MenuId into output
                                     from Real in output.DefaultIfEmpty()

                                     select new
                                     {
                                         Id = List.Id,
                                         NameE = List.NameE,
                                         NameA = List.NameA,
                                         ParentId = List.ParentId,
                                         Expanded = List.Expanded,
                                         Checked = (Real == null ? false : Real.Open),
                                         Notes = (Real == null ? "0" : SqlFunctions.StringConvert((double)Real.Id)), //Put Id for table  Priv_Groups_Rights to use it in checking
                                         Sold = List.Sold ,
                                         HasChildren = List.HasChildren
                                     }

                                ).Where(List => List.Id == currentparent && List.Sold == true).ToList()
                                .Select(x => new Priv_Menus { Id = x.Id, NameE = x.NameE, NameA = x.NameA, ParentId = x.ParentId, Expanded = x.Expanded, Checked = x.Checked, Notes = x.Notes, HasChildren=x.HasChildren }).ToList(); 






                        var rep2 = (from h in parentquery select new { h.ParentId }).First();

                        //put {[ up to end

                        //list of child
                        childlistquery = new List<Priv_Menus>();

                 

                        childlistquery = (from List in ctx.Priv_Menus
                                       join
                                           DBase in ctx.Priv_Groups_Rights.Where(c => c.GroupId == GroupId) on List.Id equals DBase.MenuId into output
                                       from Real in output.DefaultIfEmpty()

                                       select new
                                       {
                                           Id = List.Id,
                                           NameE = List.NameE,
                                           NameA = List.NameA,
                                           ParentId = List.ParentId,
                                           Expanded = List.Expanded,
                                           Checked = (Real == null ? false : Real.Open),
                                           Notes = (Real == null ? "0" : SqlFunctions.StringConvert((double)Real.Id)), //Put Id for table  Priv_Groups_Rights to use it in checking
                                           Sold = List.Sold ,
                                           HasChildren = List.HasChildren
                                       }

                                 ).Where(List => List.ParentId == currentparent && List.Sold == true).ToList()
                                 .Select(x => new Priv_Menus { Id = x.Id, NameE = x.NameE, NameA = x.NameA, ParentId = x.ParentId, Expanded = x.Expanded, Checked = x.Checked,Notes=x.Notes,HasChildren= x.HasChildren }).ToList(); 




                        foreach (var item22 in childlistquery)
                        {


                            if (mystr.Contains(item22.Id.ToString()))
                            {

                                if (item22.ParentId == currentparent)
                                {
                                    fcheck3 += 1;
                                    if (fcheck3 == 1)
                                    {
                                        counter += 1;
                                    }
                                }

                            }
                            else
                            {

                                myflag = 1;
                                if (item22.ParentId == currentparent)
                                {
                                    fcheck2 += 1;
                                    if (fcheck2 == 1)
                                    {
                                        counter -= 1;
                                        flagbreak = 1;

                                    }
                                }

                            }
                        }

                        var result55 = (from h in parentquery select new { h.Id }).First();
                        coun = Convert.ToInt16(result55.Id);
                        TempData["coun"] = Convert.ToInt16(coun);
                        currentparent = Convert.ToInt16(rep2.ParentId);
                        if (flagbreak == 1)
                        {
                            break;
                        }

                    }

                    for (int i2 = 0; i2 < counter; i2++)
                    {
                        mystr += "}]";
                    }

                    List<Priv_Menus> lastchild = new List<Priv_Menus>();

                    //lastchild = (from m in ctx.Priv_Menus
                    //             where m.ParentId == item.ParentId
                    //             select m).ToList();


                    lastchild = (from List in ctx.Priv_Menus
                                      join
                                          DBase in ctx.Priv_Groups_Rights.Where(c => c.GroupId == GroupId) on List.Id equals DBase.MenuId into output
                                      from Real in output.DefaultIfEmpty()

                                      select new
                                      {
                                          Id = List.Id,
                                          NameE = List.NameE,
                                          NameA = List.NameA,
                                          ParentId = List.ParentId,
                                          Expanded = List.Expanded,
                                          Checked = (Real == null ? false : Real.Open),
                                          Notes = (Real == null ? "0" : SqlFunctions.StringConvert((double)Real.Id)), //Put Id for table  Priv_Groups_Rights to use it in checking
                                          Sold = List.Sold ,
                                          HasChildren = List.HasChildren

                                      }

                             ).Where(List => List.ParentId == item.ParentId && List.Sold == true).ToList()
                             .Select(x => new Priv_Menus { Id = x.Id, NameE = x.NameE, NameA = x.NameA, ParentId = x.ParentId, Expanded = x.Expanded, Checked = x.Checked,Notes=x.Notes,HasChildren=x.HasChildren }).ToList(); 


                    List<Priv_Menus> lastparent = new List<Priv_Menus>();

                


                    lastparent = (from List in ctx.Priv_Menus
                                 join
                                     DBase in ctx.Priv_Groups_Rights.Where(c => c.GroupId == GroupId) on List.Id equals DBase.MenuId into output
                                 from Real in output.DefaultIfEmpty()

                                 select new
                                 {
                                     Id = List.Id,
                                     NameE = List.NameE,
                                     NameA = List.NameA,
                                     ParentId = List.ParentId,
                                     Expanded = List.Expanded,
                                     Checked = (Real == null ? false : Real.Open),
                                     Notes = (Real == null ? "0" : SqlFunctions.StringConvert((double)Real.Id)), //Put Id for table  Priv_Groups_Rights to use it in checking
                                     Sold = List.Sold ,
                                     HasChildren = List.HasChildren
                                 }

                             ).Where(List => List.ParentId == null && List.Sold == true).ToList()
                             .Select(x => new Priv_Menus { Id = x.Id, NameE = x.NameE, NameA = x.NameA, ParentId = x.ParentId, Expanded = x.Expanded, Checked = x.Checked ,Notes=x.Notes,HasChildren=x.HasChildren}).ToList(); 



                    if (lastchild.Count > 0)
                    {
                        var result_lastchild = (from h in lastchild select new { h.Id }).Last();
                        var result_lastparent = (from h in lastparent select new { h.Id }).Last();
                        int mycount = Convert.ToInt16(TempData["coun"]);
                        if (item.Id == result_lastchild.Id && mycount == result_lastparent.Id && myflag == 0)
                        {
                            mystr += "}]";
                        }
                        else if (item.Id == result_lastchild.Id && mycount == result_lastparent.Id && myflag == 1)
                        {
                            mystr += "},";
                        }
                        //Ms 7-9-2014 to handle first level node without parent
                        else if (item.Id == result_lastchild.Id && item.ParentId == null)
                        {
                            mystr += "}]";
                        }
                        //End Ms
                        else if (item.Id == result_lastchild.Id && mycount != result_lastparent.Id)
                        {
                            mystr += "},";
                        }

                    }
                    //  finish }]
                    else if (lastchild.Count == 0 && item.ParentId == null)
                    {
                        mystr += "}]";
                    }
                }
                i++;
            }


            return mystr;
        }

        public ActionResult LoadDetaiTree([DataSourceRequest]DataSourceRequest request, int Id)
        {
            PrepareUserDat();
            ViewData["TVDS"] = Treeview(0, "", 0, 0,Id);
            return Json("");

        }


    }
}
