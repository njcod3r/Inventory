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
using System;
using System.IO;
using System.Xml.Serialization;




namespace MVC_ERP.Controllers
{
    public class Stock_OpeningBalanceController : BaseController
    {
        
         [Authorize]
        public ActionResult Index(int? Id, Stock_OpeningBalanceHdr MainTbl,int? CompanyListID)
         {

           PrepareUserDat();
           if (!CheckUserPermission(PermissionType.Open)) return Json("", JsonRequestBehavior.AllowGet);

      


           if (Id != null)
           {

               MainTbl = db.Stock_OpeningBalanceHdr.Find(Id);
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
         public ActionResult Insert(int Id, int CompanyId, int BranchId, int WarehouseId, string Notes, string xmlMasterGrid, string xmlDetailGrid, decimal TotalDebit = 0, decimal TotalCredit = 0)

          {
            PrepareUserDat();
            if (!CheckUserPermission((Id == 0 ? PermissionType.Insert : PermissionType.Modifay))) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });

          
       
            //Insert New
            Stock_OpeningBalanceHdr MainTbl = new Stock_OpeningBalanceHdr();
            if (Id == 0)
            {
              
                    MainTbl.Id = (db.Stock_OpeningBalanceHdr.Max(c => (int?)c.Id) ?? 0) + 1;
                    MainTbl.MemberShipId = MemberShipId;
                    MainTbl.CompanyId = CompanyId;
                    MainTbl.BranchId = BranchId;
                    MainTbl.WarehouseId = WarehouseId;
                    MainTbl.Notes = Notes;

                    MainTbl.Create_Uid = UserId;
                    MainTbl.Create_Date = DateTime.Now;
                }

                else
                    //Update
                {
                   

                    MainTbl = db.Stock_OpeningBalanceHdr.Find(Id);
                    if (MainTbl.Post == true)
                    {
                        return Json(new { MessageType = "info", ReturnMessage = "This movment was posting you can't modify it" });
                    }


                    MainTbl.MemberShipId = MemberShipId;
                    MainTbl.CompanyId = CompanyId;
                    MainTbl.BranchId = BranchId;
                    MainTbl.WarehouseId = WarehouseId;
                    MainTbl.Notes = Notes;
                

                    MainTbl.Write_Uid = UserId;
                    MainTbl.Write_Date = DateTime.Now;

                 

                }



            //Detail
            var DAllDetailsLocation = db.Stock_AddProperties_Location.Where(p => p.SourceId == MainTbl.Id);
            db.Stock_AddProperties_Location.RemoveRange(DAllDetailsLocation);

            var DAllDetails = db.Stock_OpeningBalanceDtl.Where(p => p.MasterId == MainTbl.Id);
            db.Stock_OpeningBalanceDtl.RemoveRange(DAllDetails);

            List<Stock_OpeningBalanceDtl> TblDetail = JsonConvert.DeserializeObject<List<Stock_OpeningBalanceDtl>>(xmlMasterGrid);
            if (TblDetail.Count > 0)
            {
              
                Stock_OpeningBalanceDtl DtlList = new Stock_OpeningBalanceDtl();
                MainTbl.Stock_OpeningBalanceDtl = new List<Stock_OpeningBalanceDtl>();

                DtlList.Stock_AddProperties_Location = new List<Stock_AddProperties_Location>();


                List<Stock_AddProperties_Location> AllLocationLists = JsonConvert.DeserializeObject<List<Stock_AddProperties_Location>>(xmlDetailGrid);

                List<Stock_AddProperties_Location> CurrenLocationLists;

                 Stock_AddProperties_Location LocationList;
                 int i = 1;
               foreach (var I in TblDetail)
               {
 
                    DtlList = new Stock_OpeningBalanceDtl();



                    DtlList.Id = i;
                    DtlList.MasterId = MainTbl.Id;

                    DtlList.ProductId = I.ProductId;
                    DtlList.UnitId = I.UnitId;
                    DtlList.UnitRate = I.UnitRate;
                    DtlList.Quantity = I.Quantity;
                    DtlList.UnitCost = I.UnitCost;
                    DtlList.TotalCost = I.TotalCost;
                    DtlList.BaseQuantity = I.BaseQuantity;
                    DtlList.BaseUnitCost = I.BaseUnitCost;
                    DtlList.BaseTotalCost = I.BaseTotalCost;
                    DtlList.Notes = I.Notes;


                    //Add Location
                    CurrenLocationLists = AllLocationLists.Where(c => c.DTLRowId == I.Id).ToList();
                    DtlList.Stock_AddProperties_Location = new List<Stock_AddProperties_Location>();
                      foreach (var U in CurrenLocationLists)
                    {
                    
                        LocationList = new Stock_AddProperties_Location();


                        LocationList.SourceId = MainTbl.Id;
                        LocationList.ShelveId = U.ShelveId;
                        LocationList.RackId = U.RackId;
                        LocationList.RowId = U.RowId;
                        LocationList.CaseId = U.CaseId;
                        LocationList.Notes = U.Notes;

                        DtlList.Stock_AddProperties_Location.Add(LocationList);
                    }



                    MainTbl.Stock_OpeningBalanceDtl.Add(DtlList);


                    i = i + 1;

            }
         }
            if (Id == 0)
            {
                //New
                db.Stock_OpeningBalanceHdr.Add(MainTbl);
            }
            else
            {
                //Update
                db.Entry(MainTbl).State = EntityState.Modified;
            }


            db.SaveChanges();
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
        public ActionResult Read(int Id,int CompanyId,int BranchId,int WarehouseId ,int ReadType)
         {
             PrepareUserDat();

             Stock_OpeningBalanceHdr MainTbl = new Stock_OpeningBalanceHdr();
          
           if (ReadType == Convert.ToInt16(ReadTypes.Empty) )
            {
                //Leave Table empty
            }
            else if (ReadType == Convert.ToInt16(ReadTypes.Current))
            {
                //Read Current

                if (Id==0 || Id==null)
                {
                    MainTbl = new Stock_OpeningBalanceHdr();
                }
                else
                {
                    MainTbl = db.Stock_OpeningBalanceHdr.Find(Id);
                }
            }
           else if (ReadType == Convert.ToInt16(ReadTypes.Next))
           {
               //Read Next
               Id = (db.Stock_OpeningBalanceHdr.Where(c =>  c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.BranchId == BranchId && c.Id != -1 && c.WarehouseId == WarehouseId && c.Deleted != true && c.Id > Id).Min(c => (int?)c.Id) ?? 0);

               
               if (Id == 0 || Id == null)
               {
                   Id = (db.Stock_OpeningBalanceHdr.Where(c =>  c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.BranchId == BranchId && c.Id != -1 && c.WarehouseId == WarehouseId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);

                   MainTbl = db.Stock_OpeningBalanceHdr.Find(Id);
               }
               else
               {
                   MainTbl = db.Stock_OpeningBalanceHdr.Find(Id);
               }

           }

           else if (ReadType == Convert.ToInt16(ReadTypes.Previous))
           {
               //Read Next
               Id = (db.Stock_OpeningBalanceHdr.Where(c =>    c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.BranchId == BranchId && c.Id != -1 && c.WarehouseId == WarehouseId && c.Deleted != true && c.Id < Id).Max(c => (int?)c.Id) ?? 0);
               if (Id == 0 || Id == null)
               {
                   Id = (db.Stock_OpeningBalanceHdr.Where(c =>   c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.BranchId == BranchId && c.Id != -1 && c.WarehouseId == WarehouseId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
                   MainTbl = db.Stock_OpeningBalanceHdr.Find(Id);
               }
               else
               {
                   MainTbl = db.Stock_OpeningBalanceHdr.Find(Id);
               }

           }
           else if (ReadType == Convert.ToInt16(ReadTypes.Last))
           {
               //Read Last
               Id = (db.Stock_OpeningBalanceHdr.Where(c =>  c.MemberShipId == MemberShipId && c.CompanyId==CompanyId && c.BranchId==BranchId && c.Id != -1 && c.WarehouseId==WarehouseId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);

               if (Id == 0 || Id == null)
               {
                   MainTbl = new Stock_OpeningBalanceHdr();
                  
               }
               else
               {
                   MainTbl = db.Stock_OpeningBalanceHdr.Find(Id);
               }

           }

           else if (ReadType == Convert.ToInt16(ReadTypes.First))
           {
               //Read First
               Id = (db.Stock_OpeningBalanceHdr.Where(c =>   c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.BranchId == BranchId && c.Id != -1 && c.WarehouseId == WarehouseId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
           

               if (Id == 0 || Id == null)
               {
                   MainTbl = new Stock_OpeningBalanceHdr();
                 
               }
               else
               {
                   MainTbl = db.Stock_OpeningBalanceHdr.Find(Id);
               }
           }

           var jsonSerialiser = new JavaScriptSerializer();
           var json = "";
           var jsonS = "";
           if (MainTbl != null) { 
           if (MainTbl.Id != 0)
           {
               //Set Location details in cookies
               List<Stock_AddProperties_Location> L = db.Stock_AddProperties_Location.Where(c => c.Source == Convert.ToInt16(FormTypes.Stock_OpeningBalance) && c.SourceId == MainTbl.Id).ToList();
               List<Stock_AddProperties_Serial> S = db.Stock_AddProperties_Serial.Where(c =>c.Source==Convert.ToInt16(FormTypes.Stock_OpeningBalance) && c.SourceId == MainTbl.Id).ToList();


               json = jsonSerialiser.Serialize(L);
               jsonS = jsonSerialiser.Serialize(S);
           }
           }

           return Json(new { TBL = MainTbl, CookiesLocation = json,CookiesSerial=jsonS}, JsonRequestBehavior.AllowGet);
       }

         public ActionResult RemovePropertiesCookies(int? DTLRowId, string AllLocationTables, string AllSerialTables)
         {
             PrepareUserDat();

             List<Stock_AddProperties_Location> AllLocationsList = JsonConvert.DeserializeObject<List<Stock_AddProperties_Location>>(AllLocationTables);
             AllLocationsList.RemoveAll(c => c.DTLRowId == DTLRowId);
             var jsonSerialiser = new JavaScriptSerializer();
             var json = jsonSerialiser.Serialize(AllLocationsList);

             List<Stock_AddProperties_Serial> AllSerialList = JsonConvert.DeserializeObject<List<Stock_AddProperties_Serial>>(AllSerialTables);
             AllSerialList.RemoveAll(c => c.DTLRowId == DTLRowId);
             var jsonSerialiserSerial = new JavaScriptSerializer();
             var jsonS = jsonSerialiserSerial.Serialize(AllSerialList);


             return Json(new { CookiesLocation = json, CookiesSerial = jsonS }, JsonRequestBehavior.AllowGet);

         }
         public ActionResult UpdatePropertiesCookies(int? DTLRowId, string AllLocationTables, string CurrentLocationTables, string AllSerialTables, string CurrentSerialTables)
         {
             PrepareUserDat();

             List<Stock_AddProperties_Location> AllLocationsList=new List<Stock_AddProperties_Location>() ;
                 if(AllLocationTables!="")
                 {
                     AllLocationsList = JsonConvert.DeserializeObject<List<Stock_AddProperties_Location>>(AllLocationTables);
                 }

             List<Stock_AddProperties_Location> CurrentLocationList = JsonConvert.DeserializeObject<List<Stock_AddProperties_Location>>(CurrentLocationTables);

             AllLocationsList.RemoveAll(c => c.DTLRowId == DTLRowId);

             AllLocationsList.AddRange(CurrentLocationList);

             var jsonSerialiser = new JavaScriptSerializer();
             var json = jsonSerialiser.Serialize(AllLocationsList);


             //Serial
             List<Stock_AddProperties_Serial> AllSerialList = new List<Stock_AddProperties_Serial>();
                 if(AllSerialTables!="")
                 {
                AllSerialList = JsonConvert.DeserializeObject<List<Stock_AddProperties_Serial>>(AllSerialTables);

                 }

             List<Stock_AddProperties_Serial> CurrentSerialList = JsonConvert.DeserializeObject<List<Stock_AddProperties_Serial>>(CurrentSerialTables);

             AllSerialList.RemoveAll(c => c.DTLRowId == DTLRowId);

             AllSerialList.AddRange(CurrentSerialList);

             var jsonSerialiserS = new JavaScriptSerializer();
             var jsonS = jsonSerialiserS.Serialize(AllSerialList);



             return Json(new { CookiesLocation = json, CookiesSerial = jsonS }, JsonRequestBehavior.AllowGet);

         }
        public ActionResult LoadDetailGrid([DataSourceRequest]DataSourceRequest request, int? Id)
        {
            PrepareUserDat();
 
             if (Request.Cookies["Language"].Value == Language.English)
             {
                 var m = from DtlRow in db.Stock_OpeningBalanceDtl.Where(b => b.MasterId == Id)
                         join
                         Product in db.Stock_Products on DtlRow.ProductId equals Product.Id
                         join
                         Unit in db.Stock_Units on DtlRow.UnitId equals Unit.Id


                         select new
                         {
                             Id = DtlRow.Id,
                             MasterId = DtlRow.MasterId,

                             ProductId = DtlRow.ProductId,
                             ProductName = Product.NameE,
                             UnitId = DtlRow.UnitId,
                             UnitName = Unit.NameE,
                             Quantity = DtlRow.Quantity,
                             UnitCost = DtlRow.UnitCost,
                             TotalCost = DtlRow.TotalCost,
                             Notes = DtlRow.Notes
                         };


                 DataSourceResult result = m.ToDataSourceResult(request);
                 return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
             }

             else
             {
                 var m = from DtlRow in db.Stock_OpeningBalanceDtl.Where(b => b.MasterId == Id)
                         join
                         Product in db.Stock_Products on DtlRow.ProductId equals Product.Id
                         join
                         Unit in db.Stock_Units on DtlRow.UnitId equals Unit.Id


                         select new
                         {
                             Id = DtlRow.Id,
                             MasterId = DtlRow.MasterId,

                             ProductId = DtlRow.ProductId,
                             ProductName = Product.NameA,
                             UnitId = DtlRow.UnitId,
                             UnitName = Unit.NameA,
                             Quantity = DtlRow.Quantity,
                             UnitCost = DtlRow.UnitCost,
                             TotalCost = DtlRow.TotalCost,
                             Notes = DtlRow.Notes
                         };


                 DataSourceResult result = m.ToDataSourceResult(request);
                 return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
             }

           

        }
        public ActionResult LoadLocationGrid([DataSourceRequest]DataSourceRequest request, int? MasterId, int? DTLRowId, string AllLocationTables)
        {
            if (DTLRowId == null) return Json("", "text/x-json", JsonRequestBehavior.AllowGet);

            PrepareUserDat();




            List<Stock_AddProperties_Location> LocationLists = JsonConvert.DeserializeObject<List<Stock_AddProperties_Location>>(AllLocationTables);
            if (LocationLists == null) return Json("", "text/x-json", JsonRequestBehavior.AllowGet);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = from DtlRow in LocationLists.Where(b => b.SourceId == MasterId && b.DTLRowId == DTLRowId)
                        join
                        Shelve in db.Stock_Warehouses_Shelves on DtlRow.ShelveId equals Shelve.Id into ShelveOutput
                         from Shelve in ShelveOutput.DefaultIfEmpty()
                         join
                         Rack in db.Stock_Warehouses_Racks on DtlRow.RackId equals Rack.Id into RackeOutput
                         from Rack in RackeOutput.DefaultIfEmpty()
                         join
                         Row in db.Stock_Warehouses_RackRows on DtlRow.RowId equals Row.Id into RowOutput
                         from Row in RowOutput.DefaultIfEmpty()
                         join
                         Case in db.Stock_Warehouses_RackCases on DtlRow.CaseId equals Case.Id into CaseOutput
                         from Case in CaseOutput.DefaultIfEmpty()
                         join
                         Color in db.Sys_Colors on DtlRow.ColorId equals Color.Id into ColorOutput

                         from Color in ColorOutput.DefaultIfEmpty()


                        select new
                        {
                            Id = DtlRow.Id,
                            Source = DtlRow.Source,
                            SourceId = DtlRow.SourceId,
                            DTLRowId = DtlRow.DTLRowId,
                            ShelveId = DtlRow.ShelveId,
                            ShelveName = Shelve.ShelveNo,
                            RackId = DtlRow.RackId,
                            RackName = (Rack == null ? "" : Rack.RackNo),
                            RowId = DtlRow.RowId,
                            RowName = (Row == null ? "" : Row.RowNo),
                            CaseId = DtlRow.CaseId,
                            CaseName = (Case== null ? "" : Case.CaseNo),
                            Quantity = DtlRow.Quantity,
                            ColorId = DtlRow.ColorId,
                            ColorName = (Color == null ? "" : Color.NameE),
                            ValidDate = DtlRow.ValidDate,
                            Notes = DtlRow.Notes
                        };

                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = from DtlRow in LocationLists.Where(b => b.SourceId == MasterId && b.DTLRowId == DTLRowId)
                        join
                        Shelve in db.Stock_Warehouses_Shelves on DtlRow.ShelveId equals Shelve.Id into ShelveOutput
                        from Shelve in ShelveOutput.DefaultIfEmpty()
                        join
                        Rack in db.Stock_Warehouses_Racks on DtlRow.RackId equals Rack.Id into RackeOutput
                        from Rack in RackeOutput.DefaultIfEmpty()
                        join
                        Row in db.Stock_Warehouses_RackRows on DtlRow.RowId equals Row.Id into RowOutput
                        from Row in RowOutput.DefaultIfEmpty()
                        join
                        Case in db.Stock_Warehouses_RackCases on DtlRow.CaseId equals Case.Id into CaseOutput
                        from Case in CaseOutput.DefaultIfEmpty()
                        join
                        Color in db.Sys_Colors on DtlRow.ColorId equals Color.Id into ColorOutput

                        from Color in ColorOutput.DefaultIfEmpty()


                        select new
                        {
                            Id = DtlRow.Id,
                            Source = DtlRow.Source,
                            SourceId = DtlRow.SourceId,
                            DTLRowId = DtlRow.DTLRowId,
                            ShelveId = DtlRow.ShelveId,
                            ShelveName = Shelve.ShelveNo,
                            RackId = DtlRow.RackId,
                            RackName = (Rack == null ? "" : Rack.RackNo),
                            RowId = DtlRow.RowId,
                            RowName = (Row == null ? "" : Row.RowNo),
                            CaseId = DtlRow.CaseId,
                            CaseName = (Case == null ? "" : Case.CaseNo),
                            Quantity = DtlRow.Quantity,
                            ColorId = DtlRow.ColorId,
                            ColorName = (Color == null ? "" : Color.NameA),
                            ValidDate = DtlRow.ValidDate,
                            Notes = DtlRow.Notes
                        };

                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }
          


          




        }
        public ActionResult LoadSerialGrid([DataSourceRequest]DataSourceRequest request, int? MasterId, int? DTLRowId, string AllSerialTables)
        {
            if (DTLRowId == null) return Json("", "text/x-json", JsonRequestBehavior.AllowGet);

            PrepareUserDat();




            List<Stock_AddProperties_Serial> SerialLists = JsonConvert.DeserializeObject<List<Stock_AddProperties_Serial>>(AllSerialTables);

            if (SerialLists == null) return Json("", "text/x-json", JsonRequestBehavior.AllowGet);

            var m = from DtlRow in SerialLists.Where(b => b.SourceId == MasterId && b.DTLRowId == DTLRowId)



                    select new
                    {
                        Id = DtlRow.Id,
                        Source = DtlRow.Source,
                        SourceId = DtlRow.SourceId,
                        DTLRowId = DtlRow.DTLRowId,
                        Serial = DtlRow.Serial,

                    };


            DataSourceResult result = m.ToDataSourceResult(request);
            return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);




        }

        // Reuse Function
        [Authorize]
        public ActionResult Reuse(int Id)
        {
            PrepareUserDat();
            if (!CheckUserPermission(PermissionType.Reuse)) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });

            Stock_OpeningBalanceHdr MainTbl = new Stock_OpeningBalanceHdr();
            if (Id == null || Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = "No data to reuse" });
            }

            
                //Insert New
            List<Stock_OpeningBalanceHdr> listMain = db.Stock_OpeningBalanceHdr.Where(c => c.Id == Id).ToList();
            foreach (var U in listMain)
            {
                MainTbl.Id = (db.Stock_OpeningBalanceHdr.Max(c => (int?)c.Id) ?? 0) + 1;



                MainTbl.MemberShipId = MemberShipId;
                MainTbl.CompanyId = U.CompanyId;
                MainTbl.BranchId = U.BranchId;
                MainTbl.WarehouseId = U.WarehouseId;
                MainTbl.Notes = U.Notes;

              
              

                MainTbl.Create_Uid = UserId;
                MainTbl.Create_Date = DateTime.Now;
                MainTbl.Write_Uid = null;
                MainTbl.Write_Date = null;
                MainTbl.Post = null;
                MainTbl.Post_Uid = null;
                MainTbl.Post_Date = null;



                MainTbl.MemberShipId = MemberShipId;

               
            }

            // Detail
            List<Stock_OpeningBalanceDtl> listDetail = db.Stock_OpeningBalanceDtl.Where(c => c.MasterId == Id).ToList();

            Stock_OpeningBalanceDtl DtlList = new Stock_OpeningBalanceDtl();
            MainTbl.Stock_OpeningBalanceDtl = new List<Stock_OpeningBalanceDtl>();

            List<Stock_AddProperties_Location> CurrenLocationLists;
            Stock_AddProperties_Location LocationList;

            foreach (var I in listDetail)
            {
                DtlList = new Stock_OpeningBalanceDtl();

                DtlList.MasterId = MainTbl.Id;

                DtlList.ProductId = I.ProductId;
                DtlList.UnitId = I.UnitId;
                DtlList.UnitRate = I.UnitRate;
                DtlList.Quantity = I.Quantity;
                DtlList.UnitCost = I.UnitCost;
                DtlList.TotalCost = I.TotalCost;
                DtlList.BaseQuantity = I.BaseQuantity;
                DtlList.BaseUnitCost = I.BaseUnitCost;
                DtlList.BaseTotalCost = I.BaseTotalCost;
                DtlList.Notes = I.Notes;


                //Add Location
                CurrenLocationLists = db.Stock_AddProperties_Location.Where(c => c.DTLRowId == I.Id).ToList();
                DtlList.Stock_AddProperties_Location = new List<Stock_AddProperties_Location>();
                foreach (var U in CurrenLocationLists)
                {

                    LocationList = new Stock_AddProperties_Location();



                    LocationList.SourceId = MainTbl.Id;
                    LocationList.ShelveId = U.ShelveId;
                    LocationList.RackId = U.RackId;
                    LocationList.RowId = U.RowId;
                    LocationList.CaseId = U.CaseId;
                    LocationList.Notes = U.Notes;

                    DtlList.Stock_AddProperties_Location.Add(LocationList);
                }


                MainTbl.Stock_OpeningBalanceDtl.Add(DtlList);
            }

            db.Stock_OpeningBalanceHdr.Add(MainTbl);
            db.SaveChanges();

            return Json(new { Id = MainTbl.Id, MessageType = "success", ReturnMessage = "Save successful" });
        }
         [Authorize]
        public ActionResult Delete(int Id)
        {
            PrepareUserDat();
            if (!CheckUserPermission(PermissionType.Delete)) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });
          
             if (Id == null || Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = "No data to delete" });
            }

            Stock_OpeningBalanceHdr MainTbl = db.Stock_OpeningBalanceHdr.Find(Id);

          

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

            Stock_OpeningBalanceHdr MainTbl = db.Stock_OpeningBalanceHdr.Find(Id);

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



        public ActionResult Search([DataSourceRequest]DataSourceRequest request, string TxtSearchVal, int? SearchType, int? FinancialYearId, int CompanyId, int BranchId)
        {
            PrepareUserDat();


            if (TxtSearchVal == null || TxtSearchVal == "")
            {
                var m = (from V in db.GL_PayVoucher.Where(b => b.FinancialYearId == FinancialYearId && b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.BranchId == BranchId && b.FinancialYearId == FinancialYearId && b.Deleted != true)
                         join A in db.GL_ChartOfAccounts on V.CreditAccountId equals A.Id
                         select new
                         {
                             Id = V.Id,
                             Code = V.Code,
                             PaperCode = V.PaperCode,
                             TransactDate_Gregi = V.TransactDate.Gregi,
                             Total = V.Total,
                             CheckTransferNo = V.CheckTransferNo,
                             RecieptPerson = V.RecieptPerson,
                             Notes = V.Notes,
                             AccountName = A.NameE //(Request.Cookies["Language"].Value == Language.English ? A.NameE: A.NameA)

                         }).OrderByDescending(o => o.Id).Take(100);

                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }

            if (SearchType == 1 && TxtSearchVal != "" && TxtSearchVal != null)
            //Contain
            {


                var m = (from V in db.GL_PayVoucher.Where(b => b.FinancialYearId == FinancialYearId && b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.BranchId == BranchId && b.FinancialYearId == FinancialYearId && b.Deleted != true)
                         join A in db.GL_ChartOfAccounts on V.CreditAccountId equals A.Id
                         select new
                         {
                             Id = V.Id,
                             Code = V.Code,
                             PaperCode = V.PaperCode,
                             TransactDate_Gregi = V.TransactDate.Gregi,
                             Total = V.Total,
                             CheckTransferNo = V.CheckTransferNo,
                             RecieptPerson = V.RecieptPerson,
                             Notes = V.Notes,
                             AccountName = A.NameE //(Request.Cookies["Language"].Value == Language.English ? A.NameE: A.NameA)

                         }).Where(b => b.Code.Contains(TxtSearchVal) || SqlFunctions.StringConvert((double)b.Total).Contains(TxtSearchVal) || b.Notes.Contains(TxtSearchVal) || b.AccountName.Contains(TxtSearchVal) || b.CheckTransferNo.Contains(TxtSearchVal) || b.PaperCode.Contains(TxtSearchVal)
                || (SqlFunctions.StringConvert((double)SqlFunctions.DatePart("dd", b.TransactDate_Gregi)).Trim() + "/" +
                    SqlFunctions.StringConvert((double)SqlFunctions.DatePart("mm", b.TransactDate_Gregi)).Trim() + "/" +
                    SqlFunctions.StringConvert((double)SqlFunctions.DatePart("yyyy", b.TransactDate_Gregi)).Trim()).Contains(TxtSearchVal)).OrderByDescending(o => o.Id).Take(100);





                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);

            }

            if (SearchType == 2 && TxtSearchVal != "" && TxtSearchVal != null)
            {
                //Equal

                var m = (from V in db.GL_PayVoucher.Where(b => b.FinancialYearId == FinancialYearId && b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.BranchId == BranchId && b.FinancialYearId == FinancialYearId && b.Deleted != true)
                         join A in db.GL_ChartOfAccounts on V.CreditAccountId equals A.Id
                         select new
                         {
                             Id = V.Id,
                             Code = V.Code,
                             PaperCode = V.PaperCode,
                             TransactDate_Gregi = V.TransactDate.Gregi,
                             Total = V.Total,
                             CheckTransferNo = V.CheckTransferNo,
                             RecieptPerson = V.RecieptPerson,
                             Notes = V.Notes,
                             AccountName = A.NameE //(Request.Cookies["Language"].Value == Language.English ? A.NameE: A.NameA)

                         }).Where(b => b.Code.Equals(TxtSearchVal) || SqlFunctions.StringConvert((double)b.Total).Equals(TxtSearchVal) || b.Notes.Equals(TxtSearchVal) || b.AccountName.Equals(TxtSearchVal) || b.CheckTransferNo.Equals(TxtSearchVal) || b.PaperCode.Equals(TxtSearchVal)
                     || (SqlFunctions.StringConvert((double)SqlFunctions.DatePart("dd", b.TransactDate_Gregi)).Trim() + "/" +
                         SqlFunctions.StringConvert((double)SqlFunctions.DatePart("mm", b.TransactDate_Gregi)).Trim() + "/" +
                         SqlFunctions.StringConvert((double)SqlFunctions.DatePart("yyyy", b.TransactDate_Gregi)).Trim()).Equals(TxtSearchVal)).OrderByDescending(o => o.Id).Take(100);

                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }
            if (SearchType == 3 && TxtSearchVal != "" && TxtSearchVal != null)
            {
                //Start With

                var m = (from V in db.GL_PayVoucher.Where(b => b.FinancialYearId == FinancialYearId && b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.BranchId == BranchId && b.FinancialYearId == FinancialYearId && b.Deleted != true)
                         join A in db.GL_ChartOfAccounts on V.CreditAccountId equals A.Id
                         select new
                         {
                             Id = V.Id,
                             Code = V.Code,
                             PaperCode = V.PaperCode,
                             TransactDate_Gregi = V.TransactDate.Gregi,
                             Total = V.Total,
                             CheckTransferNo = V.CheckTransferNo,
                             RecieptPerson = V.RecieptPerson,
                             Notes = V.Notes,
                             AccountName = A.NameE //(Request.Cookies["Language"].Value == Language.English ? A.NameE: A.NameA)

                         }).Where(b => b.Code.StartsWith(TxtSearchVal) || SqlFunctions.StringConvert((double)b.Total).StartsWith(TxtSearchVal) || b.Notes.StartsWith(TxtSearchVal) || b.AccountName.StartsWith(TxtSearchVal) || b.CheckTransferNo.StartsWith(TxtSearchVal) || b.PaperCode.StartsWith(TxtSearchVal)
                       || (SqlFunctions.StringConvert((double)SqlFunctions.DatePart("dd", b.TransactDate_Gregi)).Trim() + "/" +
                         SqlFunctions.StringConvert((double)SqlFunctions.DatePart("mm", b.TransactDate_Gregi)).Trim() + "/" +
                         SqlFunctions.StringConvert((double)SqlFunctions.DatePart("yyyy", b.TransactDate_Gregi)).Trim()).StartsWith(TxtSearchVal)).OrderByDescending(o => o.Id).Take(100);

                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }


            return Json("", "text/x-json", JsonRequestBehavior.AllowGet);

        }

         public System.Data.DataTable ReturnDataTableStoredCstm(string spName, params object[] parameters)
         {
         System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
         System.Data.SqlClient.SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand();
             System.Data.SqlClient.SqlConnection Cn = new System.Data.SqlClient.SqlConnection(db.Database.Connection.ConnectionString);
             if (Cn.State == ConnectionState.Closed)
                 Cn.Open();
             da = new System.Data.SqlClient.SqlDataAdapter(spName, Cn);
             da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
             for (int i = 0; i <= parameters.Length - 2; i++)
             {
                 da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(parameters[i].ToString(), parameters[i + 1]));
                 i += 1;
             }
             System.Data.DataTable dt = new System.Data.DataTable();
             try
             {
                 if (((Cn != null)) & (Cn.State == ConnectionState.Open))
                     Cn.Close();
                 da.Fill(dt);
             }
             catch
             {
             }
             finally
             {
                 if (((Cn != null)) & (Cn.State == ConnectionState.Open))
                     Cn.Close();
                 Dispose();
             }
             return dt;
         }

        


    }
}
