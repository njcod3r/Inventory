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
using System.Xml.Serialization;




namespace MVC_ERP.Controllers
{
    public class GL_DailyVoucherController : BaseController
    {
        
         [Authorize]
        public ActionResult Index(int? Id, GL_DailyVoucher MainTbl,int? CompanyListID)
         {

           PrepareUserDat();
           if (!CheckUserPermission(PermissionType.Open)) return Json("", JsonRequestBehavior.AllowGet);

           ViewData["PassedId"] = Id;


           if (Id != null)
           {

               MainTbl = db.GL_DailyVoucher.Find(Id);
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
         public ActionResult Insert(int Id,int FinancialYearId, int CompanyId, int BranchId, int JournalId, string Code, DateTime TransactDate_Gregi, string TransactDate_Hijri, int Source, string SourceNo, string Notes, string xmlGrid, string CostCentersTables, decimal TotalDebit = 0, decimal TotalCredit = 0)

          {
            PrepareUserDat();
            if (!CheckUserPermission((Id == 0 ? PermissionType.Insert : PermissionType.Modifay))) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });

            if (TransactDate_Gregi < FinancialYearStartDate(FinancialYearId) || TransactDate_Gregi > FinancialYearEndDate(FinancialYearId))
            {
                return Json(new { MessageType = "danger", ReturnMessage = "Soory, transaction date out of financial year ranges" });
            }

       
            //Insert New
            GL_DailyVoucher MainTbl = new GL_DailyVoucher();
            if (Id == 0)
            {
              
                    MainTbl.Id = (db.GL_DailyVoucher.Max(c => (int?)c.Id) ?? 0) + 1;
                    MainTbl.MemberShipId = MemberShipId;
                    if (Code == null || Code == "")
                    {
                        //Generate auto code
                        int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.GL_DailyVoucher  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") and  (CompanyId = " + CompanyId + ") and  (BranchId = " + BranchId + ") and (JournalId = " + JournalId + ") and (FinancialYearId = " + FinancialYearId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();
                        MainTbl.Code = Max.ToString();
                    }
                    else
                    {
                        MainTbl.Code = Code;
                    }

                    MainTbl.FinancialYearId = FinancialYearId;
                    MainTbl.CompanyId = CompanyId;
                    MainTbl.BranchId = BranchId;
                    MainTbl.JournalId = JournalId;
                    MainTbl.TransactDate = new UserControl_GHDate_NotNull();
                    MainTbl.TransactDate.Gregi = TransactDate_Gregi;
                    MainTbl.TransactDate.Hijri = TransactDate_Hijri;
                    MainTbl.TotalDebit = TotalDebit;
                    MainTbl.TotalCredit = TotalCredit;
                    MainTbl.Source = 1;
                    MainTbl.SourceId = MainTbl.Id;
                    MainTbl.SourceNo = MainTbl.Code;
                    MainTbl.Notes = Notes;

                    MainTbl.Create_Uid = UserId;
                    MainTbl.Create_Date = DateTime.Now;
                }

                else
                    //Update
                {
                   

                    MainTbl = db.GL_DailyVoucher.Find(Id);
                    if (MainTbl.Post == true)
                    {
                        return Json(new { MessageType = "info", ReturnMessage = "This movment was posting you can't modify it" });
                    }


                    MainTbl.MemberShipId = MemberShipId;

                    if (Code == null || Code == "")
                    {
                        //Generate auto code
                        int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.GL_DailyVoucher  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") and  (CompanyId = " + CompanyId + ") and  (BranchId = " + BranchId + ") and (JournalId = " + JournalId + ") and (FinancialYearId = " + FinancialYearId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();
                        MainTbl.Code = Max.ToString();
                    }
                    else
                    {
                        MainTbl.Code = Code;
                    }

                    MainTbl.FinancialYearId = FinancialYearId ;
                    MainTbl.CompanyId = CompanyId;
                    MainTbl.BranchId = BranchId;
                    MainTbl.JournalId = JournalId;
                    MainTbl.TransactDate = new UserControl_GHDate_NotNull();
                    MainTbl.TransactDate.Gregi = TransactDate_Gregi;
                    MainTbl.TransactDate.Hijri = TransactDate_Hijri;
                    MainTbl.TotalDebit = TotalDebit;
                    MainTbl.TotalCredit = TotalCredit;
                    MainTbl.Source = 1;
                    MainTbl.SourceId = MainTbl.Id;
                    MainTbl.SourceNo = MainTbl.Code;
                    MainTbl.Notes = Notes;
                

                    MainTbl.Write_Uid = UserId;
                    MainTbl.Write_Date = DateTime.Now;

                 

                }



            //Detail
            var DAllDetailsC = db.GL_DailyVoucher_CostCenter.Where(p => p.DailyVoucherId == MainTbl.Id);
            db.GL_DailyVoucher_CostCenter.RemoveRange(DAllDetailsC);

            var DAllDetails = db.GL_DailyVoucher_DTL.Where(p => p.DailyVoucherId == MainTbl.Id);
            db.GL_DailyVoucher_DTL.RemoveRange(DAllDetails);

            List<GL_DailyVoucher_DTL> TblDetail = JsonConvert.DeserializeObject<List<GL_DailyVoucher_DTL>>(xmlGrid);
            if (TblDetail.Count > 0)
            {
              
                GL_DailyVoucher_DTL DtlList = new GL_DailyVoucher_DTL();
                MainTbl.GL_DailyVoucher_DTL = new List<GL_DailyVoucher_DTL>();

                DtlList.GL_DailyVoucher_CostCenter = new List<GL_DailyVoucher_CostCenter>();


                List<GL_DailyVoucher_CostCenter> AllCostCenterLists = JsonConvert.DeserializeObject<List<GL_DailyVoucher_CostCenter>>(CostCentersTables);

                List<GL_DailyVoucher_CostCenter> CurrentCostCenterLists;

                 GL_DailyVoucher_CostCenter CostList;
                 int i = 1;
               foreach (var I in TblDetail)
               {
 
                    DtlList = new GL_DailyVoucher_DTL();



                    DtlList.Id = i;
                    DtlList.DailyVoucherId = MainTbl.Id;

                    DtlList.TransactDate_Gregi = I.TransactDate_Gregi;
                    DtlList.TransactDate_Hijri = I.TransactDate_Hijri;
                    DtlList.AccountId = I.AccountId;
                    DtlList.CurrencyId = I.CurrencyId;
                    DtlList.Rate = I.CurrencyId;
                    DtlList.Debit = I.Debit;
                    DtlList.Credit = I.Credit;
                    DtlList.DebitLocal = I.DebitLocal;
                    DtlList.CreditLocal = I.CreditLocal;
                    DtlList.Notes = I.Notes;


                    //Add cost center
                    CurrentCostCenterLists = AllCostCenterLists.Where(c => c.DailyVoucherRowId == I.Id).ToList();
                    DtlList.GL_DailyVoucher_CostCenter = new List<GL_DailyVoucher_CostCenter>();
                      foreach (var U in CurrentCostCenterLists)
                    {
                    
                        CostList = new GL_DailyVoucher_CostCenter();
                      


                        CostList.CostCenterId = U.CostCenterId;
                        CostList.DailyVoucherId =MainTbl.Id;
                        CostList.TransactDate_Gregi = I.TransactDate_Gregi;
                        CostList.TransactDate_Hijri = I.TransactDate_Hijri;
                        CostList.Debit = U.Debit;
                        CostList.Credit = U.Credit;
                        CostList.DebitLocal = U.Debit * I.Rate;
                        CostList.CreditLocal = U.Credit * I.Rate;
                        CostList.Notes = U.Notes;

                        DtlList.GL_DailyVoucher_CostCenter.Add(CostList);
                    }



                    MainTbl.GL_DailyVoucher_DTL.Add(DtlList);


                    i = i + 1;

            }
         }
            if (Id == 0)
            {
                //New
                db.GL_DailyVoucher.Add(MainTbl);
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
        public ActionResult Read(int Id,int FinancialYearId,int CompanyId,int BranchId,int JournalId ,int ReadType)
         {
             PrepareUserDat();

             GL_DailyVoucher MainTbl = new GL_DailyVoucher();
             MainTbl.TransactDate = new UserControl_GHDate_NotNull();
           if (ReadType == Convert.ToInt16(ReadTypes.Empty) )
            {
                //Leave Table empty
            }
            else if (ReadType == Convert.ToInt16(ReadTypes.Current))
            {
                //Read Current

                if (Id==0 || Id==null)
                {
                    MainTbl = new GL_DailyVoucher();
                }
                else
                {
                    MainTbl = db.GL_DailyVoucher.Find(Id);
                }
            }
           else if (ReadType == Convert.ToInt16(ReadTypes.Next))
           {
               //Read Next
               Id = (db.GL_DailyVoucher.Where(c => c.FinancialYearId == FinancialYearId &&  c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.BranchId == BranchId && c.Id != -1 && c.JournalId == JournalId && c.Deleted != true && c.Id > Id).Min(c => (int?)c.Id) ?? 0);

               
               if (Id == 0 || Id == null)
               {
                   Id = (db.GL_DailyVoucher.Where(c => c.FinancialYearId == FinancialYearId && c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.BranchId == BranchId && c.Id != -1 && c.JournalId == JournalId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);

                   MainTbl = db.GL_DailyVoucher.Find(Id);
               }
               else
               {
                   MainTbl = db.GL_DailyVoucher.Find(Id);
               }

           }

           else if (ReadType == Convert.ToInt16(ReadTypes.Previous))
           {
               //Read Next
               Id = (db.GL_DailyVoucher.Where(c => c.FinancialYearId == FinancialYearId &&  c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.BranchId == BranchId && c.Id != -1 && c.JournalId == JournalId && c.Deleted != true && c.Id < Id).Max(c => (int?)c.Id) ?? 0);
               if (Id == 0 || Id == null)
               {
                   Id = (db.GL_DailyVoucher.Where(c => c.FinancialYearId == FinancialYearId && c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.BranchId == BranchId && c.Id != -1 && c.JournalId == JournalId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
                   MainTbl = db.GL_DailyVoucher.Find(Id);
               }
               else
               {
                   MainTbl = db.GL_DailyVoucher.Find(Id);
               }

           }
           else if (ReadType == Convert.ToInt16(ReadTypes.Last))
           {
               //Read Last
               Id = (db.GL_DailyVoucher.Where(c => c.FinancialYearId==FinancialYearId && c.MemberShipId == MemberShipId && c.CompanyId==CompanyId && c.BranchId==BranchId && c.Id != -1 && c.JournalId==JournalId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);
               
               //string MaxCode = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0) from dbo.GL_DailyVoucher  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") and  (CompanyId = " + CompanyId + ") and  (BranchId = " + BranchId + ") and (JournalId = " + JournalId + ") and (FinancialYearId = " + FinancialYearId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault().ToString();
               //Id = db.GL_DailyVoucher.Where(c => c.MemberShipId == MemberShipId && c.CompanyId==CompanyId && c.BranchId==BranchId && c.Code==MaxCode && c.Id != -1 && c.JournalId==JournalId && c.Deleted != true).Select(s=>s.Id).FirstOrDefault();

               if (Id == 0 || Id == null)
               {
                   MainTbl = new GL_DailyVoucher();
                   MainTbl.TransactDate = new UserControl_GHDate_NotNull();
               }
               else
               {
                   MainTbl = db.GL_DailyVoucher.Find(Id);
               }

           }

           else if (ReadType == Convert.ToInt16(ReadTypes.First))
           {
               //Read First
               Id = (db.GL_DailyVoucher.Where(c => c.FinancialYearId == FinancialYearId && c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.BranchId == BranchId && c.Id != -1 && c.JournalId == JournalId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
              // string  MinCode = db.Database.SqlQuery<int>("select isnull(Min(CAST(Code AS int)) ,0) from dbo.GL_DailyVoucher  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") and  (CompanyId = " + CompanyId + ") and  (BranchId = " + BranchId + ") and (JournalId = " + JournalId + ") and (FinancialYearId = " + FinancialYearId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault().ToString();
               //Id = db.GL_DailyVoucher.Where(c => c.MemberShipId == MemberShipId && c.CompanyId == CompanyId && c.BranchId == BranchId && c.Code == MinCode.ToString() && c.Id != -1 && c.JournalId == JournalId && c.Deleted != true).Select(s => s.Id).FirstOrDefault();


               if (Id == 0 || Id == null)
               {
                   MainTbl = new GL_DailyVoucher();
                   MainTbl.TransactDate = new UserControl_GHDate_NotNull();
               }
               else
               {
                   MainTbl = db.GL_DailyVoucher.Find(Id);
               }
           }

           var jsonSerialiser = new JavaScriptSerializer();
           var json = "";
           if (MainTbl != null) { 
           if (MainTbl.Id != 0)
           {
               //Set Cost center details in cookies
               List<GL_DailyVoucher_CostCenter> m = db.GL_DailyVoucher_CostCenter.Where(c => c.DailyVoucherId == MainTbl.Id).ToList();


               json = jsonSerialiser.Serialize(m);
           }
           }

           return Json(new { TBL = MainTbl, CookiesVal = json }, JsonRequestBehavior.AllowGet);
       }

         public ActionResult RemoveCostCookiesValues(int? DailyVoucherRowId, string CostCentersTables)
         {
             PrepareUserDat();

             List<GL_DailyVoucher_CostCenter> AllCostsList = JsonConvert.DeserializeObject<List<GL_DailyVoucher_CostCenter>>(CostCentersTables);



             AllCostsList.RemoveAll(c => c.DailyVoucherRowId == DailyVoucherRowId);

       

             var jsonSerialiser = new JavaScriptSerializer();
             var json = jsonSerialiser.Serialize(AllCostsList);
            

             return Json(new { CookiesVal = json }, JsonRequestBehavior.AllowGet);

         }
         public ActionResult UpdateCostCookiesValues(int? DailyVoucherRowId, string xmlGrid, string CostCentersTables)
         {
             PrepareUserDat();

             List<GL_DailyVoucher_CostCenter> AllCostsList = JsonConvert.DeserializeObject<List<GL_DailyVoucher_CostCenter>>(CostCentersTables);

             List<GL_DailyVoucher_CostCenter> CurrentCostsList = JsonConvert.DeserializeObject<List<GL_DailyVoucher_CostCenter>>(xmlGrid);

             AllCostsList.RemoveAll(c => c.DailyVoucherRowId == DailyVoucherRowId);

             AllCostsList.AddRange(CurrentCostsList);

             var jsonSerialiser = new JavaScriptSerializer();
             var json = jsonSerialiser.Serialize(AllCostsList);
           

             return Json(new { CookiesVal = json }, JsonRequestBehavior.AllowGet);

         }
        public ActionResult LoadDetailGrid([DataSourceRequest]DataSourceRequest request, int? Id)
        {
            PrepareUserDat();
 
             if (Request.Cookies["Language"].Value == Language.English)
             {
                 var m = from DtlRow in db.GL_DailyVoucher_DTL.Where(b => b.DailyVoucherId == Id)
                         join
                         Account in db.GL_ChartOfAccounts on DtlRow.AccountId equals Account.Id
                         join
                         Currency in db.Sys_Currencies on DtlRow.CurrencyId equals Currency.Id


                         select new
                         {
                             Id = DtlRow.Id,
                             DailyVoucherId = DtlRow.DailyVoucherId,
                             TransactDate_Gregi = DtlRow.TransactDate_Gregi,
                             TransactDate_Hijri = DtlRow.TransactDate_Hijri,
                             AccountId = DtlRow.AccountId,
                             AccountName = Account.NameE,
                             CurrencyId = DtlRow.CurrencyId,
                             CurrencyName = Currency.NameE,
                             Rate = DtlRow.Rate,
                             Debit = DtlRow.Debit,
                             Credit = DtlRow.Credit,
                             DebitLocal = DtlRow.DebitLocal,
                             CreditLocal = DtlRow.CreditLocal,
                             Notes = DtlRow.Notes
                         };


                 DataSourceResult result = m.ToDataSourceResult(request);
                 return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
             }

             else
             {
                 var m = from DtlRow in db.GL_DailyVoucher_DTL.Where(b => b.DailyVoucherId == Id)
                         join
                         Account in db.GL_ChartOfAccounts on DtlRow.AccountId equals Account.Id
                         join
                         Currency in db.Sys_Currencies on DtlRow.CurrencyId equals Currency.Id


                         select new
                         {
                             Id = DtlRow.Id,
                             DailyVoucherId = DtlRow.DailyVoucherId,
                             TransactDate_Gregi = DtlRow.TransactDate_Gregi,
                             TransactDate_Hijri = DtlRow.TransactDate_Hijri,
                             AccountId = DtlRow.AccountId,
                             AccountName = Account.NameA,
                             CurrencyId = DtlRow.CurrencyId,
                             CurrencyName = Currency.NameA,
                             Rate = DtlRow.Rate,
                             Debit = DtlRow.Debit,
                             Credit = DtlRow.Credit,
                             DebitLocal = DtlRow.DebitLocal,
                             CreditLocal = DtlRow.CreditLocal,
                             Notes = DtlRow.Notes
                         };


                 DataSourceResult result = m.ToDataSourceResult(request);
                 return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
             }

           

        }
        public ActionResult LoadCostCenter([DataSourceRequest]DataSourceRequest request,int? DailyVoucherId, int? DailyVoucherRowId,string CostCentersTables)
        {
            if (DailyVoucherRowId == null) return Json("", "text/x-json", JsonRequestBehavior.AllowGet);

            PrepareUserDat();
    
                  //var GK = Request.Cookies["DailyVoucherCostCenterCookies"].Value;


           List<GL_DailyVoucher_CostCenter> CostCenterLists = JsonConvert.DeserializeObject<List<GL_DailyVoucher_CostCenter>>(CostCentersTables);

           if (Request.Cookies["Language"].Value == Language.English)
           {
               var m = from DtlRow in CostCenterLists.Where(b => b.DailyVoucherId == DailyVoucherId && b.DailyVoucherRowId == DailyVoucherRowId)
                       join
                       CostCenter in db.GL_ChartOfCostCenter on DtlRow.CostCenterId equals CostCenter.Id


                       select new
                       {
                           Id = DtlRow.Id,
                           DailyVoucherId = DtlRow.DailyVoucherId,
                           DailyVoucherRowId = DtlRow.DailyVoucherRowId,
                           TransactDate_Gregi = DtlRow.TransactDate_Gregi,
                           TransactDate_Hijri = DtlRow.TransactDate_Hijri,
                           CostCenterId = DtlRow.CostCenterId,
                           CostCenterName = CostCenter.NameE,
                           Debit = DtlRow.Debit,
                           Credit = DtlRow.Credit,
                           DebitLocal = DtlRow.DebitLocal,
                           CreditLocal = DtlRow.CreditLocal,
                           Notes = DtlRow.Notes
                       };


               DataSourceResult result = m.ToDataSourceResult(request);
               return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
           }
           else
           {
               var m = from DtlRow in CostCenterLists.Where(b => b.DailyVoucherId == DailyVoucherId && b.DailyVoucherRowId == DailyVoucherRowId)
                       join
                       CostCenter in db.GL_ChartOfCostCenter on DtlRow.CostCenterId equals CostCenter.Id


                       select new
                       {
                           Id = DtlRow.Id,
                           DailyVoucherId = DtlRow.DailyVoucherId,
                           DailyVoucherRowId = DtlRow.DailyVoucherRowId,
                           TransactDate_Gregi = DtlRow.TransactDate_Gregi,
                           TransactDate_Hijri = DtlRow.TransactDate_Hijri,
                           CostCenterId = DtlRow.CostCenterId,
                           CostCenterName = CostCenter.NameA,
                           Debit = DtlRow.Debit,
                           Credit = DtlRow.Credit,
                           DebitLocal = DtlRow.DebitLocal,
                           CreditLocal = DtlRow.CreditLocal,
                           Notes = DtlRow.Notes
                       };


               DataSourceResult result = m.ToDataSourceResult(request);
               return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
           }
          

         

        }


        // Reuse Function
        [Authorize]
        public ActionResult Reuse(int Id)
        {
            PrepareUserDat();
            if (!CheckUserPermission(PermissionType.Reuse)) return Json(new { MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") });

            GL_DailyVoucher MainTbl = new GL_DailyVoucher();
            if (Id == null || Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = "No data to reuse" });
            }

            
                //Insert New
            List<GL_DailyVoucher> listMain = db.GL_DailyVoucher.Where(c => c.Id == Id).ToList();
            foreach (var U in listMain)
            {
                MainTbl.Id = (db.GL_DailyVoucher.Max(c => (int?)c.Id) ?? 0) + 1;


                int Max = db.Database.SqlQuery<int>("select isnull(MAX(CAST(Code AS int)) ,0)+1 from dbo.GL_DailyVoucher  WHERE (isnumeric(Code) = 1) and  (MemberShipId = " + MemberShipId + ") and  (CompanyId = " + U.CompanyId + ") and  (BranchId = " + U.BranchId + ") and (JournalId = " + U.JournalId + ") and (FinancialYearId = " + U.FinancialYearId + ") AND (ISNULL(Deleted, 0) = 0)").FirstOrDefault();
                MainTbl.Code = Max.ToString();

                MainTbl.FinancialYearId =FinancialYearId??0;
                MainTbl.CompanyId = U.CompanyId;
                MainTbl.BranchId = U.BranchId;
                MainTbl.JournalId = U.JournalId;
                MainTbl.TransactDate = new UserControl_GHDate_NotNull();
                MainTbl.TransactDate.Gregi = DateTime.Now.Date;
                MainTbl.TransactDate.Hijri = U.TransactDate.Hijri;
                MainTbl.TotalDebit = U.TotalDebit;
                MainTbl.TotalCredit = U.TotalCredit;
                MainTbl.Source = 1;
                MainTbl.SourceNo = Max.ToString();
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
            List<GL_DailyVoucher_DTL> listDetail = db.GL_DailyVoucher_DTL.Where(c => c.DailyVoucherId == Id).ToList();

            GL_DailyVoucher_DTL DtlList = new GL_DailyVoucher_DTL();
            MainTbl.GL_DailyVoucher_DTL = new List<GL_DailyVoucher_DTL>();

            List<GL_DailyVoucher_CostCenter> CurrentCostCenterLists;
            GL_DailyVoucher_CostCenter CostList;

            foreach (var Y in listDetail)
            {
                DtlList = new GL_DailyVoucher_DTL();

                DtlList.DailyVoucherId = MainTbl.Id;

                DtlList.TransactDate_Gregi = DateTime.Now.Date; ;
                DtlList.TransactDate_Hijri = Y.TransactDate_Hijri;
                DtlList.AccountId = Y.AccountId;
                DtlList.CurrencyId =Y.CurrencyId;
                DtlList.Rate = Y.Rate;
                DtlList.Debit = Y.Debit;
                DtlList.Credit = Y.Credit;
                DtlList.DebitLocal = Y.DebitLocal;
                DtlList.CreditLocal = Y.CreditLocal;
                DtlList.Notes = Y.Notes;


                //Add cost center
                CurrentCostCenterLists = db.GL_DailyVoucher_CostCenter.Where(c => c.DailyVoucherRowId == Y.Id).ToList();
                DtlList.GL_DailyVoucher_CostCenter = new List<GL_DailyVoucher_CostCenter>();
                foreach (var U in CurrentCostCenterLists)
                {

                    CostList = new GL_DailyVoucher_CostCenter();



                    CostList.CostCenterId = U.CostCenterId;
                    CostList.DailyVoucherId = MainTbl.Id;
                    CostList.TransactDate_Gregi = DateTime.Now.Date; ;
                    CostList.TransactDate_Hijri = Y.TransactDate_Hijri;
                    CostList.Debit = U.Debit;
                    CostList.Credit = U.Credit;
                    CostList.DebitLocal = U.Debit * Y.Rate;
                    CostList.CreditLocal = U.Credit * Y.Rate;
                    CostList.Notes = U.Notes;

                    DtlList.GL_DailyVoucher_CostCenter.Add(CostList);
                }


                MainTbl.GL_DailyVoucher_DTL.Add(DtlList);
            }

            db.GL_DailyVoucher.Add(MainTbl);
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

            GL_DailyVoucher MainTbl = db.GL_DailyVoucher.Find(Id);

          

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

            GL_DailyVoucher MainTbl = db.GL_DailyVoucher.Find(Id);

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



        private class SearchQueryResult
        {
            public int Id { set; get; }
            public string Code { set; get; }
            public DateTime TransactDate_Gregi { set; get; }
            public string JournalName { set; get; }
            public string Notes { set; get; }
            public string SourceName { set; get; }
            public decimal TotalDebit { set; get; }
            public string SourceNo { set; get; }
        }
         [Authorize]
          public ActionResult Search([DataSourceRequest]DataSourceRequest request,int? FinancialYearId,int CompanyId,int BranchId,int? SearchType, string TxtSearchVal="")
           {
            PrepareUserDat();
          
              
               DataTable SourceTBL;
              
               SourceTBL = ListToDataTable(StaticLists.SourceList);


               SqlParameter SourceTableParm = new SqlParameter("@SourceTable", SqlDbType.Structured);
               SourceTableParm.TypeName = "dbo.CodeName_Type";
               SourceTableParm.Value = SourceTBL;

               SqlParameter LangParm = new SqlParameter("@Lang", SqlDbType.NVarChar);
               LangParm.Value = Request.Cookies["Language"].Value;

               SqlParameter CompanyIdParm = new SqlParameter("@CompanyId", SqlDbType.Int);
               CompanyIdParm.Value = CompanyId;

               SqlParameter BranchIdParm = new SqlParameter("@BranchId", SqlDbType.Int);
               BranchIdParm.Value = BranchId;


               SqlParameter FinancialYearIdParm = new SqlParameter("@FinancialYearId", SqlDbType.Int);
               FinancialYearIdParm.Value = FinancialYearId;

               SqlParameter SearchTypeParm = new SqlParameter("@SearchType", SqlDbType.Int);
               SearchTypeParm.Value = SearchType;

               SqlParameter TxtSearchValParm = new SqlParameter("@TxtSearchVal", SqlDbType.NVarChar);
               TxtSearchValParm.Value = TxtSearchVal;


              //Parameter must send in the same order in Stored procedure
               var m = db.Database.SqlQuery<SearchQueryResult>("EXEC [dbo].[GL_DailyVoucher_Search_SP] @SourceTable, @Lang, @CompanyId ,@BranchId, @FinancialYearId,@SearchType,@TxtSearchVal", SourceTableParm, LangParm, CompanyIdParm, BranchIdParm, FinancialYearIdParm,SearchTypeParm,TxtSearchValParm).ToList();

               DataSourceResult result = m.ToDataSourceResult(request);
               return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);

         
        }

         public DataTable ReturnDataTableStoredCstm(string spName, params object[] parameters)
         {
         SqlDataAdapter da = new SqlDataAdapter();
         SqlCommand dbCommand = new SqlCommand();
            SqlConnection Cn = new SqlConnection(db.Database.Connection.ConnectionString);
             if (Cn.State == ConnectionState.Closed)
                 Cn.Open();
             da = new SqlDataAdapter(spName, Cn);
             da.SelectCommand.CommandType = CommandType.StoredProcedure;
             for (int i = 0; i <= parameters.Length - 2; i++)
             {
                 da.SelectCommand.Parameters.Add(new SqlParameter(parameters[i].ToString(), parameters[i + 1]));
                 i += 1;
             }
            DataTable dt = new DataTable();
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
