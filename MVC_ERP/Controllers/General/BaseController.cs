using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using MVC_ERP.Models;
using System.Web.Routing;
using System.Data;
using System.Reflection;

using System.Globalization;
using System.Web.Http;
namespace MVC_ERP.Controllers.General
{
    public class BaseController : Controller
    {
        public MSDBContext db = new MSDBContext();
        //protected override void Initialize(RequestContext requestContext)
        //{ 
        //TempData["MainIdReturnBackToDetailPage"]="";
        //TempData["MainNameReturnBackToDetailPage"] = "";
        //}
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
          

            string cultureName = null;

            // Attempt to read the culture cookie from Request
            if (Request.Cookies["Language"] != null)
            {
            if (Request.Cookies["Language"].Value.ToString() == "Arabic")

                cultureName = Culture.AR;
            else
                //cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                //        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                //        null;
            // Validate culture name
                cultureName = Culture.EN; ; // This is safe
            }
            else
            {
                Response.Cookies.Set(new HttpCookie("Language", Language.English));
                cultureName = Culture.EN;
            }
            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public enum FormTypes
        {
            GL_DailyVoucher=1,
            GL_PaymentVoucher=2,
            GL_ReceiptVoucher=3,

            Stock_OpeningBalance = 11,
            Stock_RecieptOrder = 12,
            Stock_DeleviryOrder = 13,
            Stock_ExecutionOrder = 14,
            Stock_TransferOrder = 15,
            Stock_StockTacking = 16,
            Stock_CollectionOrder = 17,
            Stock_DecollectionOrder = 18,
            Stock_PurchaseRequest = 19,
            Stock_PurchaseRequestQuotation = 20,
            Stock_PurchaseQuotation = 21,
            Stock_PurchaseOrder = 22,
            Stock_PurchaseInvoice = 23,
            Stock_ReturnPurchaseInvoice = 24,
            Stock_SalesQuotation = 25,
            Stock_SalesOrder = 26,
            Stock_SalesInvoice = 27,
            Stock_ReturnSalesInvoice = 28,

        }
        public int UserId, MemberShipId,DocumentSource;
        public int? FinancialYearId,FinancialYearName;
        public string Lang;
        public void PrepareUserDat()
        {
     
            if (User.Identity.GetUserId() == null || Request.Cookies["MemberShipId"] == null)
            {
                AuthenticationManager.SignOut();
                 RedirectToAction("LogOn", "Account");
                 return;
            }
            UserId = Convert.ToInt32(User.Identity.GetUserId());
            MemberShipId = Convert.ToInt32(Request.Cookies["MemberShipId"].Value);
           
            if (Request.Cookies["FinancialYearId"].Value=="")
            {
                FinancialYearId =null;
                FinancialYearName = null;
            }
                else
            {
            FinancialYearId = Convert.ToInt32(Request.Cookies["FinancialYearId"].Value);
            FinancialYearName = Convert.ToInt32(Request.Cookies["FinancialYear"].Value);
            }
            if (Request.Cookies["Language"].Value == "Arabic")
            {
                Lang = Language.Arabic;
            }
            else
            {
                Lang = Language.English;
            }
        }
        public struct PermissionType
        {
            public const int Open = 1;
            public const int Insert = 2;
            public const int Modifay = 3;
            public const int Delete = 4;
            public const int Reuse = 5;
            public const int Post = 6;
            public const int UnPost = 7;
            public const int Share = 8;
            public const int Search = 9;
            public const int Print = 10;

        }
        public bool CheckUserPermission(int PerType) 
        {
                                
            string Url = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
           

            if (UserId == -698245)
            {
                //Programmers
                return true;
            }


            var u=Url.Split('/');

            var LastUrl="";
            if(u.Length==3)
            {
                LastUrl = u[1] + "/" + "Index";
            }
            else
            {
            LastUrl = u[2] +"/"+ "Index";
            }
        

            List<Priv_Menus> Mlist = new List<Priv_Menus>();
            Mlist = db.Priv_Menus.Where(c => c.UrlLink == LastUrl && c.Sold==true && c.OnlyForProgrammer==false).ToList();

            if (Mlist.Count==0)
            {
                return false;
            }

            if (UserId == -698246)
            {
                //System admin have all permission for all sold menues and not only for programmers
                return true;
            }

            var MenueIDS = Mlist.Select(s => s.Id);
            var GroupIds = db.Priv_Users_Groups.Where(c => c.UserId == UserId).Select(c => c.GroupId);

            List<Priv_Groups_Rights> Glist = new List<Priv_Groups_Rights>();

            if (PerType == PermissionType.Open)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Open == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
               
            }

            if (PerType == PermissionType.Insert)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Insert == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

             }

            if (PerType == PermissionType.Modifay)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Modifay == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }


            if (PerType == PermissionType.Delete)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Delete == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }


            if (PerType == PermissionType.Reuse)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Reuse == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            if (PerType == PermissionType.Post)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Post == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }

            if (PerType == PermissionType.UnPost)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.UnPost == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }

            if (PerType == PermissionType.Share)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Share == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }

            if (PerType == PermissionType.Search)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Search == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }

            if (PerType == PermissionType.Print)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Print == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            return false;
        }
        public ActionResult CheckPermission(int PerType, string Url)
        {
            //for button that dont go to server such as print button

            PrepareUserDat();

            if (UserId == -698245)
            {
                //Programmers
                return Json(new { result = "True" }, JsonRequestBehavior.AllowGet);
            }

            var u = Url.Split('/');

            var LastUrl = "";
            if (u.Length == 3)
            {
                LastUrl = u[1] + "/" + "Index";
            }
            else
            {
                LastUrl = u[2] + "/" + "Index";
            }

            List<Priv_Menus> Mlist = new List<Priv_Menus>();
            Mlist = db.Priv_Menus.Where(c => c.UrlLink == LastUrl && c.Sold == true && c.OnlyForProgrammer == false).ToList();

            if (Mlist.Count == 0)
            {
                return Json(new { result = "False", MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") }, JsonRequestBehavior.AllowGet);
            }

            if (UserId == -698246)
            {
                //System admin have all permission for all sold menues and not only for programmers
                return Json(new { result = "True" }, JsonRequestBehavior.AllowGet);
            }

            var MenueIDS = Mlist.Select(s => s.Id).ToList();
            var GroupIds = db.Priv_Users_Groups.Where(c => c.UserId == UserId).Select(c => c.GroupId).ToList();

            List<Priv_Groups_Rights> Glist = new List<Priv_Groups_Rights>();

            if (PerType == PermissionType.Print)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Print == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return Json(new { result = "False", MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "True" }, JsonRequestBehavior.AllowGet);
                }

            }
            if (PerType == PermissionType.Search)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Search == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return Json(new { result = "False", MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "True" }, JsonRequestBehavior.AllowGet);
                }

            }

            if (PerType == PermissionType.Delete)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Delete == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return Json(new { result = "False", MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { result = "True" }, JsonRequestBehavior.AllowGet);
                }

            }


            if (PerType == PermissionType.Reuse)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Reuse == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return Json(new { result = "False", MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { result = "True" }, JsonRequestBehavior.AllowGet);
                }

            }
            if (PerType == PermissionType.Post)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.Post == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return Json(new { result = "False", MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { result = "True" }, JsonRequestBehavior.AllowGet);
                }

            }

            if (PerType == PermissionType.UnPost)
            {
                Glist = db.Priv_Groups_Rights.Where(c => c.UnPost == true && MenueIDS.Contains(c.MenuId) && GroupIds.Contains(c.GroupId)).ToList();
                if (Glist.Count == 0)
                {
                    return Json(new { result = "False", MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { result = "True" }, JsonRequestBehavior.AllowGet);
                }

            }

            return Json(new { result = "False", MessageType = "danger", ReturnMessage = (Request.Cookies["Language"].Value == Language.English ? "Sorry, you don't have permission" : "نأسف ليس لديك صلاحيات كافية") }, JsonRequestBehavior.AllowGet);
        }
        public DateTime FinancialYearStartDate(int FinancialYearId)  
        {
            GL_FinancialYears FYTBL = db.GL_FinancialYears.Find(FinancialYearId);

            return FYTBL.StartDate.Gregi.Value;
        
        }

      
        public DateTime FinancialYearEndDate(int FinancialYearId)
        {
            GL_FinancialYears FYTBL = db.GL_FinancialYears.Find(FinancialYearId);

            return FYTBL.EndDate.Gregi.Value;

        }

        public ActionResult GetPopUpSessionValue()
        {
            if (HttpContext.Session["MainIdReturnBackToDetailPage"].ToString() == "") System.Web.HttpContext.Current.Session["MainIdReturnBackToDetailPage"] = 0;
            if (HttpContext.Session["MainNameReturnBackToDetailPage"] == null) System.Web.HttpContext.Current.Session["MainNameReturnBackToDetailPage"] = "";

            return Json(new { Id =Convert.ToInt32( HttpContext.Session["MainIdReturnBackToDetailPage"].ToString()), Name = HttpContext.Session["MainNameReturnBackToDetailPage"].ToString() });

        }

        public ActionResult SetPopUpSessionValue()
        {
            System.Web.HttpContext.Current.Session["IsReturnBackToDetailPageActive"] = "True";
            return Json("");

        }

        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                if (prop.Name=="Id")
                {
                    dataTable.Columns.Add(prop.Name, typeof(int));
                }
                else
                {
                    dataTable.Columns.Add(prop.Name);
                }
               
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public enum MessageTypes
        {
            SaveSuccess = 1,
            ModifaySuccess = 2,
            DeleteSuccess = 3,
            PostSuccess = 4,
            ManagerConfirm = 5,
            MangerCancel = 6,
            EditWhenPost = 7,
            NoPermission = 8,
            NoData=9,
            DeleteWhenPost = 10,
            UnPostSuccess=11  ,
            HasChildern = 12
        }
        public string GetMessage(MessageTypes MsgType)
        {

            if (MsgType == MessageTypes.SaveSuccess)
            {
                 if (Request.Cookies["Language"].Value == "English")
                 {
                     return "Save successful";
                 }
                 else
                 {
                     return "تم الحفظ بنجاح";
                 }
            }

            if (MsgType == MessageTypes.ModifaySuccess)
            {
                if (Request.Cookies["Language"].Value == "English")
                {
                    return "Modifay successful";
                }
                else
                {
                    return "تم التعديل بنجاح";
                }
            }

            if (MsgType == MessageTypes.DeleteSuccess)
            {
                if (Request.Cookies["Language"].Value == "English")
                {
                    return "Delete successful";
                }
                else
                {
                    return "تم الحذف بنجاح";
                }
            }

            if (MsgType == MessageTypes.PostSuccess)
            {
                if (Request.Cookies["Language"].Value == "English")
                {
                    return "Post successful";
                }
                else
                {
                    return "تم التوثيق بنجاح";
                }
            }

            if (MsgType == MessageTypes.UnPostSuccess)
            {
                if (Request.Cookies["Language"].Value == "English")
                {
                    return "Un-Post successful";
                }
                else
                {
                    return "تم فك التوثيق بنجاح";
                }
            }

            if (MsgType == MessageTypes.ManagerConfirm)
            {
                if (Request.Cookies["Language"].Value == "English")
                {
                    return "Confirm successful";
                }
                else
                {
                    return "تم اعتماد المستند";
                }
            }

            if (MsgType == MessageTypes.MangerCancel)
            {
                if (Request.Cookies["Language"].Value == "English")
                {
                    return "Cancel successful";
                }
                else
                {
                    return "تم الغاء المستند";
                }
            }
 
          if (MsgType == MessageTypes.EditWhenPost)
            {
                if (Request.Cookies["Language"].Value == "English")
                {
                    return "This movment was posting you can't modify it";
                }
                else
                {
                    return "عفوا هذة الحركة موثقة لا يمكنك التعديل";
                }
            }
          if (MsgType == MessageTypes.NoPermission)
            {
                if (Request.Cookies["Language"].Value == "English")
                {
                    return "Sorry, you don't have permission";
                }
                else
                {
                    return "عفوا ليس لديك صلاحيات كافية";
                }
            }
          if (MsgType == MessageTypes.NoData)
            {
                if (Request.Cookies["Language"].Value == "English")
                {
                    return "No data to use";
                }
                else
                {
                    return "لا يوجد بيانات لاستخدامها";
                }
            }
          if (MsgType == MessageTypes.DeleteWhenPost)
            {
                if (Request.Cookies["Language"].Value == "English")
                {
                    return "This movment was posting you can't delete it";
                }
                else
                {
                    return "عفوا هذة الحركة موثقة لا يمكنك الحذف";
                }
            }

          if (MsgType == MessageTypes.HasChildern)
          {
              if (Request.Cookies["Language"].Value == "English")
              {
                  return "Sorry you can't delete this record cause it's  has childeren";
              }
              else
              {
                  return "عفواً لا يمكن حذف هذة الحركة تحتوي علي عناصر";
              }
          }



            return "";

        }

    }
}