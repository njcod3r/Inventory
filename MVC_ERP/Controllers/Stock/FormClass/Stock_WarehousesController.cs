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
    public class Stock_WarehousesController : BaseController
    {

        [Authorize]
        public ActionResult Index(int? Id, Stock_Warehouses MainTbl, int? CompanyListID)
        {

            PrepareUserDat();
            if (!CheckUserPermission(PermissionType.Open)) return Json("", JsonRequestBehavior.AllowGet);



            if (Id != null)
            {

                MainTbl = db.Stock_Warehouses.Find(Id);
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
        public ActionResult Insert(int Id, int CompanyId, string Code, string NameE, string NameA, string Adress, string Tel, string Warehouseman, string Notes, int? Height, int? Area, string xmlShelvesGrid)
        {
            PrepareUserDat();
            if (!CheckUserPermission((Id == 0 ? PermissionType.Insert : PermissionType.Modifay))) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });


            //Insert New
            Stock_Warehouses MainTbl = new Stock_Warehouses();
            if (Id == 0)
            {



                MainTbl.Id = (db.Stock_Warehouses.Max(c => (int?)c.Id) ?? 0) + 1;
                if (Code == null || Code == "")
                {
                    //Generate auto code
                    int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Stock_Warehouses  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();
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
                MainTbl.Adress = Adress;
                MainTbl.Tel = Tel;
                MainTbl.Warehouseman = Warehouseman;
                MainTbl.Height = Height;
                MainTbl.Area = Area;
                MainTbl.Notes = Notes;


                MainTbl.Create_Uid = UserId;
                MainTbl.Create_Date = DateTime.Now;

                db.Stock_Warehouses.Add(MainTbl);




            }

            else
            //Update
            {



                MainTbl = db.Stock_Warehouses.Find(Id);
                if (MainTbl.Post == true)
                {
                    return Json(new { MessageType = "info", ReturnMessage = "This movment was posting you can't modify it" });
                }


                db.Entry(MainTbl).State = EntityState.Modified;

                if (Code == null || Code == "")
                {
                    //Generate auto code
                    int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Stock_Warehouses  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();
                    MainTbl.Code = Max.ToString();
                }
                else
                {
                    MainTbl.Code = Code;
                }

                MainTbl.CompanyId = CompanyId;
                MainTbl.NameA = NameA;
                MainTbl.NameE = NameE;
                MainTbl.Adress = Adress;
                MainTbl.Tel = Tel;
                MainTbl.Warehouseman = Warehouseman;
                MainTbl.Height = Height;
                MainTbl.Area = Area;
                MainTbl.Notes = Notes;

                MainTbl.Write_Uid = UserId;
                MainTbl.Write_Date = DateTime.Now;



            }

            List<Stock_Warehouses_Shelves> ShelvesTblDetail = JsonConvert.DeserializeObject<List<Stock_Warehouses_Shelves>>(xmlShelvesGrid);
           
        



            //Stock_Warehouses_RackRows add to context
            if (ShelvesTblDetail.Count > 0)
            {
                Stock_Warehouses_Shelves SDtlList = new Stock_Warehouses_Shelves();
                MainTbl.Stock_Warehouses_Shelves = new List<Stock_Warehouses_Shelves>();

              
                foreach (var I in ShelvesTblDetail)
                {
                    if (I.Id == 0)
                    {

                        SDtlList = new Stock_Warehouses_Shelves();
                        SDtlList.WarehouseId = MainTbl.Id;
                        SDtlList.ShelveNo = I.ShelveNo;
                        SDtlList.Notes = I.Notes;

                        MainTbl.Stock_Warehouses_Shelves.Add(SDtlList);
                     
                    }
                    else
                    {
                        SDtlList = db.Stock_Warehouses_Shelves.Find(I.Id);
                        db.Entry(SDtlList).State = EntityState.Modified;
                     
                        SDtlList.WarehouseId = MainTbl.Id;
                        SDtlList.ShelveNo = I.ShelveNo;
                        SDtlList.Notes = I.Notes;

                    }
               
                }
                      

            }

            db.SaveChanges();

            return Json(new { Id = MainTbl.Id, MessageType = "success", ReturnMessage = "Save successful" });

        }

        enum ReadTypes
        {
            Empty = 0,
            First = 1,
            Next = 2,
            Current = 3,
            Previous = 4,
            Last = 5
        }
        [Authorize]
        public ActionResult Read(int Id, int? CompanyId, int ReadType)
        {
            PrepareUserDat();

            Stock_Warehouses MainTbl = new Stock_Warehouses();

            if (ReadType == Convert.ToInt16(ReadTypes.Empty))
            {
                //Leave Table empty
            }
            else if (ReadType == Convert.ToInt16(ReadTypes.Current))
            {
                //Read Current

                if (Id == 0 || Id == null)
                {
                    MainTbl = new Stock_Warehouses();
                }
                else
                {
                    MainTbl = db.Stock_Warehouses.Find(Id);
                }
            }
            else if (ReadType == Convert.ToInt16(ReadTypes.Next))
            {
                //Read Next
                Id = (db.Stock_Warehouses.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true && c.Id > Id).Min(c => (int?)c.Id) ?? 0);


                if (Id == 0)
                {
                    Id = (db.Stock_Warehouses.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);

                    MainTbl = db.Stock_Warehouses.Find(Id);
                }
                else
                {
                    MainTbl = db.Stock_Warehouses.Find(Id);
                }

            }

            else if (ReadType == Convert.ToInt16(ReadTypes.Previous))
            {
                //Read Previous
                Id = (db.Stock_Warehouses.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true && c.Id < Id).Max(c => (int?)c.Id) ?? 0);
                if (Id == 0)
                {
                    Id = (db.Stock_Warehouses.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
                    MainTbl = db.Stock_Warehouses.Find(Id);
                }
                else
                {
                    MainTbl = db.Stock_Warehouses.Find(Id);
                }

            }
            else if (ReadType == Convert.ToInt16(ReadTypes.Last))
            {
                //Read Last
                Id = (db.Stock_Warehouses.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);
                if (Id == 0 || Id == null)
                {
                    MainTbl = new Stock_Warehouses();
                }
                else
                {
                    MainTbl = db.Stock_Warehouses.Find(Id);
                }

            }

            else if (ReadType == Convert.ToInt16(ReadTypes.First))
            {
                //Read First
                Id = (db.Stock_Warehouses.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
                if (Id == 0 || Id == null)
                {
                    MainTbl = new Stock_Warehouses();
                }
                else
                {
                    MainTbl = db.Stock_Warehouses.Find(Id);
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

            Stock_Warehouses MainTbl = new Stock_Warehouses();
            if (Id == null || Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = "No data to reuse" });
            }


            //Insert New
            List<Stock_Warehouses> listMain = db.Stock_Warehouses.Where(c => c.Id == Id).ToList();

            foreach (var U in listMain)
            {
                MainTbl.Id = (db.Stock_Warehouses.Max(c => (int?)c.Id) ?? 0) + 1;

                int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Stock_Warehouses  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();
                MainTbl.Code = Max.ToString();
                MainTbl.MemberShipId = MemberShipId;
                MainTbl.NameA = U.NameA + Max;
                MainTbl.NameE = U.NameE + Max;
                MainTbl.CompanyId = U.CompanyId;
                MainTbl.Notes = U.Notes;
                MainTbl.Adress = U.Adress;
                MainTbl.Tel = U.Tel;
                MainTbl.Warehouseman = U.Warehouseman;

                MainTbl.Create_Uid = UserId;
                MainTbl.Create_Date = DateTime.Now;




            }



            db.Stock_Warehouses.Add(MainTbl);
            db.SaveChanges();

            return Json(new { Id = MainTbl.Id, MessageType = "success", ReturnMessage = "Save successful" });
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

            Stock_Warehouses MainTbl = db.Stock_Warehouses.Find(Id);



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

            Stock_Warehouses MainTbl = db.Stock_Warehouses.Find(Id);
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
            if (TxtSearchVal != null)
            {
                var m = db.Stock_Warehouses.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true && (b.Code.Contains(TxtSearchVal) || b.NameE.Contains(TxtSearchVal) || b.NameA.Contains(TxtSearchVal) || b.Notes.Contains(TxtSearchVal))).Take(20);
                IQueryable<Stock_Warehouses> mm = m;
                DataSourceResult result = mm.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.Stock_Warehouses.OrderByDescending(b => b.Id).Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true).Take(20);
                IQueryable<Stock_Warehouses> mm = m;
                DataSourceResult result = mm.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);

            }

            //}
        }


        public ActionResult LoadShelvesGrid([DataSourceRequest]DataSourceRequest request, int? Id)
        {
            PrepareUserDat();

                var m = from DtlRow in db.Stock_Warehouses_Shelves.Where(b => b.WarehouseId == Id)
                       

                        select new
                        {
                            Id = DtlRow.Id,
                            WarehouseId = DtlRow.WarehouseId,
                            ShelveNo = DtlRow.ShelveNo,
                            Notes = DtlRow.Notes,


                        };


                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
           

        }

        public ActionResult LoadRacksGrid([DataSourceRequest]DataSourceRequest request, int? sId)
        {
            PrepareUserDat();

      
                var m = from DtlRow in db.Stock_Warehouses_Racks.Where(b => b.ShelveId == sId)
                        
                        select new
                        {
                            Id = DtlRow.Id,
                            WarehouseId = DtlRow.WarehouseId,
                            ShelveId = DtlRow.ShelveId,
                            RackNo = DtlRow.RackNo,
                            Notes = DtlRow.Notes,


                        };


                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
           
        }

        public ActionResult LoadRackRowsGrid([DataSourceRequest]DataSourceRequest request, int? rId, int? sId , int? wId)
        {
            PrepareUserDat();

                var m = from DtlRow in db.Stock_Warehouses_RackRows.Where(b => b.RackId == rId && b.ShelveId == sId && b.WarehouseId == wId)
                      

                        select new
                        {
                            Id = DtlRow.Id,
                            WarehouseId = DtlRow.WarehouseId,
                            ShelveId = DtlRow.ShelveId,
                            RackId = DtlRow.RackId,
                            RowNo = DtlRow.RowNo,
                            Width = DtlRow.Width,
                            Height = DtlRow.Height,
                            Depth = DtlRow.Depth,
                            Notes = DtlRow.Notes,


                        };


                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
          

        }

        public ActionResult LoadRackCasesGrid([DataSourceRequest]DataSourceRequest request, int? wId, int? sId, int? rId, int? rowId)
        {
            PrepareUserDat();

        
                var m = from DtlRow in db.Stock_Warehouses_RackCases.Where(b => b.WarehouseId == wId && b.ShelveId == sId && b.RackId == rId && b.RowId == rowId)
                       

                        select new
                        {
                            Id = DtlRow.Id,
                            WarehouseId = DtlRow.WarehouseId,
                            ShelveId = DtlRow.ShelveId,
                            RackId = DtlRow.RackId,
                            RowId = DtlRow.RowId,
                            CaseNo = DtlRow.CaseNo,
                            Notes = DtlRow.Notes,

                        };


                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
           

          

        }

        public ActionResult SaveRack(int wId ,int sId, string xmlRacksGrid)
        {
            List<Stock_Warehouses_Racks> RacksTblDetail = JsonConvert.DeserializeObject<List<Stock_Warehouses_Racks>>(xmlRacksGrid);

            if (RacksTblDetail.Count > 0)
            {
                Stock_Warehouses_Racks RDtlList = new Stock_Warehouses_Racks();
              

                int r = 1;
                foreach (var I in RacksTblDetail)
                {
                    RDtlList = new Stock_Warehouses_Racks();
                    if ( I.Id == 0)
                    {
                        
                        RDtlList.Id = r;
                        RDtlList.WarehouseId = wId;
                        RDtlList.ShelveId = sId;
                        RDtlList.RackNo = I.RackNo;
                        RDtlList.Notes = I.Notes;

                        db.Stock_Warehouses_Racks.Add(RDtlList);
                        r = r + 1;
                    }
                    else
                    {
                        RDtlList = db.Stock_Warehouses_Racks.Find(I.Id);
                        db.Entry(RDtlList).State = EntityState.Modified;

                        RDtlList.WarehouseId = wId;
                        RDtlList.ShelveId = sId;
                        RDtlList.RackNo = I.RackNo;
                        RDtlList.Notes = I.Notes;

                    }

                  
                }
            }


            db.SaveChanges();

            return Json(new { Id = wId, MessageType = "success", ReturnMessage = "Save successful" });
        }

        public ActionResult SaveRackRow(int wId, int sId,int rId,string xmlRackRowsGrid)
        {
            List<Stock_Warehouses_RackRows> RackRowsTblDetail = JsonConvert.DeserializeObject<List<Stock_Warehouses_RackRows>>(xmlRackRowsGrid);
             
            //Stock_Warehouses_RackRows add to context
                    if (RackRowsTblDetail.Count > 0)
                    {
                        Stock_Warehouses_RackRows RRDtlList = new Stock_Warehouses_RackRows();
                        

                        int rr = 1;
                        foreach (var I in RackRowsTblDetail)
                        {
                            RRDtlList = new Stock_Warehouses_RackRows();

                            if (I.Id == 0)
                            {

                                RRDtlList.Id = rr;
                                RRDtlList.WarehouseId = wId;

                                RRDtlList.ShelveId = sId;
                                RRDtlList.RackId = rId;
                                RRDtlList.RowNo = I.RowNo;
                                RRDtlList.Height = I.Height;
                                RRDtlList.Depth = I.Depth;
                                RRDtlList.Width = I.Width;
                                RRDtlList.Notes = I.Notes;

                                db.Stock_Warehouses_RackRows.Add(RRDtlList);
                                rr = rr + 1;
                            }
                            else
                            {
                                RRDtlList = db.Stock_Warehouses_RackRows.Find(I.Id);
                                db.Entry(RRDtlList).State = EntityState.Modified;

                                RRDtlList.WarehouseId = wId;

                                RRDtlList.ShelveId = sId;
                                RRDtlList.RackId = rId;
                                RRDtlList.RowNo = I.RowNo;
                                RRDtlList.Height = I.Height;
                                RRDtlList.Depth = I.Depth;
                                RRDtlList.Width = I.Width;
                                RRDtlList.Notes = I.Notes;
                            }
                          
                        }
                    }

            db.SaveChanges();

            return Json(new { Id = wId, MessageType = "success", ReturnMessage = "Save successful" });
        }

        public ActionResult SaveRackCase(int wId, int sId, int rId, int rowId, string xmlRackCasesGrid)
        {

            List<Stock_Warehouses_RackCases> RackCasesTblDetail = JsonConvert.DeserializeObject<List<Stock_Warehouses_RackCases>>(xmlRackCasesGrid);

            //Stock_Warehouses_RackRows add to context
            if (RackCasesTblDetail.Count > 0)
            {
                Stock_Warehouses_RackCases RCDtlList = new Stock_Warehouses_RackCases();
               
                int rc = 1;
                foreach (var I in RackCasesTblDetail)
                {
                    RCDtlList = new Stock_Warehouses_RackCases();

                    if (I.Id == 0)
                    {
                        RCDtlList.Id = rc;
                        RCDtlList.WarehouseId = wId;

                        RCDtlList.ShelveId = sId;
                        RCDtlList.RackId = rId;
                        RCDtlList.RowId = rowId;
                        RCDtlList.CaseNo = I.CaseNo;
                        RCDtlList.Notes = I.Notes;

                        db.Stock_Warehouses_RackCases.Add(RCDtlList);
                        rc = rc + 1;
                    }else
                    {
                        RCDtlList = db.Stock_Warehouses_RackCases.Find(I.Id);
                        db.Entry(RCDtlList).State = EntityState.Modified;

                        RCDtlList.ShelveId = sId;
                        RCDtlList.RackId = rId;
                        RCDtlList.RowId = rowId;
                        RCDtlList.CaseNo = I.CaseNo;
                        RCDtlList.Notes = I.Notes;
                    }

                   
                }


            }

            db.SaveChanges();

            return Json(new { Id = wId, MessageType = "success", ReturnMessage = "Save successful" });
        }
    }
}
