using Kendo.Mvc.UI;
using MVC_ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using System.Web.Script.Serialization;

namespace MVC_ERP.Controllers
{
    public class GenerlaClassesController : Controller
    {
        public int LanguageID;
        //
        // GET: /GenerlaClasses/
        private MSDBContext db = new MSDBContext();
        public ActionResult Index()
        {
            return View();
        }

   
        public ActionResult SetVariable(string key, string value)
        {
            Session[key] = value;

            return this.Json(new { success = true });


        }
        public ActionResult FillCompanies()
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);
          

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.Companies.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true);
                TempData["Companies"] = m;
                return Json(m.Select(p => new { Id = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);
              
            }

            else
            {
                var m = db.Companies.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true);
                return Json(m.Select(p => new { Id = p.Id, Name = p.NameA }), JsonRequestBehavior.AllowGet);
            }
          
         
        }
        public ActionResult FillBranches(int? CompanyId=0)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.Branches.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId  && b.Deleted != true);

                return Json(m.Select(p => new { Id = p.Id,p.Code, Name = p.NameE }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.Branches.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true);

                return Json(m.Select(p => new { Id = p.Id, p.Code, Name = p.NameA}), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillCountries()
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);
            var m = db.Sys_Countries.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true);

            return Json(m.Select(p => new { Id = p.Id,Code=p.Code, Name = p.NameE }), JsonRequestBehavior.AllowGet);
            //return Json(  m.AsQueryable(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillCities(int? CountryId, string text = "", int? Id = 0)
        {



            if (text == "0") text = "";
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.Sys_Cities.Where(b => b.Id == Id).Union(db.Sys_Cities.Where(b => b.MemberShipId == MemberShipId && b.CountryId == CountryId  && b.Deleted != true && (b.NameE.Contains(text) || b.Code.Contains(text))).Take(30).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE}), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.Sys_Cities.Where(b => b.Id == Id).Union(db.Sys_Cities.Where(b => b.MemberShipId == MemberShipId && b.CountryId == CountryId && b.Deleted != true && (b.NameA.Contains(text) || b.Code.Contains(text))).Take(30).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA }), JsonRequestBehavior.AllowGet);
            }
        }

      

       
        public ActionResult FillCurrencies(string text = "", int? Id = 0)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (text == "0" || text == "All" || text == Id.ToString()) text = "";

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.Sys_Currencies.Where(b => b.Id == Id).Union(db.Sys_Currencies.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true && (b.NameE.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE }), JsonRequestBehavior.AllowGet);

            }
            else
            {
                var m = db.Sys_Currencies.Where(b => b.Id == Id).Union(db.Sys_Currencies.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true && (b.NameA.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA }), JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult GetLocalCurrency()
        {

            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
               int Id = db.Sys_Currencies.Where(b => b.MemberShipId == MemberShipId && b.BaseCurrency == true && b.Deleted != true).Select(c=>c.Id).FirstOrDefault();
               Sys_Currencies MainTable = db.Sys_Currencies.Find(Id);
               return Json(new { Id = MainTable.Id, Code = MainTable.Code, Name = MainTable.NameE }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int Id = db.Sys_Currencies.Where(b => b.MemberShipId == MemberShipId && b.BaseCurrency == true && b.Deleted != true).Select(c => c.Id).FirstOrDefault();
                Sys_Currencies MainTable = db.Sys_Currencies.Find(Id);
                return Json(new { Id = MainTable.Id, Code = MainTable.Code, Name = MainTable.NameA }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCurrencyRate(int? CurrencyId)
        {

            if (Request.Cookies["Language"].Value == Language.English)
            {
                Sys_Currencies MainTable = db.Sys_Currencies.Find(CurrencyId);
                return Json(new { Rate = MainTable.Rate }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Sys_Currencies MainTable = db.Sys_Currencies.Find(CurrencyId);
                return Json(new { Rate = MainTable.Rate }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetProductDefaultUnit(int? ProductId)
        {
            int UnitId = db.Stock_Products_Units.Where(c => c.ProductId == ProductId && c.IsDefault == true).Select(s=>s.UnitId).FirstOrDefault();
            if (UnitId==0)
            {
                return Json(new { Id = 0, Name = ""}, JsonRequestBehavior.AllowGet);
            }
            if (Request.Cookies["Language"].Value == Language.English)
            {
                Stock_Units MainTable = db.Stock_Units.Find(UnitId);
                return Json(new { Id = MainTable.Id,Name=MainTable.NameE }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Stock_Units MainTable = db.Stock_Units.Find(UnitId);
                return Json(new { Id = MainTable.Id, Name = MainTable.NameA }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillCustomerGroups(int CompanyId)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);
            var m = db.Sys_PartnerGroups.Where(b => b.MemberShipId == MemberShipId && b.CompanyId==CompanyId && b.GroupType==1 && b.Deleted != true);

            return Json(m.Select(p => new { Id = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);
            //return Json(  m.AsQueryable(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillSupplierGroups(int CompanyId)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);
            var m = db.Sys_PartnerGroups.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.GroupType == 2 && b.Deleted != true);

            return Json(m.Select(p => new { Id = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);
            //return Json(  m.AsQueryable(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillActivities()
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);
            var m = db.Sys_Activities.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true);

            return Json(m.Select(p => new { Id = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);
            //return Json(  m.AsQueryable(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillCustomers(int CompanyId, int PartnerId)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (PartnerId == null || PartnerId == 0)
            {
                var m = db.Sys_Partners.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.PartnerType == 1 && b.Deleted != true && b.Stoped != true);
                return Json(m.Select(p => new { Id = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);
                //return Json(  m.AsQueryable(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                //Exclude sent partner id from shown in list
                var m = db.Sys_Partners.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.PartnerType == 1 && b.Id!=PartnerId && b.Deleted != true && b.Stoped != true);
                return Json(m.Select(p => new { Id = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);
                //return Json(  m.AsQueryable(), JsonRequestBehavior.AllowGet);
            }
        }
      
        public ActionResult FillUsers()
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);
            var m = db.Priv_Users.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true);

            return Json(m.Select(p => new { UserId = p.id, Name = p.NameE }), JsonRequestBehavior.AllowGet);
            //return Json(  m.AsQueryable(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillPrivGroups()
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);
            var m = db.Priv_Groups.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true);

            return Json(m.Select(p => new { GroupId = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);
            //return Json(  m.AsQueryable(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillFinancialYear()
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);
            var m = db.GL_FinancialYears.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true);

            return Json(m.Select(p => new { Id = p.Id, Name = p.FinancialYear }), JsonRequestBehavior.AllowGet);
            //return Json(  m.AsQueryable(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillUserFinancialYears(int?UserId)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                    var FYIDS=db.Priv_Users_FinancialYears.Where(u=>u.UserId==UserId && u.Show==true).Select(s=>s.FinancialYearId);
                    return Json(db.GL_FinancialYears.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true && FYIDS.Contains(c.Id)).Select(p => new { Id = p.Id, Name = p.FinancialYear }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                    var FYIDS = db.Priv_Users_FinancialYears.Where(u => u.UserId == UserId && u.Show == true).Select(s => s.FinancialYearId);
                    return Json(db.GL_FinancialYears.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true && FYIDS.Contains(c.Id)).Select(p => new { Id = p.Id, Name = p.FinancialYear }), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillFinancialYearPeriods(int? FinancialYearId)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);


            var m = db.GL_FinancialYears_Periods.Where(b => b.MemberShipId == MemberShipId && b.FinancialYearId == FinancialYearId);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE }), JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA }), JsonRequestBehavior.AllowGet);

            }
            //return Json(  m.AsQueryable(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillUserCompanies(int? UserId)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var CompIDS = db.Priv_Users_CompaniesBranches.Where(u => u.UserId == UserId && u.Show == true).Select(s => s.CompanyId);
                return Json(db.Companies.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true && CompIDS.Contains(c.Id)).Select(p => new { Id = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var CompIDS = db.Priv_Users_CompaniesBranches.Where(u => u.UserId == UserId && u.Show == true).Select(s => s.CompanyId);
                return Json(db.Companies.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true && CompIDS.Contains(c.Id)).Select(p => new { Id = p.Id, Name = p.NameA }), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillUserBranches(int? UserId,int? CompanyId)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var BranchIDS = db.Priv_Users_CompaniesBranches.Where(u => u.UserId == UserId && u.CompanyId == CompanyId && u.Show == true).Select(s => s.BranchId);
                return Json(db.Branches.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true && BranchIDS.Contains(c.Id)).Select(p => new { Id = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var BranchIDS = db.Priv_Users_CompaniesBranches.Where(u => u.UserId == UserId && u.CompanyId == CompanyId && u.Show == true).Select(s => s.BranchId);
                return Json(db.Branches.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true && BranchIDS.Contains(c.Id)).Select(p => new { Id = p.Id, Name = p.NameA }), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillCostCenters(int? CompanyId, string text = "", int? Id = 0)
        {
            if (text == "0" || text == "All" || text == Id.ToString()) text = "";

            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.GL_ChartOfCostCenter.Where(b => b.Id == Id).Union(db.GL_ChartOfCostCenter.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == false && b.Deleted != true && (b.NameE.Contains(text) || b.Code.Contains(text))).Take(8).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.GL_ChartOfCostCenter.Where(b => b.Id == Id).Union(db.GL_ChartOfCostCenter.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == false && b.Deleted != true && (b.NameA.Contains(text) || b.Code.Contains(text))).Take(8).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult FillAllCostCenters([DataSourceRequest] DataSourceRequest request,int? CompanyId,string filters)
        {
            

            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                if (filters == null || filters == "")
                {
                    var m = db.GL_ChartOfCostCenter.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == false && b.Deleted != true).OrderBy(o => o.Code);
                    var result = m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId });
                    var z = result.ToDataSourceResult(request);
                    return Json(z, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    var m = db.GL_ChartOfCostCenter.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == false && b.Deleted != true && (b.Code.Contains(filters) || b.NameE.Contains(filters))).OrderBy(o => o.Code);
                    var result = m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId });
                    var z = result.ToDataSourceResult(request);
                    return Json(z, JsonRequestBehavior.AllowGet);

                }
          }
            else
            {
                if (filters == null || filters == "")
                {
                    var m = db.GL_ChartOfCostCenter.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == false && b.Deleted != true).OrderBy(o => o.Code).AsQueryable();
                    var result = m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA, CompanyId = p.CompanyId });
                    var z = result.ToDataSourceResult(request);
                    return Json(z, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    var m = db.GL_ChartOfCostCenter.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == false && b.Deleted != true && (b.Code.Contains(filters) || b.NameE.Contains(filters))).OrderBy(o => o.Code).AsQueryable();
                    var result = m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA, CompanyId = p.CompanyId });
                    var z = result.ToDataSourceResult(request);
                    return Json(z, JsonRequestBehavior.AllowGet);

                }
            }
        }
        public ActionResult FillCostCentersParent(int? CompanyId)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.GL_ChartOfCostCenter.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == true && b.Deleted != true);

                return Json(m.Select(p => new { Id = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.GL_ChartOfCostCenter.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == true && b.Deleted != true);

                return Json(m.Select(p => new { Id = p.Id, Name = p.NameA }), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillAccounts(int? CompanyId=0,string text="",int?Id=0)
        {
            if (text == "0" || text == "All" || text == Id.ToString()) text = "";
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.GL_ChartOfAccounts.Where(b => b.Id == Id).Union(db.GL_ChartOfAccounts.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == false && b.Deleted != true && (b.NameE.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o=>o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.GL_ChartOfAccounts.Where(b => b.Id == Id).Union(db.GL_ChartOfAccounts.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == false && b.Deleted != true && (b.NameA.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillAllAccounts([DataSourceRequest] DataSourceRequest request,int? CompanyId,string filters)
        {
          
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
            
               if (filters==null || filters=="")
               {
                    var m = db.GL_ChartOfAccounts.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == false && b.Deleted != true).OrderBy(o => o.Code);
                    var result = m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId });
                    var z = result.ToDataSourceResult(request);
                   return Json(z, JsonRequestBehavior.AllowGet);

               }
               else
               {
                   var m = db.GL_ChartOfAccounts.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == false && b.Deleted != true && (b.Code.Contains(filters) || b.NameE.Contains(filters))).OrderBy(o => o.Code);
                   var result = m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId });
                   var z = result.ToDataSourceResult(request);
                   return Json(z, JsonRequestBehavior.AllowGet);

               }


            }
            else
            {
                if (filters == null || filters == "")
                {
                    var m = db.GL_ChartOfAccounts.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == false && b.Deleted != true).OrderBy(o => o.Code);
                    var result = m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA, CompanyId = p.CompanyId });
                    var z = result.ToDataSourceResult(request);
                    return Json(z, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    var m = db.GL_ChartOfAccounts.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == false && b.Deleted != true && (b.Code.Contains(filters) || b.NameA.Contains(filters))).OrderBy(o => o.Code);
                    var result = m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA, CompanyId = p.CompanyId });
                    var z = result.ToDataSourceResult(request);
                    return Json(z, JsonRequestBehavior.AllowGet);

                }
            }
        }
        public ActionResult FillAccountsParent(int? CompanyId)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.GL_ChartOfAccounts.Where(b => b.MemberShipId == MemberShipId && b.HasChildren == true && b.Deleted != true);

                return Json(m.Select(p => new { Id = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.GL_ChartOfAccounts.Where(b => b.MemberShipId == MemberShipId && b.HasChildren == true && b.Deleted != true);

                return Json(m.Select(p => new { Id = p.Id, Name = p.NameA }), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillAccountsFirstLevel(int? CompanyId)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.GL_ChartOfAccounts.Where(b => b.MemberShipId == MemberShipId && b.Levels == 1 && b.Deleted != true);

                return Json(m.Select(p => new { Id = p.Id,Code=p.Code, Name = p.NameE }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.GL_ChartOfAccounts.Where(b => b.MemberShipId == MemberShipId && b.Levels == 1 && b.Deleted != true);

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA }), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillJournals()
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);


            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.GL_JournalTypes.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true);
                return Json(m.Select(p => new { Id = p.Id,p.Code, Name = p.NameE }), JsonRequestBehavior.AllowGet);

            }

            else
            {
                var m = db.GL_JournalTypes.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true);
                return Json(m.Select(p => new { Id = p.Id, p.Code, Name = p.NameA }), JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult FillSourceName()
        {
        
            

            if (Request.Cookies["Language"].Value == Language.English)
            {

                return Json(StaticLists.SourceList.Select(p => new { Id = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);

            }

            else
            {
                return Json(StaticLists.SourceList.Select(p => new { Id = p.Id, Name = p.NameA }), JsonRequestBehavior.AllowGet);

            }


        }
        public ActionResult FillBanks()
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);


            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.GL_Banks.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true);
                return Json(m.Select(p => new { Id = p.Id, p.Code, Name = p.NameE }), JsonRequestBehavior.AllowGet);

            }

            else
            {
                var m = db.GL_Banks.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true);
                return Json(m.Select(p => new { Id = p.Id, p.Code, Name = p.NameA }), JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult FillBankAccounts(int? CompanyId, int BankId, string text)
        {
         
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            var m = from Account in db.GL_ChartOfAccounts.Where(b => b.CompanyId == CompanyId)
                    join Bank in db.GL_BankAccounts.Where(b => b.BankId == BankId) on Account.Id equals Bank.AccountId
                    select Account;
                        
            if (Request.Cookies["Language"].Value == Language.English)
            {

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(m.Select(p => new { Id = p.Id, Name = p.NameA, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
        }

        //Stock
        public ActionResult FillProductGroupParent(int? CompanyId)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.Stock_ProductGroups.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == true && b.Deleted != true);

                return Json(m.Select(p => new { Id = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.Stock_ProductGroups.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.HasChildren == true && b.Deleted != true);

                return Json(m.Select(p => new { Id = p.Id, Name = p.NameA }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillProductGroups(int? CompanyId = 0, string text = "", int? Id = 0)
        {
            if (text == "0" || text == "All" || text == Id.ToString()) text = "";
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.Stock_ProductGroups.Where(b => b.Id == Id).Union(db.Stock_ProductGroups.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId  && b.Deleted != true && (b.NameE.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.Stock_ProductGroups.Where(b => b.Id == Id).Union(db.Stock_ProductGroups.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId  && b.Deleted != true && (b.NameA.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillProductUnits(int? CompanyId = 0,int ProductId=0, string text = "", int? Id = 0)
        {
            if (text == "0" || text == "All" || text == Id.ToString()) text = "";
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var ProductUnit = db.Stock_Products_Units.Where(c => c.ProductId == ProductId).Select(s=>s.UnitId);
                var m = db.Stock_Units.Where(b => b.Id == Id).Union(db.Stock_Units.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && ProductUnit.Contains(b.Id) && b.Deleted != true && (b.NameE.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ProductUnit = db.Stock_Products_Units.Where(c => c.ProductId == ProductId).Select(s => s.UnitId);
                var m = db.Stock_Units.Where(b => b.Id == Id).Union(db.Stock_Units.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && ProductUnit.Contains(b.Id) && b.Deleted != true && (b.NameA.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillUnits(int? CompanyId = 0, string text = "", int? Id = 0)
        {
            if (text == "0" || text == "All" || text == Id.ToString()) text = "";
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.Stock_Units.Where(b => b.Id == Id).Union(db.Stock_Units.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true && (b.NameE.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.Stock_Units.Where(b => b.Id == Id).Union(db.Stock_Units.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true && (b.NameA.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillSuppliers(int? CompanyId = 0, string text = "", int? Id = 0)
        {
            if (text == "0" || text == "All" || text == Id.ToString()) text = "";
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.Sys_Partners.Where(b => b.Id == Id).Union(db.Sys_Partners.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.PartnerType == 2 && b.Deleted != true && (b.NameE.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.Sys_Partners.Where(b => b.Id == Id).Union(db.Sys_Partners.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.PartnerType == 2 && b.Deleted != true && (b.NameA.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillBrands(int? CompanyId = 0, string text = "", int? Id = 0)
        {
            if (text == "0" || text == "All" || text == Id.ToString()) text = "";
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.Stock_Brands.Where(b => b.Id == Id).Union(db.Stock_Brands.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true && (b.NameE.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.Stock_Brands.Where(b => b.Id == Id).Union(db.Stock_Brands.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true && (b.NameA.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillProducts(int? CompanyId = 0, string text = "", int? Id = 0)
        {
            if (text == "0" || text == "All" || text == Id.ToString()) text = "";
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.Stock_Products.Where(b => b.Id == Id).Union(db.Stock_Products.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true && (b.NameE.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.Stock_Products.Where(b => b.Id == Id).Union(db.Stock_Products.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true && (b.NameA.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillWarehouses(int? CompanyId = 0, string text = "", int? Id = 0)
        {
            if (text == "0" || text == "All" || text == Id.ToString()) text = "";
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.Stock_Warehouses.Where(b => b.Id == Id).Union(db.Stock_Warehouses.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true && (b.NameE.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var m = db.Stock_Warehouses.Where(b => b.Id == Id).Union(db.Stock_Warehouses.Where(b => b.MemberShipId == MemberShipId && b.CompanyId == CompanyId && b.Deleted != true && (b.NameA.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA, CompanyId = p.CompanyId }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillShelves(int WarehouseId=0, string text = "", int? Id = 0)
        {
            if (text == "0" || text == "All" || text == Id.ToString()) text = "";
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

         
                var m = db.Stock_Warehouses_Shelves.Where(b => b.Id == Id).Union(db.Stock_Warehouses_Shelves.Where(b => b.WarehouseId == WarehouseId && (b.ShelveNo.Contains(text))).Take(100).OrderBy(o => o.ShelveNo));

                return Json(m.Select(p => new { Id = p.Id, Code ="", Name = p.ShelveNo }), JsonRequestBehavior.AllowGet);
           
        }

        public ActionResult FillRacks(int ShelveId = 0, string text = "", int? Id = 0)
        {
            if (text == "0" || text == "All" || text == Id.ToString()) text = "";
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);


            var m = db.Stock_Warehouses_Racks.Where(b => b.Id == Id).Union(db.Stock_Warehouses_Racks.Where(b => b.ShelveId == ShelveId && (b.RackNo.Contains(text))).Take(100).OrderBy(o => o.RackNo));

            return Json(m.Select(p => new { Id = p.Id, Code = "", Name = p.RackNo }), JsonRequestBehavior.AllowGet);

        }

        public ActionResult FillRackRows(int RackId = 0, string text = "", int? Id = 0)
        {
            if (text == "0" || text == "All" || text == Id.ToString()) text = "";
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);


            var m = db.Stock_Warehouses_RackRows.Where(b => b.Id == Id).Union(db.Stock_Warehouses_RackRows.Where(b => b.RackId == RackId && (b.RowNo.Contains(text))).Take(100).OrderBy(o => o.RowNo));

            return Json(m.Select(p => new { Id = p.Id, Code = "", Name = p.RowNo }), JsonRequestBehavior.AllowGet);

        }

        public ActionResult FillRackCases(int RowId = 0, string text = "", int? Id = 0)
        {
            if (text == "0" || text == "All" || text == Id.ToString()) text = "";
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);


            var m = db.Stock_Warehouses_RackCases.Where(b => b.Id == Id).Union(db.Stock_Warehouses_RackCases.Where(b => b.RowId == RowId && (b.CaseNo.Contains(text))).Take(100).OrderBy(o => o.CaseNo));

            return Json(m.Select(p => new { Id = p.Id, Code = "", Name = p.CaseNo }), JsonRequestBehavior.AllowGet);

        }

        public ActionResult FillColors(string text = "", int? Id = 0)
        {
            int MemberShipId = Convert.ToInt16(Request.Cookies["MemberShipId"].Value);

            if (text == "0" || text == "All" || text == Id.ToString()) text = "";

            if (Request.Cookies["Language"].Value == Language.English)
            {
                var m = db.Sys_Colors.Where(b => b.Id == Id).Union(db.Sys_Colors.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true && (b.NameE.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameE }), JsonRequestBehavior.AllowGet);

            }
            else
            {
                var m = db.Sys_Colors.Where(b => b.Id == Id).Union(db.Sys_Colors.Where(b => b.MemberShipId == MemberShipId && b.Deleted != true && (b.NameA.Contains(text) || b.Code.Contains(text))).Take(100).OrderBy(o => o.Code));

                return Json(m.Select(p => new { Id = p.Id, Code = p.Code, Name = p.NameA }), JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult FillValueOrPercent()
        {



            if (Request.Cookies["Language"].Value == Language.English)
            {

                return Json(StaticLists.DiscountTypeList.Select(p => new { Id = p.Id, Code = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);

            }

            else
            {
                return Json(StaticLists.DiscountTypeList.Select(p => new { Id = p.Id, Code = p.Id, Name = p.NameA }), JsonRequestBehavior.AllowGet);

            }


        }
        public ActionResult FillPurchasePricingPolicy()
        {



            if (Request.Cookies["Language"].Value == Language.English)
            {

                return Json(StaticLists.PurchasePricingPolicyList.Select(p => new { Id = p.Id,Code=p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);

            }

            else
            {
                return Json(StaticLists.PurchasePricingPolicyList.Select(p => new { Id = p.Id, Code = p.Id, Name = p.NameA }), JsonRequestBehavior.AllowGet);

            }


        }
        public ActionResult FillSalesPricingPolicy()
        {



            if (Request.Cookies["Language"].Value == Language.English)
            {

                return Json(StaticLists.SalesPricingPolicyList.Select(p => new { Id = p.Id, Code = p.Id, Name = p.NameE }), JsonRequestBehavior.AllowGet);

            }

            else
            {
                return Json(StaticLists.SalesPricingPolicyList.Select(p => new { Id = p.Id, Code = p.Id, Name = p.NameA }), JsonRequestBehavior.AllowGet);

            }


        }


      

	}
}