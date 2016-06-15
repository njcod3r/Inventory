using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_ERP.Models
{
   

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MSDBContext())
            {
                Database.SetInitializer<MSDBContext>(new MenueContextSeedInitializer());

                var stdQuery = (from d in context.Priv_Menus
                                select new { Id = d.Id, Name = d.NameE });

                foreach (var q in stdQuery)
                {
                    Console.WriteLine("ID : " + q.Id + ", Name : " + q.Name);
                }

                Console.ReadKey();
            }
        }
    }
    
    public class StaticLists

    {
        public static List<Priv_Menus> AllMenueList = new List<Priv_Menus>
            {

                new Priv_Menus { Id = 1000, NameE = "Menu", NameA = "القائمة", ParentId = null, Sold = false, Expanded = true, HasChildren = true, UrlLink = "", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                //System
                new Priv_Menus { Id = 1001, NameE = "System", NameA = "النظام", ParentId = 1000, Sold = false, Expanded = false, HasChildren = true, UrlLink = "", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 1002, NameE = "Companies", NameA = "الشركات", ParentId = 1001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Companies/Index", ImageUrlLink = "", Target = "frame", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 1003, NameE = "Branches", NameA = "الفروع", ParentId = 1001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Branches/Index", ImageUrlLink = "", Target = "frame", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 1004, NameE = "Countries", NameA = "الدول", ParentId = 1001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Countries/Index", ImageUrlLink = "", Target = "frame", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 1005, NameE = "Cities", NameA = "المدن", ParentId = 1001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Cities/Index", ImageUrlLink = "", Target = "frame", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 1006, NameE = "Currencies", NameA = "العملات", ParentId = 1001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Currencies/Index", ImageUrlLink = "", Target = "frame", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 1007, NameE = "Activities", NameA = "الأنشطة", ParentId = 1001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Activities/Index", ImageUrlLink = "", Target = "frame", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 1008, NameE = "Customer groups", NameA = "مجموعات العملاء", ParentId = 1001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "CustomerGroups/Index", ImageUrlLink = "", Target = "frame", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 1009, NameE = "Customers", NameA = "العملاء", ParentId = 1001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Customers/Index", ImageUrlLink = "", Target = "frame", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 1010, NameE = "Supplier groups", NameA = "مجموعات العملاء", ParentId = 1001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "SupplierGroups/Index", ImageUrlLink = "", Target = "frame", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 1011, NameE = "Suppliers", NameA = "العملاء", ParentId = 1001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Suppliers/Index", ImageUrlLink = "", Target = "frame", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                 new Priv_Menus { Id = 1012, NameE = "Projects", NameA = "المشاريع", ParentId = 1001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Projects/Index", ImageUrlLink = "", Target = "frame", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
              
                //GL
                new Priv_Menus { Id = 2000, NameE = "General ledger", NameA = "الحسابات", ParentId = 1000, Sold = false, Expanded = false, HasChildren = true, UrlLink = "", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 2001, NameE = "Financial years", NameA = "الفترات المحاسبية", ParentId = 2000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "GL_FinancialYears/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 2002, NameE = "Chart of cost centers", NameA = "شجرة مراكز التكلفة", ParentId = 2000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "GL_ChartOFCostCenters/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 2003, NameE = "Chart of accounts", NameA = "شجرة الحسابات", ParentId = 2000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "GL_ChartOFAccounts/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 2004, NameE = "Account setting", NameA = "اعدادات الحسابات", ParentId = 2000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "GL_Setting/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 2005, NameE = "Journal types", NameA = "انواع اليوميات", ParentId = 2000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "GL_JournalTypes/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 2006, NameE = "Banks", NameA = "البنوك", ParentId = 2000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "GL_Banks/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 2007, NameE = "Opening balance", NameA = "الارصدة الافتتاحية", ParentId = 2000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "GL_OppeningBalance/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 2008, NameE = "Estimate budget", NameA = "الموازنة التقديرية", ParentId = 2000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "GL_EstimateBudget/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 2009, NameE = "Daily voucher", NameA = "القيود اليومية", ParentId = 2000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "GL_DailyVoucher/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 2010, NameE = "Payment voucher", NameA = "سندات الصرف", ParentId = 2000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "GL_PayVoucher/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 2011, NameE = "Receipt voucher", NameA = "سندات القبض", ParentId = 2000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "GL_ReceiptVoucher/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },

                  //Stock
                new Priv_Menus { Id = 3000, NameE = "Stock", NameA = "المخزون", ParentId = 1000, Sold = false, Expanded = false, HasChildren = true, UrlLink = "", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 3001, NameE = "Warehouses", NameA = "المستودعات", ParentId = 3000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Stock_Warehouses/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 3002, NameE = "Units", NameA = "الوحدات", ParentId = 3000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Stock_Units/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 3003, NameE = "Product Groups", NameA = "مجموعات المنتجات", ParentId = 3000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Stock_Groups/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 3004, NameE = "Brands", NameA = "العلامات التجارية", ParentId = 3000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Stock_Brands/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 3005, NameE = "Products", NameA = "المنتجات", ParentId = 3000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Stock_Products/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 3006, NameE = "Stock Setting", NameA = "اعدادات المخزون", ParentId = 3000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Stock_Setting/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 3007, NameE = "Opening Balance", NameA = "الأرصدة الافتتاحية", ParentId = 3000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Stock_OpeningBalance/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 3008, NameE = "Receive Order", NameA = "اذن استلام بضاعة", ParentId = 3000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Stock_Units/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 3009, NameE = "Deliver Order", NameA = "اذن صرف بضاعة", ParentId = 3000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Stock_Units/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 3010, NameE = "Transfer Order", NameA = "اذن تحويل بضاعة", ParentId = 3000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Stock_Units/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 3011, NameE = "Execution Order", NameA = "اذن اعدام بضاعة", ParentId = 3000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Stock_Units/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 3012, NameE = "Stocktaking", NameA = "جرد المخزون", ParentId = 3000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Stock_Units/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },

                  //Reports
                new Priv_Menus { Id = 29000, NameE = "Reports", NameA = "التقارير", ParentId = 1000, Sold = false, Expanded = false, HasChildren = true, UrlLink = "", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 29001, NameE = "Finance Reports", NameA = "التقارير المحاسبية", ParentId = 29000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = true, Notes = "" },
                new Priv_Menus { Id = 29002, NameE = "Account Statement", NameA = "كشف حساب", ParentId = 29001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Rpt_GL_AccountStatment/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 29003, NameE = "Account Summary", NameA = "ملخص حركة حساب", ParentId = 29001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Rpt_GL_AccountSummary/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 29004, NameE = "Account Balance /Period", NameA = "رصيد حساب بالفترات", ParentId = 29001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Rpt_GL_PeriodBalance/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 29005, NameE = "Cost Center Statement", NameA = "كشف حساب مركز تكلفة", ParentId = 29001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Rpt_GL_CCenterStatment/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 29006, NameE = "Cost Center Balance", NameA = "ارصدة مراكز التكلفة", ParentId = 29001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Rpt_GL_CCenterBalance/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 29007, NameE = "Trial Balance", NameA = "ميزان المراجعة", ParentId = 29001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Rpt_GL_TrialBalance/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 29008, NameE = "Income Statement", NameA = "قائمة الدخل", ParentId = 29001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Rpt_GL_IncomeStatement/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 29009, NameE = "Balance Sheet", NameA = "الميزانية العمومية", ParentId = 29001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Rpt_GL_BalanceSheet/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 29010, NameE = "Compare Budjet & Real", NameA = "مقارنة الموازنة بالفعلي", ParentId = 29001, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Rpt_GL_BudjetComparison/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },


                //Privileges
                new Priv_Menus { Id = 30000, NameE = "Privileges", NameA = "الصلاحيات", ParentId = 1000, Sold = false, Expanded = false, HasChildren = true, UrlLink = "", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 30001, NameE = "Sold operation", NameA = "عملية البيع", ParentId = 30000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Priv_Programmer/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = true, Notes = "" },
                new Priv_Menus { Id = 30002, NameE = "Groups", NameA = "المجموعات", ParentId = 30000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Priv_Groups/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 30003, NameE = "Users", NameA = "المستخدمين", ParentId = 30000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Priv_Users/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 30004, NameE = "System admin", NameA = "مدير النظام", ParentId = 30000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Priv_Backup/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 30005, NameE = "Log history", NameA = "سجل المستخدمين", ParentId = 30000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Priv_Backup/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
                new Priv_Menus { Id = 30006, NameE = "Database operation", NameA = "عمليات قاعدة البيانات", ParentId = 30000, Sold = false, Expanded = false, HasChildren = false, UrlLink = "Priv_Backup/Index", ImageUrlLink = "", Target = "", HtmlAttributes = "", OnlyForProgrammer = false, Notes = "" },
           
            };

             public static List<Source_Name> SourceList = new List<Source_Name>
             {
                // Form Name appear as source in Daily Voucher
                new Source_Name { Id = 1, NameE = "Daily voucher", NameA = "القيود اليومية"},
                new Source_Name { Id = 2, NameE = "Payment voucher", NameA = "سند الصرف"},
                new Source_Name { Id = 3, NameE = "Receipt voucher", NameA = "سند القبض"},

               };

             public static List<Voucher_Name> VoucherList = new List<Voucher_Name>
             {
                //For Bind Subsystem With Journal
                new Voucher_Name { Id = 1, NameE = "Cash Payment Voucher", NameA = "سند صرف نقدي"},
                new Voucher_Name { Id = 2, NameE = "Check Payment voucher", NameA = "سند صرف شيكات"},
                new Voucher_Name { Id = 3, NameE = "Suppliers Cash Payment", NameA = "سداد الموردين نقدا"},
                new Voucher_Name { Id = 4, NameE = "Suppliers Check Payment", NameA = "سداد الموردين شيكات"},
                new Voucher_Name { Id = 5, NameE = "Suppliers Cash Prepaid", NameA = "دفعات مقدمة نقدا للموردين"},
                new Voucher_Name { Id = 6, NameE = "Suppliers Check Prepaid", NameA = "دفعات مقدمة بشيكات للموردين"},
                new Voucher_Name { Id = 7, NameE = "Cash Prepaid Expenses Payment", NameA = "مصروفات مدفوعة مقدما نقدا"},
                new Voucher_Name { Id = 8, NameE = "Check Prepaid Expenses Payment", NameA = "مصروفات مدفوعة مقدما شيكات"},

                new Voucher_Name { Id = 9, NameE = "Cash Receipt Voucher", NameA = "سند صرف نقدي"},
                new Voucher_Name { Id = 10, NameE = "Check Receipt voucher", NameA = "سند صرف شيكات"},
                new Voucher_Name { Id = 11, NameE = "Customers Cash Receipt", NameA = "سداد العملاء نقدا"},
                new Voucher_Name { Id = 12, NameE = "Customers Check Receipt", NameA = "سداد العملاء شيكات"},
                new Voucher_Name { Id = 13, NameE = "Customers Cash Prepaid", NameA = "دفعات مقدمة نقدا للعملاء"},
                new Voucher_Name { Id = 14, NameE = "Customers Check Prepaid", NameA = "دفعات مقدمة بشيكات للعملاء"},

               };

             public static List<DiscountType> DiscountTypeList = new List<DiscountType>
             {
                
                new DiscountType { Id = 1, NameE = "Value", NameA = "قيمة"},
                new DiscountType { Id = 2, NameE = "Percentge", NameA = "نسبة"},
             

               };

             public static List<PurchasePricingPolicy> PurchasePricingPolicyList = new List<PurchasePricingPolicy>
             {
                
                new PurchasePricingPolicy { Id = 1, NameE = "Real", NameA = "حقيقي"},
                new PurchasePricingPolicy { Id = 2, NameE = "Last Purchase", NameA = "اخر فاتورة مشتريات"},
             

               };

             public static List<SalesPricingPolicy> SalesPricingPolicyList = new List<SalesPricingPolicy>
             {
                
                new SalesPricingPolicy { Id = 1, NameE = "Real", NameA = "حقيقي"},
                new SalesPricingPolicy { Id = 2, NameE = "Last Sales", NameA = "اخر فاتورة مبيعات"},
             

               };

    }



}