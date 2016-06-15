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
using System.IO;




namespace MVC_ERP.Controllers
{
    public class Stock_ProductsController : BaseController
    {

        [Authorize]
        public ActionResult Index(int? Id, Stock_Products MainTbl, int? CompanyListID)
        {

            PrepareUserDat();
            if (!CheckUserPermission(PermissionType.Open)) return Json("", JsonRequestBehavior.AllowGet);



            if (Id != null)
            {

                MainTbl = db.Stock_Products.Find(Id);
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
        public ActionResult Insert(int Id, int CompanyId, int GroupId, string Code, string NameE, string NameA, int ItemType, int CostType, bool HasValidDate, bool HasSerial, bool SerialNeedInReciept, bool SerialNeedInDeliver, decimal? MaxLimit, decimal? RequireLimit, decimal? MinLimit, decimal? slumpRate, int? BrandId, int? Warranty, int? CustomerLeadTime, bool Active, string Notes, bool HasColor, int Width, int Height, int Depth, string xmlUntisGrid, string xmlSuppliresGrid, string xmlPricingGrid, string xmlAlternativeGrid, string xmlRecommendSPGrid, string xmlCollectionDtlGrid, string xmlStorageLocationGrid)
        {
            PrepareUserDat();
            if (!CheckUserPermission((Id == 0 ? PermissionType.Insert : PermissionType.Modifay))) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });


            //Insert New
            Stock_Products MainTbl = new Stock_Products();
            if (Id == 0)
            {


                MainTbl.Id = (db.Stock_Products.Max(c => (int?)c.Id) ?? 0) + 1;


                if (Code == null || Code == "")
                {
                    //Generate auto code
                    int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Stock_Products  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();
                    MainTbl.Code = Max.ToString();
                }
                else
                {
                    MainTbl.Code = Code;
                }

                MainTbl.MemberShipId = MemberShipId;
                MainTbl.CompanyId = CompanyId;
                MainTbl.GroupId = GroupId;
                MainTbl.NameA = NameA;
                MainTbl.NameE = NameE;
                MainTbl.ItemType = ItemType;
                MainTbl.CostType = CostType;
                MainTbl.HasValidDate = HasValidDate;
                MainTbl.HasSerial = HasSerial;
                MainTbl.SerialNeedInReciept = SerialNeedInReciept;
                MainTbl.SerialNeedInDeliver = SerialNeedInDeliver;
                MainTbl.MaxLimit = MaxLimit;
                MainTbl.RequireLimit = RequireLimit;
                MainTbl.MinLimit = MinLimit;
                MainTbl.slumpRate = slumpRate;
                MainTbl.BrandId = BrandId;
                MainTbl.HasColor = HasColor;
                MainTbl.Warranty = Warranty;
                MainTbl.CustomerLeadTime = CustomerLeadTime;
                MainTbl.Active = Active;
                MainTbl.Width = Width;
                MainTbl.Height = Height;
                MainTbl.Depth = Depth;
                MainTbl.Notes = Notes;








                MainTbl.Create_Uid = UserId;
                MainTbl.Create_Date = DateTime.Now;

                db.Stock_Products.Add(MainTbl);




            }

            else
            //Update
            {



                MainTbl = db.Stock_Products.Find(Id);
                if (MainTbl.Post == true)
                {
                    return Json(new { MessageType = "info", ReturnMessage = "This movment was posting you can't modify it" });
                }


                db.Entry(MainTbl).State = EntityState.Modified;

                if (Code == null || Code == "")
                {
                    //Generate auto code
                    int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Stock_Products  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();
                    MainTbl.Code = Max.ToString();
                }
                else
                {
                    MainTbl.Code = Code;
                }
                MainTbl.MemberShipId = MemberShipId;
                MainTbl.CompanyId = CompanyId;
                MainTbl.GroupId = GroupId;
                MainTbl.NameA = NameA;
                MainTbl.NameE = NameE;
                MainTbl.ItemType = ItemType;
                MainTbl.CostType = CostType;
                MainTbl.HasValidDate = HasValidDate;
                MainTbl.HasSerial = HasSerial;
                MainTbl.SerialNeedInReciept = SerialNeedInReciept;
                MainTbl.SerialNeedInDeliver = SerialNeedInDeliver;
                MainTbl.MaxLimit = MaxLimit;
                MainTbl.RequireLimit = RequireLimit;
                MainTbl.MinLimit = MinLimit;
                MainTbl.slumpRate = slumpRate;
                MainTbl.BrandId = BrandId;
                MainTbl.HasColor = HasColor;
                MainTbl.Warranty = Warranty;
                MainTbl.CustomerLeadTime = CustomerLeadTime;
                MainTbl.Active = Active;
                MainTbl.Width = Width;
                MainTbl.Height = Height;
                MainTbl.Depth = Depth;
                MainTbl.Notes = Notes;


                MainTbl.Write_Uid = UserId;
                MainTbl.Write_Date = DateTime.Now;

            }

           


            List<Stock_Products_Units> UTblDetail = JsonConvert.DeserializeObject<List<Stock_Products_Units>>(xmlUntisGrid);
            List<Stock_Products_Suppliers> STblDetail = JsonConvert.DeserializeObject<List<Stock_Products_Suppliers>>(xmlSuppliresGrid);
            List<Stock_Products_Pricing> PTblDetail = JsonConvert.DeserializeObject<List<Stock_Products_Pricing>>(xmlPricingGrid);
            List<Stock_Products_Alternative> ATblDetail = JsonConvert.DeserializeObject<List<Stock_Products_Alternative>>(xmlAlternativeGrid);
            List<Stock_Products_RecomendeSP> RSPTblDetail = JsonConvert.DeserializeObject<List<Stock_Products_RecomendeSP>>(xmlRecommendSPGrid);
            List<Stock_Products_SPCollection> CTblDetail = JsonConvert.DeserializeObject<List<Stock_Products_SPCollection>>(xmlCollectionDtlGrid);
            List<Stock_Products_DefaultStorageLocation> SLTblDetail = JsonConvert.DeserializeObject<List<Stock_Products_DefaultStorageLocation>>(xmlStorageLocationGrid);



            var DUnitsAllDetails = db.Stock_Products_Units.Where(p => p.ProductId == MainTbl.Id);
            db.Stock_Products_Units.RemoveRange(DUnitsAllDetails);

            //Stock_Products_Units add to context
            if (UTblDetail.Count > 0)
            {
                Stock_Products_Units DtlList = new Stock_Products_Units();
                MainTbl.Stock_Products_Units = new List<Stock_Products_Units>();

                int i = 1;
                foreach (var I in UTblDetail)
                {

                    DtlList = new Stock_Products_Units();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;

                    DtlList.UnitId = I.UnitId;
                    DtlList.IsBaseUnit = I.IsBaseUnit;
                    DtlList.IsDefault = I.IsDefault;
                    DtlList.Rate = I.Rate;
                    DtlList.BarCode = I.BarCode;



                    MainTbl.Stock_Products_Units.Add(DtlList);
                    i = i + 1;
                }

            }

          

            var DSuppliersAllDetails = db.Stock_Products_Suppliers.Where(p => p.ProductId == MainTbl.Id);
            db.Stock_Products_Suppliers.RemoveRange(DSuppliersAllDetails);

            //Stock_Products_Suppliers add to context
            if (STblDetail.Count > 0)
            {
                Stock_Products_Suppliers DtlList = new Stock_Products_Suppliers();
                MainTbl.Stock_Products_Suppliers = new List<Stock_Products_Suppliers>();
                int i = 1;
                foreach (var I in STblDetail)
                {

                    DtlList = new Stock_Products_Suppliers();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;

                    DtlList.SupplierId = I.SupplierId;
                    DtlList.ProductCode = I.ProductCode;
                    DtlList.Warranty = I.Warranty;
                    DtlList.DeliveryLeadTime = I.DeliveryLeadTime;
                    DtlList.MinimalQuantity = I.MinimalQuantity;



                    MainTbl.Stock_Products_Suppliers.Add(DtlList);
                    i = i + 1;
                }
            }


            var DPricingAllDetails = db.Stock_Products_Pricing.Where(p => p.ProductId == MainTbl.Id);
            db.Stock_Products_Pricing.RemoveRange(DPricingAllDetails);

            //Stock_Products_Pricing add to context
            if (PTblDetail.Count > 0)
            {
                Stock_Products_Pricing DtlList = new Stock_Products_Pricing();
                MainTbl.Stock_Products_Pricing = new List<Stock_Products_Pricing>();

                int i = 1;
                foreach (var I in PTblDetail)
                {

                    DtlList = new Stock_Products_Pricing();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;
                    DtlList.UnitId = I.UnitId;
                    DtlList.PurchasePolicy = I.PurchasePolicy;
                    DtlList.PCurrencyId = I.PCurrencyId;
                    DtlList.PurchaseValue = I.PurchaseValue;
                    DtlList.SalesPolicy = I.SalesPolicy;
                    DtlList.SCurrencyId = I.SCurrencyId;
                    DtlList.SalesValue = I.SalesValue;
                 


                    MainTbl.Stock_Products_Pricing.Add(DtlList);
                    i = i + 1;
                }
            }

            var DAlternativeAllDetails = db.Stock_Products_Alternative.Where(p => p.ProductId == MainTbl.Id);
            db.Stock_Products_Alternative.RemoveRange(DAlternativeAllDetails);

            //Stock_Products_Alternative add to context
            if (ATblDetail.Count > 0)
            {
                Stock_Products_Alternative DtlList = new Stock_Products_Alternative();
                MainTbl.Stock_Products_Alternative = new List<Stock_Products_Alternative>();

                int i = 1;
                foreach (var I in ATblDetail)
                {

                    DtlList = new Stock_Products_Alternative();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;

                    DtlList.AlternateId = I.AlternateId;
                    DtlList.Notes = I.Notes;

                    MainTbl.Stock_Products_Alternative.Add(DtlList);
                    i = i + 1;
                }
            }

            var DRecomendeSPAllDetails = db.Stock_Products_RecomendeSP.Where(p => p.ProductId == MainTbl.Id);
            db.Stock_Products_RecomendeSP.RemoveRange(DRecomendeSPAllDetails);

            //Stock_Products_RecomendeSP add to context
            if (RSPTblDetail.Count > 0)
            {
                Stock_Products_RecomendeSP DtlList = new Stock_Products_RecomendeSP();
                MainTbl.Stock_Products_RecomendeSP = new List<Stock_Products_RecomendeSP>();

                int i = 1;
                foreach (var I in RSPTblDetail)
                {

                    DtlList = new Stock_Products_RecomendeSP();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;

                    DtlList.SPId = I.SPId;
                    DtlList.UnitId = I.UnitId;
                    DtlList.Quantity = I.Quantity;
                    DtlList.Notes = I.Notes;

                    MainTbl.Stock_Products_RecomendeSP.Add(DtlList);
                    i = i + 1;
                }
            }

            var DSPCollectionAllDetails = db.Stock_Products_SPCollection.Where(p => p.ProductId == MainTbl.Id);
            db.Stock_Products_SPCollection.RemoveRange(DSPCollectionAllDetails);

            //Stock_Products_SPCollection add to context
            if (CTblDetail.Count > 0)
            {
                Stock_Products_SPCollection DtlList = new Stock_Products_SPCollection();
                MainTbl.Stock_Products_SPCollection = new List<Stock_Products_SPCollection>();

                int i = 1;
                foreach (var I in CTblDetail)
                {

                    DtlList = new Stock_Products_SPCollection();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;

                    DtlList.SPId = I.SPId;
                    DtlList.UnitId = I.UnitId;
                    DtlList.Quantity = I.Quantity;
                    DtlList.Notes = I.Notes;

                    MainTbl.Stock_Products_SPCollection.Add(DtlList);
                    i = i + 1;
                }
            }


            var DDLAllDetails = db.Stock_Products_DefaultStorageLocation.Where(p => p.ProductId == MainTbl.Id);
            db.Stock_Products_DefaultStorageLocation.RemoveRange(DDLAllDetails);

            //Stock_Products_DefaultStorageLocation add to context
            if (SLTblDetail.Count > 0)
            {
                Stock_Products_DefaultStorageLocation DtlList = new Stock_Products_DefaultStorageLocation();
                MainTbl.Stock_Products_DefaultStorageLocation = new List<Stock_Products_DefaultStorageLocation>();

                int i = 1;
                foreach (var I in SLTblDetail)
                {

                    DtlList = new Stock_Products_DefaultStorageLocation();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;

                    DtlList.WarehouseId = I.WarehouseId;
                    DtlList.ShelveId = I.ShelveId;
                    DtlList.RackId = (I.RackId == 0 ? null : I.RackId);
                    DtlList.RowId = (I.RowId == 0 ? null : I.RowId);
                    DtlList.CaseId = (I.CaseId == 0 ? null : I.CaseId);
                    DtlList.Notes = I.Notes;

                    MainTbl.Stock_Products_DefaultStorageLocation.Add(DtlList);
                    i = i + 1;
                }
            }

            if (Id == 0)
            {
                //New
                db.Stock_Products.Add(MainTbl);
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

            Stock_Products MainTbl = new Stock_Products();

            if (ReadType == Convert.ToInt16(ReadTypes.Empty))
            {
                //Leave Table empty
            }
            else if (ReadType == Convert.ToInt16(ReadTypes.Current))
            {
                //Read Current

                if (Id == 0 || Id == null)
                {
                    MainTbl = new Stock_Products();
                }
                else
                {
                    MainTbl = db.Stock_Products.Find(Id);
                }
            }
            else if (ReadType == Convert.ToInt16(ReadTypes.Next))
            {
                //Read Next
                Id = (db.Stock_Products.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true && c.Id > Id).Min(c => (int?)c.Id) ?? 0);


                if (Id == 0)
                {
                    Id = (db.Stock_Products.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);

                    MainTbl = db.Stock_Products.Find(Id);
                }
                else
                {
                    MainTbl = db.Stock_Products.Find(Id);
                }

            }

            else if (ReadType == Convert.ToInt16(ReadTypes.Previous))
            {
                //Read Previous
                Id = (db.Stock_Products.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true && c.Id < Id).Max(c => (int?)c.Id) ?? 0);
                if (Id == 0)
                {
                    Id = (db.Stock_Products.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
                    MainTbl = db.Stock_Products.Find(Id);
                }
                else
                {
                    MainTbl = db.Stock_Products.Find(Id);
                }

            }
            else if (ReadType == Convert.ToInt16(ReadTypes.Last))
            {
                //Read Last
                Id = (db.Stock_Products.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);
                if (Id == 0 || Id == null)
                {
                    MainTbl = new Stock_Products();
                }
                else
                {
                    MainTbl = db.Stock_Products.Find(Id);
                }

            }

            else if (ReadType == Convert.ToInt16(ReadTypes.First))
            {
                //Read First
                Id = (db.Stock_Products.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
                if (Id == 0 || Id == null)
                {
                    MainTbl = new Stock_Products();
                }
                else
                {
                    MainTbl = db.Stock_Products.Find(Id);
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

            Stock_Products MainTbl = new Stock_Products();
            if (Id == null || Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = "No data to reuse" });
            }


            //Insert New
            List<Stock_Products> listMain = db.Stock_Products.Where(c => c.Id == Id).ToList();

            foreach (var U in listMain)
            {
                MainTbl.Id = (db.Stock_Products.Max(c => (int?)c.Id) ?? 0) + 1;

                int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.Stock_Products  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();
                MainTbl.Code = Max.ToString();
                MainTbl.MemberShipId = MemberShipId;
                MainTbl.NameA = U.NameA + Max;
                MainTbl.NameE = U.NameE + Max;
                MainTbl.CompanyId = U.CompanyId;
                MainTbl.Notes = U.Notes;

                MainTbl.Create_Uid = UserId;
                MainTbl.Create_Date = DateTime.Now;




            }

            // Detail
            List<Stock_Products_Units> UTblDetail = db.Stock_Products_Units.Where(c => c.ProductId == Id).ToList();
            List<Stock_Products_Suppliers> STblDetail = db.Stock_Products_Suppliers.Where(c => c.ProductId == Id).ToList();
            List<Stock_Products_Pricing> PTblDetail = db.Stock_Products_Pricing.Where(c => c.ProductId == Id).ToList();
            List<Stock_Products_Alternative> ATblDetail = db.Stock_Products_Alternative.Where(c => c.ProductId == Id).ToList();
            List<Stock_Products_RecomendeSP> RSPTblDetail = db.Stock_Products_RecomendeSP.Where(c => c.ProductId == Id).ToList();
            List<Stock_Products_SPCollection> CTblDetail = db.Stock_Products_SPCollection.Where(c => c.ProductId == Id).ToList();
            List<Stock_Products_DefaultStorageLocation> SLTblDetail = db.Stock_Products_DefaultStorageLocation.Where(c => c.ProductId == Id).ToList();



            //Stock_Products_Units add to context
            if (UTblDetail.Count > 0)
            {
                Stock_Products_Units DtlList = new Stock_Products_Units();
                MainTbl.Stock_Products_Units = new List<Stock_Products_Units>();

                int i = 1;
                foreach (var I in UTblDetail)
                {

                    DtlList = new Stock_Products_Units();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;

                    DtlList.UnitId = I.UnitId;
                    DtlList.IsBaseUnit = I.IsBaseUnit;
                    DtlList.IsDefault = I.IsDefault;
                    DtlList.Rate = I.Rate;
                    DtlList.BarCode = I.BarCode;



                    MainTbl.Stock_Products_Units.Add(DtlList);
                    i = i + 1;
                }

            }

            //Stock_Products_Suppliers add to context
            if (STblDetail.Count > 0)
            {
                Stock_Products_Suppliers DtlList = new Stock_Products_Suppliers();
                MainTbl.Stock_Products_Suppliers = new List<Stock_Products_Suppliers>();
                int i = 1;
                foreach (var I in STblDetail)
                {

                    DtlList = new Stock_Products_Suppliers();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;

                    DtlList.SupplierId = I.SupplierId;
                    DtlList.ProductCode = I.ProductCode;
                    DtlList.Warranty = I.Warranty;
                    DtlList.DeliveryLeadTime = I.DeliveryLeadTime;
                    DtlList.MinimalQuantity = I.MinimalQuantity;



                    MainTbl.Stock_Products_Suppliers.Add(DtlList);
                    i = i + 1;
                }
            }


            //Stock_Products_Pricing add to context
            if (PTblDetail.Count > 0)
            {
                Stock_Products_Pricing DtlList = new Stock_Products_Pricing();
                MainTbl.Stock_Products_Pricing = new List<Stock_Products_Pricing>();

                int i = 1;
                foreach (var I in PTblDetail)
                {

                    DtlList = new Stock_Products_Pricing();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;

                    DtlList.UnitId = I.UnitId;
                    DtlList.PurchasePolicy = I.PurchasePolicy;
                    DtlList.PCurrencyId = I.PCurrencyId;
                    DtlList.PurchaseValue = I.PurchaseValue;
                 
                    DtlList.SalesPolicy = I.SalesPolicy;
                    DtlList.SCurrencyId = I.SCurrencyId;
                    DtlList.SalesValue = I.SalesValue;
                


                    MainTbl.Stock_Products_Pricing.Add(DtlList);
                    i = i + 1;
                }
            }

            //Stock_Products_Alternative add to context
            if (ATblDetail.Count > 0)
            {
                Stock_Products_Alternative DtlList = new Stock_Products_Alternative();
                MainTbl.Stock_Products_Alternative = new List<Stock_Products_Alternative>();

                int i = 1;
                foreach (var I in ATblDetail)
                {

                    DtlList = new Stock_Products_Alternative();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;

                    DtlList.AlternateId = I.AlternateId;
                    DtlList.Notes = I.Notes;

                    MainTbl.Stock_Products_Alternative.Add(DtlList);
                    i = i + 1;
                }
            }

            //Stock_Products_RecomendeSP add to context
            if (RSPTblDetail.Count > 0)
            {
                Stock_Products_RecomendeSP DtlList = new Stock_Products_RecomendeSP();
                MainTbl.Stock_Products_RecomendeSP = new List<Stock_Products_RecomendeSP>();

                int i = 1;
                foreach (var I in RSPTblDetail)
                {

                    DtlList = new Stock_Products_RecomendeSP();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;

                    DtlList.SPId = I.SPId;
                    DtlList.UnitId = I.UnitId;
                    DtlList.Quantity = I.Quantity;
                    DtlList.Notes = I.Notes;

                    MainTbl.Stock_Products_RecomendeSP.Add(DtlList);
                    i = i + 1;
                }
            }


            //Stock_Products_SPCollection add to context
            if (CTblDetail.Count > 0)
            {
                Stock_Products_SPCollection DtlList = new Stock_Products_SPCollection();
                MainTbl.Stock_Products_SPCollection = new List<Stock_Products_SPCollection>();

                int i = 1;
                foreach (var I in CTblDetail)
                {

                    DtlList = new Stock_Products_SPCollection();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;

                    DtlList.SPId = I.SPId;
                    DtlList.UnitId = I.UnitId;
                    DtlList.Quantity = I.Quantity;
                    DtlList.Notes = I.Notes;

                    MainTbl.Stock_Products_SPCollection.Add(DtlList);
                    i = i + 1;
                }
            }


            //Stock_Products_DefaultStorageLocation add to context
            if (SLTblDetail.Count > 0)
            {
                Stock_Products_DefaultStorageLocation DtlList = new Stock_Products_DefaultStorageLocation();
                MainTbl.Stock_Products_DefaultStorageLocation = new List<Stock_Products_DefaultStorageLocation>();

                int i = 1;
                foreach (var I in SLTblDetail)
                {

                    DtlList = new Stock_Products_DefaultStorageLocation();

                    DtlList.Id = i;
                    DtlList.ProductId = MainTbl.Id;

                    DtlList.WarehouseId = I.WarehouseId;
                    DtlList.ShelveId = I.ShelveId;
                    DtlList.RackId = I.RackId;
                    DtlList.RowId = I.RowId;
                    DtlList.CaseId = I.CaseId;
                    DtlList.Notes = I.Notes;

                    MainTbl.Stock_Products_DefaultStorageLocation.Add(DtlList);
                    i = i + 1;
                }
            }


            db.Stock_Products.Add(MainTbl);
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

            Stock_Products MainTbl = db.Stock_Products.Find(Id);



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

            Stock_Products MainTbl = db.Stock_Products.Find(Id);
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

        public ActionResult LoadUnitsGrid([DataSourceRequest]DataSourceRequest request, int? Id)
        {
            PrepareUserDat();

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = from DtlRow in db.Stock_Products_Units.Where(b => b.ProductId == Id)
                        join
                        Unit in db.Stock_Units on DtlRow.UnitId equals Unit.Id

                        select new
                        {
                            Id = DtlRow.Id,
                            UnitId = DtlRow.UnitId,
                            UnitName = Unit.NameE,
                            IsBaseUnit = DtlRow.IsBaseUnit,
                            Rate = DtlRow.Rate,
                            BarCode = DtlRow.BarCode,
                            ProductId = DtlRow.ProductId

                        };


                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }

            else
            {
                var m = from DtlRow in db.Stock_Products_Units.Where(b => b.ProductId == Id)
                        join
                        Unit in db.Stock_Units on DtlRow.UnitId equals Unit.Id

                        select new
                        {
                            Id = DtlRow.Id,
                             ProductId = DtlRow.ProductId,
                            UnitId = DtlRow.UnitId,
                            UnitName = Unit.NameA,
                            IsBaseUnit = DtlRow.IsBaseUnit,
                            Rate = DtlRow.Rate,
                            BarCode = DtlRow.BarCode,
                           

                        };


                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }



        }

        public ActionResult LoadSuppliersGrid([DataSourceRequest]DataSourceRequest request, int? Id)
        {
            PrepareUserDat();

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = from DtlRow in db.Stock_Products_Suppliers.Where(b => b.ProductId == Id)
                        join
                        Supplier in db.Sys_Partners on DtlRow.SupplierId equals Supplier.Id

                        select new
                        {
                            Id = DtlRow.Id,
                            ProductId = DtlRow.ProductId,
                            SupplierId = DtlRow.SupplierId,
                            SupplierName = Supplier.NameE,
                            ProductCode = DtlRow.ProductCode,
                            MinimalQuantity = DtlRow.MinimalQuantity,
                       

                        };


                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }

            else
            {
                var m = from DtlRow in db.Stock_Products_Suppliers.Where(b => b.ProductId == Id)
                        join
                        Supplier in db.Sys_Partners on DtlRow.SupplierId equals Supplier.Id

                        select new
                        {
                            Id = DtlRow.Id,
                            ProductId = DtlRow.ProductId,
                            SupplierId = DtlRow.SupplierId,
                            SupplierName = Supplier.NameA,
                            ProductCode = DtlRow.ProductCode,
                            MinimalQuantity = DtlRow.MinimalQuantity,

                        };


                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }



        }

        public ActionResult LoadPricingGrid([DataSourceRequest]DataSourceRequest request, int? Id)
        {
            PrepareUserDat();

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = from DtlRow in db.Stock_Products_Pricing.Where(b => b.ProductId == Id)
                        join
                        //Pursh in StaticLists.PurchasePricingPolicyList.ToList() on DtlRow.PurchasePolicy equals Pursh.Id
                        //join
                        //Sale in StaticLists.SalesPricingPolicyList.ToList() on DtlRow.SalesPolicy equals Sale.Id
                        //join
                        Unit in db.Stock_Units on DtlRow.UnitId equals Unit.Id
                        join
                        PCurrency in db.Sys_Currencies on DtlRow.PCurrencyId equals PCurrency.Id
                        join
                        SCurrency in db.Sys_Currencies on DtlRow.SCurrencyId equals SCurrency.Id 
                        select new
                        {
                            Id = DtlRow.Id,
                            ProductId = DtlRow.ProductId,
                            UnitId = Unit.Id,
                            UnitName = Unit.NameE,
                            PurchasePolicy = DtlRow.PurchasePolicy,
                            //PurchasePolicyName = Pursh.NameE,
                            PCurrencyId = DtlRow.PCurrencyId,
                            PCurrencyName = PCurrency.NameE,
                            PurchaseValue= DtlRow.PurchaseValue,
                            SalesPolicy = DtlRow.SalesPolicy,
                            //SalesPolicyName = Sale.NameE,
                            SCurrencyId = DtlRow.SCurrencyId,
                            SCurrencyName = SCurrency.NameE,
                            SalesValue = DtlRow.SalesValue,
                          

                        };


                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }

            else
            {
                var m = from DtlRow in db.Stock_Products_Pricing.Where(b => b.ProductId == Id)
                        //join
                        //Pursh in StaticLists.PurchasePricingPolicyList on DtlRow.PurchasePolicy equals Pursh.Id
                        //join
                        //Sale in StaticLists.SalesPricingPolicyList on DtlRow.SalesPolicy equals Sale.Id
                        join
                        Unit in db.Stock_Units on DtlRow.UnitId equals Unit.Id
                        join
                        PCurrency in db.Sys_Currencies on DtlRow.PCurrencyId equals PCurrency.Id
                        join
                        SCurrency in db.Sys_Currencies on DtlRow.SCurrencyId equals SCurrency.Id
                        select new
                        {
                            Id = DtlRow.Id,
                            ProductId = DtlRow.ProductId,
                            UnitId = Unit.Id,
                            UnitName = Unit.NameA,
                            PurchasePolicy = DtlRow.PurchasePolicy,
                            //PurchasePolicyName = Pursh.NameA,
                            PCurrencyId = DtlRow.PCurrencyId,
                            PCurrencyName = PCurrency.NameA,
                            PurchaseValue = DtlRow.PurchaseValue,
                            SalesPolicy = DtlRow.SalesPolicy,
                            //SalesPolicyName = Sale.NameA,
                            SCurrencyId = DtlRow.SCurrencyId,
                            SCurrencyName = SCurrency.NameA,
                            SalesValue = DtlRow.SalesValue,


                        };


                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }



        }

        public ActionResult LoadAlternativeGrid([DataSourceRequest]DataSourceRequest request, int? Id)
         {
             PrepareUserDat();

             if (Request.Cookies["Language"].Value == Language.English)
             {
                 var m = from DtlRow in db.Stock_Products_Alternative.Where(b => b.ProductId == Id)
                         join
                         Product in db.Stock_Products on DtlRow.AlternateId equals Product.Id
                         select new
                         {
                             Id = DtlRow.Id,
                             ProductId = DtlRow.ProductId,
                             AlternateId = DtlRow.AlternateId,
                             AlternateName = Product.NameE,
                             Notes = DtlRow.Notes,
                        

                         };


                 DataSourceResult result = m.ToDataSourceResult(request);
                 return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
             }

             else
             {
                 var m = from DtlRow in db.Stock_Products_Alternative.Where(b => b.ProductId == Id)
                         join
                         Product in db.Stock_Products on DtlRow.AlternateId equals Product.Id
                         select new
                         {
                             Id = DtlRow.Id,
                             ProductId = DtlRow.ProductId,
                             AlternateId = DtlRow.AlternateId,
                             AlternateName = Product.NameA,
                             Notes = DtlRow.Notes,


                         };


                 DataSourceResult result = m.ToDataSourceResult(request);
                 return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
             }



         }

        public ActionResult LoadRecomendeSPGrid([DataSourceRequest]DataSourceRequest request, int? Id)
        {
            PrepareUserDat();

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = from DtlRow in db.Stock_Products_RecomendeSP.Where(b => b.ProductId == Id)
                        join
                        Product in db.Stock_Products on DtlRow.SPId equals Product.Id
                        join
                        Units in db.Stock_Units on DtlRow.UnitId equals Units.Id

                        select new
                        {
                            Id = DtlRow.Id,
                            ProductId = DtlRow.ProductId,
                            SPId = DtlRow.SPId,
                            SPName = Product.NameE,
                            UnitId = DtlRow.UnitId,
                            UnitName = Units.NameE,
                            Quantity = DtlRow.Quantity,
                            Notes = DtlRow.Notes,

                        };



                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }

            else
            {
                var m = from DtlRow in db.Stock_Products_RecomendeSP.Where(b => b.ProductId == Id)
                        join
                        Product in db.Stock_Products on DtlRow.SPId equals Product.Id
                        join
                        Units in db.Stock_Units on DtlRow.UnitId equals Units.Id

                        select new
                        {
                            Id = DtlRow.Id,
                            ProductId = DtlRow.ProductId,
                            SPId = DtlRow.SPId,
                            SPName = Product.NameA,
                            UnitId = DtlRow.UnitId,
                            UnitName = Units.NameA,
                            Quantity = DtlRow.Quantity,
                            Notes = DtlRow.Notes,

                        };


                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }



        }

        public ActionResult LoadSPCollectionGrid([DataSourceRequest]DataSourceRequest request, int? Id)
        {
            PrepareUserDat();

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = from DtlRow in db.Stock_Products_SPCollection.Where(b => b.ProductId == Id)
                        join
                        Product in db.Stock_Products on DtlRow.SPId equals Product.Id
                        join
                        Units in db.Stock_Units on DtlRow.UnitId equals Units.Id

                        select new
                        {
                            Id = DtlRow.Id,
                            ProductId = DtlRow.ProductId,
                            UnitId = DtlRow.UnitId,
                            UnitName = Units.NameE,
                            SPId = DtlRow.SPId,
                            SPName = Product.NameE,
                            Quantity = DtlRow.Quantity,
                            Notes = DtlRow.Notes,


                        };


                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }

            else
            {
                var m = from DtlRow in db.Stock_Products_SPCollection.Where(b => b.ProductId == Id)
                        join
                        Product in db.Stock_Products on DtlRow.SPId equals Product.Id
                        join
                        Units in db.Stock_Units on DtlRow.UnitId equals Units.Id

                        select new
                        {
                            Id = DtlRow.Id,
                            ProductId = DtlRow.ProductId,
                            UnitId = DtlRow.UnitId,
                            UnitName = Units.NameA,
                            SPId = DtlRow.SPId,
                            SPName = Product.NameA,
                            Quantity = DtlRow.Quantity,
                            Notes = DtlRow.Notes,


                        };

                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }



        }

        public ActionResult LoadDefaultStorageLocationGrid([DataSourceRequest]DataSourceRequest request, int? Id)
        {
            PrepareUserDat();

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = from DtlRow in db.Stock_Products_DefaultStorageLocation.Where(b => b.ProductId == Id)
                        join
                        Warehouse in db.Stock_Warehouses on DtlRow.WarehouseId equals Warehouse.Id into WarehouseOutput
                        from Warehouse in WarehouseOutput.DefaultIfEmpty()
                        join
                        Shelve in db.Stock_Warehouses_Shelves on DtlRow.ShelveId equals Shelve.Id into ShelveeOutput
                        from Shelve in ShelveeOutput.DefaultIfEmpty()
                        join
                        Rack in db.Stock_Warehouses_Racks on DtlRow.RackId equals Rack.Id into RackOutput
                        from Rack in RackOutput.DefaultIfEmpty()
                        join
                        Row in db.Stock_Warehouses_RackRows on DtlRow.RackId equals Row.Id into RowOutput
                        from Row in RowOutput.DefaultIfEmpty()
                        join
                        Case in db.Stock_Warehouses_RackCases on DtlRow.CaseId equals Case.Id into CaseOutput
                        from Case in CaseOutput.DefaultIfEmpty()

                         select new
                        {
                            Id = DtlRow.Id,
                            ProductId = DtlRow.ProductId,
                            WarehouseId = DtlRow.WarehouseId,
                            WarehouseName = Warehouse.NameE,
                            ShelveId = DtlRow.ShelveId,
                            ShelveName = Shelve.ShelveNo,
                            RackId = DtlRow.RackId,
                            RackName = (Rack == null ? "" : Rack.RackNo),
                            RowId = DtlRow.RowId,
                            RowName = (Row == null ? "" : Row.RowNo),
                            CaseId = DtlRow.CaseId,
                            CaseName = (Case == null ? "" : Case.CaseNo),
                            Notes = DtlRow.Notes,

                        };


                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }

            else
            {
                var m = from DtlRow in db.Stock_Products_DefaultStorageLocation.Where(b => b.ProductId == Id)
                        join
                        Warehouse in db.Stock_Warehouses on DtlRow.WarehouseId equals Warehouse.Id into WarehouseOutput
                        from Warehouse in WarehouseOutput
                        join
                        Shelve in db.Stock_Warehouses_Shelves on DtlRow.ShelveId equals Shelve.Id into ShelveeOutput
                        from Shelve in ShelveeOutput
                        join
                        Rack in db.Stock_Warehouses_Racks on DtlRow.RackId equals Rack.Id into RackOutput
                        from Rack in RackOutput
                        join
                        Row in db.Stock_Warehouses_RackRows on DtlRow.RackId equals Row.Id into RowOutput
                        from Row in RowOutput
                        join
                        Case in db.Stock_Warehouses_RackCases on DtlRow.CaseId equals Case.Id into CaseOutput
                        from Case in CaseOutput

                        select new
                        {
                            Id = DtlRow.Id,
                            ProductId = DtlRow.ProductId,
                            WarehouseId = DtlRow.WarehouseId,
                            WarehouseName = Warehouse.NameA,
                            ShelveId = DtlRow.ShelveId,
                            ShelveName = Shelve.ShelveNo,
                            RackId = DtlRow.RackId,
                            RackName = Rack.RackNo,
                            RowId = DtlRow.RowId,
                            RowName = Row.RowNo,
                            CaseId = DtlRow.CaseId,
                            CaseName = Case.CaseNo,
                            Notes = DtlRow.Notes,

                        };



                DataSourceResult result = m.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
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
                var m = db.Stock_Products.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true && (b.Code.Contains(TxtSearchVal) || b.NameE.Contains(TxtSearchVal) || b.NameA.Contains(TxtSearchVal) || b.Notes.Contains(TxtSearchVal))).Take(20);
                IQueryable<Stock_Products> mm = m;
                DataSourceResult result = mm.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.Stock_Products.OrderByDescending(b => b.Id).Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true).Take(20);
                IQueryable<Stock_Products> mm = m;
                DataSourceResult result = mm.ToDataSourceResult(request);
                return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);

            }

            //}
        }

        [HttpPost]
        public ActionResult SaveImage(IEnumerable<HttpPostedFileBase> files, int? Id)
        {

            // The Name of the Upload component is "files"
            Stock_Products MainTbl = db.Stock_Products.Find(Id);
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

                    MainTbl.Image = imageSize;


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
