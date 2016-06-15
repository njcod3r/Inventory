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
using System.Xml.Linq;

namespace MVC_ERP.Controllers.Stock.FormClass
{
    public class Stock_SettingController : BaseController
    {

        #region "I N D E X"
		  // index action method
        [Authorize]
        public ActionResult Index(int? Id, Stock_Setting_General MainTbl, int? CompanyListID)
        {

            PrepareUserDat();

            //Read function
            if (Id == null)
            {
                Id = db.Stock_Setting_General.Where(t => t.MemberShipId == MemberShipId && t.Deleted != true).Max(c => (int?)c.Id);
            }

            if (Id != null)
            {

                MainTbl = db.Stock_Setting_General.Find(Id);
            }


            //ViewData["TVDS"] = TreeviewWarehouse(0, "", 1);

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                return View(MainTbl);
            }

            return View(MainTbl);

        } 
	#endregion
      
        #region "I N S E R T"
        // Insert Function
        [Authorize]
        public ActionResult Insert(int Id, int CompanyId, int BranchId, int AccountBindMethod, int? BoxAccountId, int? VisaAccountId, int StocktackingType, int? ReceiptDeleviryPolicy, int? DefaultPayMethod,
                                    int? SalesAccountId, int? PurchaseAccountId, int? ReturnSalesAccountId, int? ReturnPurchaseAccountId, int? StoreAccountId, int? CostGoodsAccountId, int? GoodsInTransitAccountId,
                                    int? AdjustAccountId, int? DestoryAccount, int? AllowDiscountAccountId, int? EarnDiscountAccountId, int? SalesAccount, int? ReturnSalesAccount, string xmlWarehouseNodes, string xmlProductGroupNodes)
        {
            PrepareUserDat();

            //insert or update header

            //Insert New
            Stock_Setting_General MainTbl = new Stock_Setting_General();

            if (Id == 0)
            {
                MainTbl.Id = (db.Stock_Setting_General.Max(c => (int?)c.Id) ?? 0) + 1;
                MainTbl.MemberShipId = MemberShipId;

                MainTbl.CompanyId = CompanyId;
                MainTbl.BranchId = BranchId;
                MainTbl.AccountBindMethod = AccountBindMethod;
                MainTbl.BoxAccountId = BoxAccountId;
                MainTbl.VisaAccountId = VisaAccountId;
                MainTbl.StocktackingType = StocktackingType;
                MainTbl.ReceiptDeleviryPolicy = ReceiptDeleviryPolicy;
                MainTbl.DefaultPayMethod = DefaultPayMethod;
                MainTbl.Create_Uid = UserId;
                MainTbl.Create_Date = DateTime.Now;




            }

            else
            //Update
            {


                MainTbl = db.Stock_Setting_General.Find(Id);

                if (MainTbl.Post == true)
                {
                    return Json(new { MessageType = "info", ReturnMessage = "This movment was posting you can't modify it" });
                }



                MainTbl.MemberShipId = MemberShipId;

                MainTbl.CompanyId = CompanyId;
                MainTbl.BranchId = BranchId;
                MainTbl.AccountBindMethod = AccountBindMethod;
                MainTbl.BoxAccountId = BoxAccountId;
                MainTbl.VisaAccountId = VisaAccountId;
                MainTbl.StocktackingType = StocktackingType;
                MainTbl.ReceiptDeleviryPolicy = ReceiptDeleviryPolicy;
                MainTbl.DefaultPayMethod = DefaultPayMethod;
                MainTbl.Write_Uid = UserId;
                MainTbl.Write_Date = DateTime.Now;

            }

            //Detail Table



            if (AccountBindMethod == 2)
            {
                var model = JsonConvert.DeserializeObject<List<WareHouseModel.RootObject>>(xmlWarehouseNodes);
                Stock_Setting_Accounts DtlList = new Stock_Setting_Accounts();
                MainTbl.Stock_Setting_Accounts = new List<Stock_Setting_Accounts>();

                foreach (var item in model[0].items)
                {
                    DtlList = new Stock_Setting_Accounts();

                    if (item.@checked == true && item.Id != 0)
                    {
                        var DAllDetails = db.Stock_Setting_Accounts.Where(p => p.MasterId == MainTbl.Id && p.WarehouseId == item.Id);
                        db.Stock_Setting_Accounts.RemoveRange(DAllDetails);

                        DtlList.WarehouseId = Convert.ToInt32(item.Id);
                        DtlList.MasterId = MainTbl.Id;
                        DtlList.BranchId = BranchId;
                        DtlList.SalesAccountId = (SalesAccountId == 0 ? null : SalesAccountId);
                        DtlList.ReturnSalesAccountId = (ReturnSalesAccountId == 0 ? null : ReturnSalesAccountId);
                        DtlList.PurchaseAccountId = (PurchaseAccountId == 0 ? null : PurchaseAccountId);
                        DtlList.ReturnPurchaseAccountId = (ReturnPurchaseAccountId == 0 ? null : ReturnPurchaseAccountId);
                        DtlList.GoodsInTransitAccountId = (GoodsInTransitAccountId == 0 ? null : GoodsInTransitAccountId);
                        DtlList.CostGoodsAccountId = (CostGoodsAccountId == 0 ? null : CostGoodsAccountId);
                        DtlList.AdjustAccountId = (AdjustAccountId == 0 ? null : AdjustAccountId);
                        DtlList.AllowDiscountAccountId = (AllowDiscountAccountId == 0 ? null : AllowDiscountAccountId);
                        DtlList.EarnDiscountAccountId = (EarnDiscountAccountId == 0 ? null : EarnDiscountAccountId);
                        DtlList.StoreAccountId = (StoreAccountId == 0 ? null : StoreAccountId);
                        DtlList.EarnDiscountAccountId = (EarnDiscountAccountId == 0 ? null : EarnDiscountAccountId);

                        MainTbl.Stock_Setting_Accounts.Add(DtlList);
                    }
                }
            }

            else if (AccountBindMethod == 3)
            {
                //List<string> h = xmlNodes;
                //var states = (JObject)xmlNodes;

                string m = "{items:" + xmlProductGroupNodes + "}";
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();

                //object Menue = serializer1.Deserialize(xmlNodes, typeof(JObject));

                XmlDocument xml = JsonConvert.DeserializeXmlNode(m);
                XmlReader xr = new XmlNodeReader(xml);

                DataSet ds = new DataSet(xmlProductGroupNodes);
                ds.ReadXml(xr);

                DataTable TblMenue = ds.Tables[0];


                Stock_Setting_Accounts DtlList = new Stock_Setting_Accounts();
                MainTbl.Stock_Setting_Accounts = new List<Stock_Setting_Accounts>();

                for (int i = 0; i < TblMenue.Rows.Count; i++)
                {
                    DtlList = new Stock_Setting_Accounts();

                    bool tts = Convert.ToBoolean(TblMenue.Rows[i][5]);
                    if (tts == true && TblMenue.Rows[i]["Id"].ToString() != "")
                    {

                        var DAllDetails = db.Stock_Setting_Accounts.Where(p => p.MasterId == MainTbl.Id && p.GroupId == Convert.ToInt32(TblMenue.Rows[i][3]));
                        db.Stock_Setting_Accounts.RemoveRange(DAllDetails);

                        DtlList.GroupId = Convert.ToInt32(TblMenue.Rows[i][3]);
                        DtlList.MasterId = MainTbl.Id;
                        DtlList.BranchId = BranchId;
                        DtlList.SalesAccountId = (SalesAccountId == 0 ? null : SalesAccountId);
                        DtlList.ReturnSalesAccountId = (ReturnSalesAccountId == 0 ? null : ReturnSalesAccountId);
                        DtlList.PurchaseAccountId = (PurchaseAccountId == 0 ? null : PurchaseAccountId);
                        DtlList.ReturnPurchaseAccountId = (ReturnPurchaseAccountId == 0 ? null : ReturnPurchaseAccountId);
                        DtlList.GoodsInTransitAccountId = (GoodsInTransitAccountId == 0 ? null : GoodsInTransitAccountId);
                        DtlList.CostGoodsAccountId = (CostGoodsAccountId == 0 ? null : CostGoodsAccountId);
                        DtlList.AdjustAccountId = (AdjustAccountId == 0 ? null : AdjustAccountId);
                        DtlList.AllowDiscountAccountId = (AllowDiscountAccountId == 0 ? null : AllowDiscountAccountId);
                        DtlList.EarnDiscountAccountId = (EarnDiscountAccountId == 0 ? null : EarnDiscountAccountId);
                        DtlList.StoreAccountId = (StoreAccountId == 0 ? null : StoreAccountId);
                        DtlList.EarnDiscountAccountId = (EarnDiscountAccountId == 0 ? null : EarnDiscountAccountId);

                        MainTbl.Stock_Setting_Accounts.Add(DtlList);

                    }

                }

            }
            else
            {
                var DAllDetails = db.Stock_Setting_Accounts.Where(p => p.MasterId == MainTbl.Id);
                db.Stock_Setting_Accounts.RemoveRange(DAllDetails);

                Stock_Setting_Accounts DtlList = new Stock_Setting_Accounts();
                MainTbl.Stock_Setting_Accounts = new List<Stock_Setting_Accounts>();

                DtlList = new Stock_Setting_Accounts();

                DtlList.MasterId = MainTbl.Id;
                DtlList.BranchId = BranchId;
                DtlList.SalesAccountId = (SalesAccountId == 0 ? null : SalesAccountId);
                DtlList.ReturnSalesAccountId = (ReturnSalesAccountId == 0 ? null : ReturnSalesAccountId);
                DtlList.PurchaseAccountId = (PurchaseAccountId == 0 ? null : PurchaseAccountId);
                DtlList.ReturnPurchaseAccountId = (ReturnPurchaseAccountId == 0 ? null : ReturnPurchaseAccountId);
                DtlList.GoodsInTransitAccountId = (GoodsInTransitAccountId == 0 ? null : GoodsInTransitAccountId);
                DtlList.CostGoodsAccountId = (CostGoodsAccountId == 0 ? null : CostGoodsAccountId);
                DtlList.AdjustAccountId = (AdjustAccountId == 0 ? null : AdjustAccountId);
                DtlList.AllowDiscountAccountId = (AllowDiscountAccountId == 0 ? null : AllowDiscountAccountId);
                DtlList.EarnDiscountAccountId = (EarnDiscountAccountId == 0 ? null : EarnDiscountAccountId);
                DtlList.StoreAccountId = (StoreAccountId == 0 ? null : StoreAccountId);
                DtlList.EarnDiscountAccountId = (EarnDiscountAccountId == 0 ? null : EarnDiscountAccountId);

                MainTbl.Stock_Setting_Accounts.Add(DtlList);

            }


            if (Id == 0)
            {
                //New
                db.Stock_Setting_General.Add(MainTbl);
            }
            else
            {
                //Update
                db.Entry(MainTbl).State = EntityState.Modified;

            }


            db.SaveChanges();
            if (Id == 0)
            {
                return Json(new { Id = MainTbl.Id, MessageType = "success", ReturnMessage = GetMessage(MessageTypes.SaveSuccess) });

            }
            else
            {
                return Json(new { Id = MainTbl.Id, MessageType = "success", ReturnMessage = GetMessage(MessageTypes.ModifaySuccess) });

            }

        }
        #endregion

        #region "C H E C K S E T T I N G B E I N D T Y P E"
        public ActionResult CheckSetting(int Id, int AccountBindMethod)
        {
            try
            {
                Stock_Setting_General Setting_General = new Stock_Setting_General();
                Setting_General = db.Stock_Setting_General.Where(x => x.CompanyId == Id).SingleOrDefault();
                if (Setting_General.AccountBindMethod == AccountBindMethod)
                {
                    return Json(new { AccountBindMethod = Setting_General.AccountBindMethod }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { AccountBindMethod = 0 }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion
     
        #region "T R E E V I E W P R O D U C T G R O UP"
        [Authorize]
        public ActionResult TreeviewProductGroup(int companyId, string Language)
        {
            var tvPG = TVProductGroup(0, "", companyId, Language);
            return Json(new { TreePGDs = tvPG, JsonRequestBehavior.AllowGet });
        }
        public string TVProductGroup(int itemID, string mystr, int CompanyId, string Language)
        {
            if (Language == "Arabic")
            {
                PrepareUserDat();
                List<Stock_ProductGroups> querylist = new List<Stock_ProductGroups>();
                var ctx = new MSDBContext();

                mystr += "[{id:'0',text:'مجموعات المنتجات',HasChildren:true,expanded:true,items:[";

                //querylist = (from m in ctx.Priv_Menus
                //             where m.ParentId == itemID
                //             select m).ToList();

                querylist = (from List in ctx.Stock_ProductGroups.ToList()
                             where List.CompanyId == CompanyId && List.Deleted != true

                             select new Stock_ProductGroups
                             {
                                 Id = List.Id,
                                 NameE = List.NameE,
                                 NameA = List.NameA,
                                 ParentId = List.ParentId,
                                 HasChildren = List.HasChildren,
                                 Notes = List.Notes,
                                 Code = List.Code
                             }

                              ).Where(p => p.ParentId == null).ToList<Stock_ProductGroups>(); ;


                //Below line shows an example of how to make parent node with his child
                //[{ id: "1", text: "P1", items: [{ id: "5", text: "P2" }] }]

                foreach (var item in querylist)
                {
                    var lastparent = querylist.Last();

                    if (item.Equals(lastparent))
                    {
                        mystr += "{id:\"" + item.Id + ",Id:\"" + item.Id + "\",NameA:\"" + item.NameA + "\",HasChildren:" + "false" + ",checked:" + "-false" + ",expanded:" + "false" + ",text:\"" + item.NameA + "\",Code:\"" + item.Code + "\"" + "";
                    }
                    else
                    {
                        mystr += "{id:\"" + item.Id + ",Id:\"" + item.Id + "\",NameA:\"" + item.NameA + "\",HasChildren:" + "false" + ",checked:" + "-false" + ",expanded:" + "false" + ",text:\"" + item.NameA + "\",Code:\"" + item.Code + "\"" + "";
                    }



                    if (item.HasChildren == true)
                    {
                        var childlist = db.Stock_ProductGroups.Where(p => p.ParentId != null & p.ParentId == item.Id && p.Deleted != true).ToList();
                        mystr += ",items:[";
                        foreach (var child in childlist)
                        {
                            var lastchild = childlist.Last();

                            if (child.Equals(lastchild))
                            {
                                if (item.Equals(lastparent))
                                {
                                    //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Sold ? "true" : "false") + "";
                                    mystr += "{id:\"" + child.Id + "\",Id:\"" + child.Id + "\",NameA:\"" + child.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + child.NameA + "\",Code:\"" + child.Code + "\"" + "}]}";
                                }
                                else
                                {
                                    //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Sold ? "true" : "false") + "";
                                    mystr += "{id:\"" + child.Id + "\",Id:\"" + child.Id + "\",NameA:\"" + child.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + child.NameA + "\",Code:\"" + child.Code + "\"" + "}]},";
                                }



                            }
                            else
                            {
                                //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Sold ? "true" : "false") + "";
                                mystr += "{id:\"" + child.Id + "\",Id:\"" + child.Id + "\",NameA:\"" + child.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + child.NameA + "\",Code:\"" + child.Code + "\"" + "},";
                            }


                        }

                    }
                    else
                    {
                        mystr += "},";
                    }





                    #region get product under product group
                    //var childquantity = querylistParent.Count;
                    ////If Parent Has Child again call Treeview with new parameter
                    //if (childquantity > 0)
                    //{
                    //    var last = querylistParent.Last();

                    //    mystr += ",items:[";
                    //    foreach (var item2 in querylistParent)
                    //    {
                    //        if (item2.Equals(last))
                    //        {
                    //            mystr += "{id:\"" + item2.Id + "\",NameA:\"" + item2.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + item2.NameE + "\",Code:\"" + item2.Code + "\"" + "}],";
                    //        }
                    //        else
                    //        {
                    //            //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Sold ? "true" : "false") + "";
                    //            mystr += "{id:\"" + item2.Id + "\",NameA:\"" + item2.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + item2.NameE + "\",Code:\"" + item2.Code + "\"" + "},";
                    //        }
                    //    }


                    //}
                    ////No Child and No Last Node
                    //else if (childquantity == 0 && i < querylist.Count)
                    //{
                    //    mystr += "},";
                    //}

                    #endregion

                }

                mystr += "]}]";
                return mystr;
            }
            else
            {
                PrepareUserDat();
                List<Stock_ProductGroups> querylist = new List<Stock_ProductGroups>();
                var ctx = new MSDBContext();

                mystr += "[{id:'0',text:'Product Group',HasChildren:true,expanded:true,items:[";

                //querylist = (from m in ctx.Priv_Menus
                //             where m.ParentId == itemID
                //             select m).ToList();

                querylist = (from List in ctx.Stock_ProductGroups.ToList()
                             where List.CompanyId == CompanyId     && List.Deleted != true

                             select new Stock_ProductGroups
                             {
                                 Id = List.Id,
                                 NameE = List.NameE,
                                 NameA = List.NameA,
                                 ParentId = List.ParentId,
                                 HasChildren = List.HasChildren,
                                 Notes = List.Notes,
                                 Code = List.Code
                             }

                              ).Where(p => p.ParentId == null).ToList<Stock_ProductGroups>(); ;


                //Below line shows an example of how to make parent node with his child
                //[{ id: "1", text: "P1", items: [{ id: "5", text: "P2" }] }]

                foreach (var item in querylist)
                {
                    var lastparent = querylist.Last();

                    if (item.Equals(lastparent))
                    {
                        mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + item.NameA + "\",HasChildren:" + "false" + ",checked:" + "-false" + ",expanded:" + "false" + ",text:\"" + item.NameE + "\",Code:\"" + item.Code + "\"" + "";
                    }
                    else
                    {
                        mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + item.NameA + "\",HasChildren:" + "false" + ",checked:" + "-false" + ",expanded:" + "false" + ",text:\"" + item.NameE + "\",Code:\"" + item.Code + "\"" + "";
                    }



                    if (item.HasChildren == true)
                    {
                        var childlist = db.Stock_ProductGroups.Where(p => p.ParentId != null & p.ParentId == item.Id && p.Deleted != true).ToList();
                        if (childlist.Count == 0)
                        {
                            mystr += "},";
                        }
                        else { 
                        mystr += ",items:[";
                        foreach (var child in childlist)
                        {
                            var lastchild = childlist.Last();

                            if (child.Equals(lastchild))
                            {
                                if (item.Equals(lastparent))
                                {
                                    //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Sold ? "true" : "false") + "";
                                    mystr += "{id:\"" + child.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + child.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + child.NameE + "\",Code:\"" + child.Code + "\"" + "}]}";
                                }
                                else
                                {
                                    //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Sold ? "true" : "false") + "";
                                    mystr += "{id:\"" + child.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + child.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + child.NameE + "\",Code:\"" + child.Code + "\"" + "}]},";
                                }



                            }
                            else
                            {
                                //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Sold ? "true" : "false") + "";
                                mystr += "{id:\"" + child.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + child.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + child.NameE + "\",Code:\"" + child.Code + "\"" + "},";
                            }


                        }

                    }
                    }
                   





                    #region get product under product group
                    //var childquantity = querylistParent.Count;
                    ////If Parent Has Child again call Treeview with new parameter
                    //if (childquantity > 0)
                    //{
                    //    var last = querylistParent.Last();

                    //    mystr += ",items:[";
                    //    foreach (var item2 in querylistParent)
                    //    {
                    //        if (item2.Equals(last))
                    //        {
                    //            mystr += "{id:\"" + item2.Id + "\",NameA:\"" + item2.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + item2.NameE + "\",Code:\"" + item2.Code + "\"" + "}],";
                    //        }
                    //        else
                    //        {
                    //            //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Sold ? "true" : "false") + "";
                    //            mystr += "{id:\"" + item2.Id + "\",NameA:\"" + item2.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + item2.NameE + "\",Code:\"" + item2.Code + "\"" + "},";
                    //        }
                    //    }


                    //}
                    ////No Child and No Last Node
                    //else if (childquantity == 0 && i < querylist.Count)
                    //{
                    //    mystr += "},";
                    //}

                    #endregion

                }

                mystr += "]}]";
                return mystr;
            }

        }
        #endregion

        #region "T R E E V I E W W A R E H O U S E"
        [Authorize]
        public ActionResult TreeviewWarehouse(int CompanyId, string Language)
        {

            var tvWh = TVWarehouse("", CompanyId, Language);
           
            return Json(new { TreeWH = tvWh, JsonRequestBehavior.AllowGet });

        }

        public string TVWarehouse(string mystr, int CompanyId, string Language)
        {
            if (Language == "Arabic")
            {
                PrepareUserDat();
                List<Stock_Warehouses> querylist = new List<Stock_Warehouses>();
                var ctx = new MSDBContext();


                querylist = ctx.Stock_Warehouses.Where(sw => sw.CompanyId == CompanyId && sw.Deleted != true).ToList();

                mystr += "[{id:'0',text:'المخازن',HasChildren:true,expanded:true,items:[";

                //mystr += "[";
                //get parent menus 
                querylist = ctx.Stock_Warehouses.Where(sw => sw.CompanyId == CompanyId && sw.Deleted != true).ToList();




                foreach (var item in querylist)
                {

                    var lastparent = querylist.Last();

                    if (item.Equals(lastparent))
                    {
                        //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Open ? "true" : "false") + "";
                        mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + item.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + item.NameA + "\",Code:\"" + item.Code + "\"" + "}";


                    }
                    else
                    {
                        //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Open ? "true" : "false") + "";
                        mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + item.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + item.NameA + "\",Code:\"" + item.Code + "\"" + "},";


                    }
                

                }

                mystr += "]}]";
                return mystr;
            }
            else
            {
                PrepareUserDat();
                List<Stock_Warehouses> querylist = new List<Stock_Warehouses>();
                var ctx = new MSDBContext();


                querylist = ctx.Stock_Warehouses.Where(sw => sw.CompanyId == CompanyId && sw.Deleted != true).ToList();

                mystr += "[{id:'0',text:'Warehouses',HasChildren:true,expanded:true,items:[";

                //mystr += "[";
                //get parent menus 
                querylist = ctx.Stock_Warehouses.Where(sw => sw.CompanyId == CompanyId && sw.Deleted != true).ToList();




                foreach (var item in querylist)
                {

                     var lastparent = querylist.Last();

                     if (item.Equals(lastparent))
                     {

                         //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Open ? "true" : "false") + "";
                         mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + item.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + item.NameE + "\",Code:\"" + item.Code + "\"" + "}";

                     }
                     else
                     {
                         //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Open ? "true" : "false") + "";
                         mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + item.NameA + "\",HasChildren:" + "false" + ",checked:" + "false" + ",expanded:" + "false" + ",text:\"" + item.NameE + "\",Code:\"" + item.Code + "\"" + "},";

                     }


                }

                mystr += "]}]";
                return mystr;
            }

        }

        #endregion

        #region "D E L E T E S E T T I N G"
             [Authorize]
        public ActionResult Delete(int Id)
        {
            PrepareUserDat();
            if (!CheckUserPermission(PermissionType.Delete)) return Json(new { MessageType = "danger", ReturnMessage = GetMessage(MessageTypes.NoPermission)});

            //prevent delet parent  sub childeren
            if (Id != 0)
            {
                Stock_Setting_General mm = db.Stock_Setting_General.Where(t => t.Id == Id && t.Deleted != true).FirstOrDefault();
                if (mm != null)
                {
                    if (mm.Id != null)
                    {

                        return Json(new { MessageType = "danger", ReturnMessage = GetMessage(MessageTypes.HasChildern) });
                    }
                }
            }

            if (Id == 0)
            {
                //Message no data to delete
                return Json(new { MessageType = "info", ReturnMessage = GetMessage(MessageTypes.NoData) });
            }

            Stock_Setting_General MainTbl = db.Stock_Setting_General.Find(Id);



            if (MainTbl.Post == true)
            {
                //Message for postiong
                return Json(new { MessageType = "info", ReturnMessage = GetMessage(MessageTypes.DeleteWhenPost) });
            }


            db.Entry(MainTbl).State = EntityState.Modified;


            MainTbl.Deleted = true;
            MainTbl.Delete_Uid = UserId;
            MainTbl.Delete_Date = DateTime.Now;

            db.SaveChanges();


            return Json(new { Id = Id, MessageType = "success", ReturnMessage = GetMessage(MessageTypes.DeleteSuccess) });


        }
        #endregion

        #region "P O S T S E T T I N G"
             [Authorize]
             public ActionResult Post(int Id)
             {
                 PrepareUserDat();
                 if (Id == 0)
                 {
                     //Message no data to delete
                     return Json(new { MessageType = "info", ReturnMessage = GetMessage(MessageTypes.NoData) });
                 }

                 Stock_Setting_General MainTbl = db.Stock_Setting_General.Find(Id);
                 if (!CheckUserPermission((MainTbl.Post == true ? PermissionType.UnPost : PermissionType.Post))) return Json(new { MessageType = GetMessage(MessageTypes.NoPermission)});


                 if (MainTbl.Post == true)
                 {
                     //UnPost
                     db.Entry(MainTbl).State = EntityState.Modified;


                     MainTbl.Post = false;
                     MainTbl.Post_Uid = UserId;
                     MainTbl.Post_Date = DateTime.Now;

                     db.SaveChanges();
                     MemberShipId = MemberShipId;


                     return Json(new { Id = Id, MessageType = "success", ReturnMessage = GetMessage(MessageTypes.UnPostSuccess) });

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


                     return Json(new { Id = Id, MessageType = "success", ReturnMessage = GetMessage(MessageTypes.PostSuccess) });

                 }


             }
       
             #endregion

        #region "R E A D"
            

             [Authorize]
             public ActionResult Read(int Id,int CompanyId , int BranchId)
             {
                 PrepareUserDat();

                 Stock_Setting_General MainTbl = new Stock_Setting_General();
                 List<Stock_Setting_Accounts> TBLDtlList = new List<Stock_Setting_Accounts>();

                 MainTbl = db.Stock_Setting_General.Find(Id);
                 MainTbl.Stock_Setting_Accounts = new List<Stock_Setting_Accounts>();
                 TBLDtlList = db.Stock_Setting_Accounts.Where(p => p.MasterId == MainTbl.Id).ToList();


                 return Json(new { TBL = MainTbl, TBLDtl = TBLDtlList });
             }
        
       #endregion

        public ActionResult TreeViewChanged(int SelectedId, int GroupId)
        {
            PrepareUserDat();
            Stock_Warehouses Tbl = new Stock_Warehouses();

            var DetailId = db.Stock_Warehouses.Select(c => c.Id).FirstOrDefault();

            if (DetailId != 0)
            {
                Tbl = db.Stock_Warehouses.Find(DetailId);
            }
            return Json(Tbl, JsonRequestBehavior.AllowGet);


        }




        #region ReadWarehouse
        //[Authorize]
        //public ActionResult ReadWarehouse(int Id, int ReadType)
        //{
        //    PrepareUserDat();

        //    Stock_Warehouses WarehouseTbl = new Stock_Warehouses();

        //    if (ReadType == Convert.ToInt16(ReadTypes.Empty))
        //    {
        //        //Leave Table empty
        //    }
        //    else if (ReadType == Convert.ToInt16(ReadTypes.Current))
        //    {
        //        //Read Current

        //        if (Id == 0 || Id == null)
        //        {
        //            WarehouseTbl = new Stock_Warehouses();
        //        }
        //        else
        //        {
        //            WarehouseTbl = db.Stock_Warehouses.Find(Id);
        //        }
        //    }
        //    else if (ReadType == Convert.ToInt16(ReadTypes.Next))
        //    {
        //        //Read Next
        //        Id = (db.Stock_Warehouses.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true && c.Id > Id).Min(c => (int?)c.Id) ?? 0);


        //        if (Id == 0)
        //        {
        //            Id = (db.Stock_Warehouses.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);

        //            WarehouseTbl = db.Stock_Warehouses.Find(Id);
        //        }
        //        else
        //        {
        //            WarehouseTbl = db.Stock_Warehouses.Find(Id);
        //        }

        //    }

        //    else if (ReadType == Convert.ToInt16(ReadTypes.Previous))
        //    {
        //        //Read Previous
        //        Id = (db.Stock_Warehouses.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true && c.Id < Id).Max(c => (int?)c.Id) ?? 0);
        //        if (Id == 0)
        //        {
        //            Id = (db.Priv_Groups.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
        //            WarehouseTbl = db.Stock_Warehouses.Find(Id);
        //        }
        //        else
        //        {
        //            WarehouseTbl = db.Stock_Warehouses.Find(Id);
        //        }

        //    }
        //    else if (ReadType == Convert.ToInt16(ReadTypes.Last))
        //    {
        //        //Read Last
        //        Id = (db.Stock_Warehouses.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);
        //        if (Id == 0 || Id == null)
        //        {
        //            WarehouseTbl = new Stock_Warehouses();
        //        }
        //        else
        //        {
        //            WarehouseTbl = db.Stock_Warehouses.Find(Id);
        //        }

        //    }

        //    else if (ReadType == Convert.ToInt16(ReadTypes.First))
        //    {
        //        //Read First
        //        Id = (db.Stock_Warehouses.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
        //        if (Id == 0 || Id == null)
        //        {
        //            WarehouseTbl = new Stock_Warehouses();
        //        }
        //        else
        //        {
        //            WarehouseTbl = db.Stock_Warehouses.Find(Id);
        //        }
        //    }

        //    //Read Related Detail
        //    string TreeDS = TreeviewWarehouse(0, "", 0, 0, Id);


        //    return Json(new { Id = WarehouseTbl.Id, Code = WarehouseTbl.Code, NameA = WarehouseTbl.NameA, NameE = WarehouseTbl.NameE, TreeDS = TreeDS });

        //}

        //public ActionResult ReadProductGroup(int Id, int ReadType)
        //{
        //    PrepareUserDat();

        //    Stock_ProductGroups ProductGroupTbl = new Stock_ProductGroups();

        //    if (ReadType == Convert.ToInt16(ReadTypes.Empty))
        //    {
        //        //Leave Table empty
        //    }
        //    else if (ReadType == Convert.ToInt16(ReadTypes.Current))
        //    {
        //        //Read Current

        //        if (Id == 0 || Id == null)
        //        {
        //            ProductGroupTbl = new Stock_ProductGroups();
        //        }
        //        else
        //        {
        //            ProductGroupTbl = db.Stock_ProductGroups.Find(Id);
        //        }
        //    }
        //    else if (ReadType == Convert.ToInt16(ReadTypes.Next))
        //    {
        //        //Read Next
        //        Id = (db.Stock_ProductGroups.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true && c.Id > Id).Min(c => (int?)c.Id) ?? 0);


        //        if (Id == 0)
        //        {
        //            Id = (db.Stock_ProductGroups.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);

        //            ProductGroupTbl = db.Stock_ProductGroups.Find(Id);
        //        }
        //        else
        //        {
        //            ProductGroupTbl = db.Stock_ProductGroups.Find(Id);
        //        }

        //    }

        //    else if (ReadType == Convert.ToInt16(ReadTypes.Previous))
        //    {
        //        //Read Previous
        //        Id = (db.Stock_ProductGroups.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true && c.Id < Id).Max(c => (int?)c.Id) ?? 0);
        //        if (Id == 0)
        //        {
        //            Id = (db.Priv_Groups.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
        //            ProductGroupTbl = db.Stock_ProductGroups.Find(Id);
        //        }
        //        else
        //        {
        //            ProductGroupTbl = db.Stock_ProductGroups.Find(Id);
        //        }

        //    }
        //    else if (ReadType == Convert.ToInt16(ReadTypes.Last))
        //    {
        //        //Read Last
        //        Id = (db.Stock_Warehouses.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true).Max(c => (int?)c.Id) ?? 0);
        //        if (Id == 0 || Id == null)
        //        {
        //            ProductGroupTbl = new Stock_ProductGroups();
        //        }
        //        else
        //        {
        //            ProductGroupTbl = db.Stock_ProductGroups.Find(Id);
        //        }

        //    }

        //    else if (ReadType == Convert.ToInt16(ReadTypes.First))
        //    {
        //        //Read First
        //        Id = (db.Stock_ProductGroups.Where(c => c.MemberShipId == MemberShipId && c.Deleted != true).Min(c => (int?)c.Id) ?? 0);
        //        if (Id == 0 || Id == null)
        //        {
        //            ProductGroupTbl = new Stock_ProductGroups();
        //        }
        //        else
        //        {
        //            ProductGroupTbl = db.Stock_ProductGroups.Find(Id);
        //        }
        //    }

        //    //Read Related Detail
        //    string TreeDS = TreeviewProductGroup(0, "", 0, 0, Id);


        //    return Json(new { Id = ProductGroupTbl.Id, Code = ProductGroupTbl.Code, NameA = ProductGroupTbl.NameA, NameE = ProductGroupTbl.NameE, TreeDS = TreeDS });

        //}
        #endregion

        #region LoadDetialTree
        //public ActionResult LoadDetailTree([DataSourceRequest]DataSourceRequest request, int Id)
        //{
        //    PrepareUserDat();
        //    ViewData["TVDS"] = TreeviewWarehouse(0, "", 0, 0, Id);
        //    return Json("");

        //}
        #endregion      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



    }



    // model  ware house to serialize the ware house
    #region "Warehouse model"
    public class WareHouseModel
    {

        public class Item
        {
            public int Id { get; set; }
            public string NameA { get; set; }
            public bool HasChildren { get; set; }
            public bool @checked { get; set; }
            public bool expanded { get; set; }
            public string text { get; set; }
            public string Code { get; set; }
            public int index { get; set; }
        }

        public class RootObject
        {
            public int Id { get; set; }
            public string text { get; set; }
            public bool HasChildren { get; set; }
            public bool expanded { get; set; }
            public List<Item> items { get; set; }
            public int index { get; set; }
            public bool @checked { get; set; }
        }
    }

    #endregion
    

}