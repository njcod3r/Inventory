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
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Data.SqlClient;
using MVC_ERP.Controllers.General;



namespace MVC_ERP.Controllers
{
    public class Priv_ProgrammerController : BaseController
    {
        
        private MSDBContext db = new MSDBContext();
        int UserId,MemberShipId;
        object  Deleted = false;

         [Authorize]
        public ActionResult Index(int? Id, string submitButton, Priv_Menus MainTbl, string[] checkedItems)
        {
            PrepareUserDat();
           int? CC;
           
           switch(submitButton) {

            case "Save":

              //Check if  new insert under record has parent 
             
               //New or edit according Id pass if 0 new else edit
               if( ModelState.IsValid)
               {
                  
               }
               //Model not valid
               return View(MainTbl);

            case "Discard":
               //Cancel return to Index view

                   return RedirectToAction("Index");

            case "Delete":

                   
                  

               //Delete return to previous
                   if (MainTbl.Id == null || MainTbl.Id == 0)
               {
                   //Message no data to delete
                   TempData["MessageType"] = "info";
                   TempData["ReturnMessage"] = "No data to delete";
                   return View(MainTbl);
               }
               if (ModelState.IsValid)
               {
                 RouteData.Values.Remove("Id");
               
                 {
              
                     return RedirectToAction("Index");
                 }
               }
               //Model not valid
               return View(MainTbl);

            case "Reuse":

               if (MainTbl.Id == null || MainTbl.Id == 0)
               {
                   //Message no data to delete
                   TempData["MessageType"] = "info";
                   TempData["ReturnMessage"] = "No data to reuse";
                   return View(MainTbl);
               }
               //New or edit according Id pass if 0 new else edit
               if (ModelState.IsValid)
               {
                   RouteData.Values.Remove("Id");
                

               }
               //Model not valid
               return View(MainTbl);

            case "Post":
               if (MainTbl.Id == null || MainTbl.Id == 0)
               {
                   //Message no data to delete
                   TempData["MessageType"] = "info";
                   TempData["ReturnMessage"] = "No data to post";
                   return View(MainTbl);
               }
               //Delete return to previous
               if (ModelState.IsValid)
               {
                   RouteData.Values.Remove("Id");
                
               }
               //Model not valid
               return View(MainTbl);

 
            default:

               //Read function
 
                       
               ViewData["TVDS"] = Treeview(0, "", 0, 0);
            


               return View(MainTbl);
              }



       }

        // Insert Function
         [Authorize]
        public ActionResult Insert(string xmlNodes) 
            {
                PrepareUserDat();
                //List<string> h = xmlNodes;
                //var states = (JObject)xmlNodes;
                string m = "{items:" + xmlNodes  + "}";
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();

                //object Menue = serializer1.Deserialize(xmlNodes, typeof(JObject));


                XmlDocument xml = JsonConvert.DeserializeXmlNode(m);
                XmlReader xr = new XmlNodeReader(xml);

                DataSet ds = new DataSet(xmlNodes);
                ds.ReadXml(xr);

                DataTable TblMenue = ds.Tables[0];
                TblMenue.Columns["checked"].ColumnName = "Checked";
              
                TblMenue.Columns.Add("Sold", typeof(Boolean), "Checked");

                TblMenue.Rows[0]["ParentId"] = null;
                TblMenue.Rows[0]["Checked"] = true;

                TblMenue.Columns.Remove("text");

                TblMenue.ChildRelations.Clear();
                TblMenue.ParentRelations.Clear();
                TblMenue.Constraints.Remove("items_items");
                TblMenue.Constraints.Clear();
                TblMenue.PrimaryKey = null;
                TblMenue.Columns.Remove("index");
                TblMenue.Columns.Remove("items_Id");
                TblMenue.Columns.Remove("items_Id_0");
                if (TblMenue.Columns.Contains("selected"))
                {
                    TblMenue.Columns.Remove("selected");
                }

               using (SqlBulkCopy s = new SqlBulkCopy(db.Database.Connection.ConnectionString)) 
                {
                    db.Database.ExecuteSqlCommand("delete from Priv_Menus");
                    db.SaveChanges();

                    s.DestinationTableName = "Priv_Menus";
                    s.WriteToServer(TblMenue); 
                    s.Close(); 

                   //Sold all parent that have any checked items
                    db.Database.ExecuteSqlCommand("UPDATE  Priv_Menus SET  Sold = 1 WHERE (Id IN  (SELECT ParentId FROM  Priv_Menus AS Priv_Menus_1  WHERE (HasChildren = 0) AND (Sold = 1)  GROUP BY ParentId))");
                    db.Database.ExecuteSqlCommand("UPDATE  Priv_Menus SET  Sold = 1 WHERE (Id IN  (SELECT ParentId FROM  Priv_Menus AS Priv_Menus_2  WHERE (Id IN  (SELECT  ParentId FROM   Priv_Menus AS Priv_Menus_1 WHERE  (HasChildren = 0) AND (Sold = 1)GROUP BY ParentId))))");
                    db.Database.ExecuteSqlCommand("UPDATE  Priv_Menus SET  Expanded = 1 WHERE Id=1000");
                    db.Database.ExecuteSqlCommand("UPDATE  Priv_Menus SET  Expanded = 0 WHERE Id<>1000");
             
               } 

                   
                
                    return Json("");

                    //Implment it in java script
                    //TempData["MessageType"] = "success";
                    //TempData["ReturnMessage"] = "Save successful";

                    //return RedirectToAction("Index");
                

           }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search([DataSourceRequest]DataSourceRequest request, string TxtSearchVal, int? CompanySearchId)
        {
            TempData["SelectedID"] = CompanySearchId;
            MemberShipId = Convert.ToInt32(Session["MemberShipId"]);
           if (TxtSearchVal !=null)
           {
               var m = db.Priv_Menus.Where(b => b.NameE.Contains(TxtSearchVal)).Take(20);
             IQueryable<Priv_Menus> mm = m;
             DataSourceResult result = mm.ToDataSourceResult(request);
             return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);
           }
           else
           {
               var m = db.Priv_Menus.OrderByDescending(b => b.Id).Where(b => b.NameE.Contains(TxtSearchVal)).Take(20);
               IQueryable<Priv_Menus> mm = m;
               DataSourceResult result = mm.ToDataSourceResult(request);
               return Json(result, "text/x-json", JsonRequestBehavior.AllowGet);

           }
          
            //}
        }


        int mainNode = 0;
        int childquantity = 0;
        int myflag;
        public string Treeview(int itemID, string mystr, int j, int flag)
        {
         
         
           List<Priv_Menus> querylist = new List<Priv_Menus>();
            var ctx = new MSDBContext();

            if (flag == 0)
            {

                //querylist = (from m in ctx.Priv_Menus
                //             where m.ParentId == null
                //             select m).ToList();


                //Join static list menus with real menus in database
                querylist = (from List in StaticLists.AllMenueList.ToList() join
                                  DBase in ctx.Priv_Menus.ToList() on List.Id equals DBase.Id into output
                             from Real in output.DefaultIfEmpty()

                             select new Priv_Menus
                                      {
                                          Id = List.Id,
                                          NameE = List.NameE,
                                          NameA = List.NameA,
                                          ParentId = List.ParentId,
                                          Expanded = List.Expanded,
                                          HasChildren = List.HasChildren,
                                          Sold = (Real == null ? false : Real.Sold),
                                          UrlLink = List.UrlLink,
                                          ImageUrlLink = List.ImageUrlLink,
                                          Target = List.Target,
                                          Notes = List.Notes,
                                          OnlyForProgrammer = List.OnlyForProgrammer,
                                      }

                             ).Where(List => List.ParentId == null).ToList<Priv_Menus>(); ;

        

                mainNode = querylist.Count;

                mystr += "[";
            }
            if (flag == 1)
            {

                //querylist = (from m in ctx.Priv_Menus
                //             where m.ParentId == itemID
                //             select m).ToList();

                querylist = (from List in StaticLists.AllMenueList.ToList()
                             join
                                 DBase in ctx.Priv_Menus.ToList() on List.Id equals DBase.Id into output
                             from Real in output.DefaultIfEmpty()

                             select new Priv_Menus
                             {
                                 Id = List.Id,
                                 NameE = List.NameE,
                                 NameA = List.NameA,
                                 ParentId = List.ParentId,
                                 Expanded = List.Expanded,
                                 HasChildren = List.HasChildren,
                                 Sold = (Real == null ? false : Real.Sold),
                                 UrlLink = List.UrlLink,
                                 ImageUrlLink = List.ImageUrlLink,
                                 Target = List.Target,
                                 Notes = List.Notes,
                                 OnlyForProgrammer = List.OnlyForProgrammer,
                             }

                           ).Where(List => List.ParentId == itemID).ToList<Priv_Menus>(); 

                mainNode = querylist.Count;
                mystr += ",items:[";
            }

            //Below line shows an example of how to make parent node with his child
            //[{ id: "1", text: "P1", items: [{ id: "5", text: "P2" }] }]


            int i = 1;
            foreach (var item in querylist)
            {
                myflag = 0;
                //mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",HasChildren:\"" + item.HasChildren + "\",NameE:\"" + item.NameE + "\",NameA:\"" + item.NameA + "\",text:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",expanded:" + (item.Expanded ? "true" : "false") + ",dataUrlField:\"" + "Home/Index" + "\",checked:" + (item.Sold ? "true" : "false") + "";
                mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + item.NameA + "\",NameE:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",checked:" + (item.HasChildren ? "false" : (item.Sold? "true" :"false")) + ",expanded:" + (item.Expanded ? "true" : "false") + "" + ",HasChildren:" + (item.HasChildren ? "true" : "false") + ",text:\"" + item.NameE + "\",UrlLink:\"" + item.UrlLink + "\",ImageUrlLink:\"" + item.ImageUrlLink + "\",Target:\"" + item.Target + "\",HtmlAttributes:\"" + item.HtmlAttributes + "\",OnlyForProgrammer:" + (item.OnlyForProgrammer ? "true" : "false") + ",Notes:\"" + item.Notes + "\"";

                List<Priv_Menus> querylistParent = new List<Priv_Menus>();
                //Check this parent has child or not , if yes how many?

                //querylistParent = (from m in ctx.Priv_Menus
                //                   where m.ParentId == item.Id
                //                   select m).ToList();

                querylistParent = (from List in StaticLists.AllMenueList.ToList()
                             join
                                 DBase in ctx.Priv_Menus.ToList() on List.Id equals DBase.Id into output
                             from Real in output.DefaultIfEmpty()

                             select new Priv_Menus
                             {
                                 Id = List.Id,
                                 NameE = List.NameE,
                                 NameA = List.NameA,
                                 ParentId = List.ParentId,
                                 Expanded = List.Expanded,
                                 HasChildren = List.HasChildren,
                                 Sold = (Real == null ? false : Real.Sold),
                                 UrlLink = List.UrlLink,
                                 ImageUrlLink = List.ImageUrlLink,
                                 Target = List.Target,
                                 Notes = List.Notes,
                                 OnlyForProgrammer = List.OnlyForProgrammer,
                             }

                       ).Where(List => List.ParentId == item.Id).ToList<Priv_Menus>(); 


                childquantity = querylistParent.Count;
                //If Parent Has Child again call Treeview with new parameter
                if (childquantity > 0)
                {
                    mystr = Treeview(item.Id, mystr, i, 1);

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

                        //parentquery = (from m in ctx.Priv_Menus
                        //               where m.Id == currentparent
                        //               select m).ToList();

                        parentquery = (from List in StaticLists.AllMenueList.ToList()
                                       join
                                           DBase in ctx.Priv_Menus.ToList() on List.Id equals DBase.Id into output
                                       from Real in output.DefaultIfEmpty()

                                       select new Priv_Menus
                                       {
                                           Id = List.Id,
                                           NameE = List.NameE,
                                           NameA = List.NameA,
                                           ParentId = List.ParentId,
                                           Expanded = List.Expanded,
                                           HasChildren = List.HasChildren,
                                           Sold = (Real == null ? false : Real.Sold),
                                           UrlLink = List.UrlLink,
                                           ImageUrlLink = List.ImageUrlLink,
                                           Target = List.Target,
                                           Notes = List.Notes,
                                           OnlyForProgrammer = List.OnlyForProgrammer,
                                       }

                    ).Where(List => List.Id == currentparent).ToList<Priv_Menus>(); 


                        var rep2 = (from h in parentquery select new { h.ParentId }).First();

                        //put {[ up to end

                        //list of child
                        childlistquery = new List<Priv_Menus>();

                        //childlistquery = (from m in ctx.Priv_Menus
                        //                  where m.ParentId == currentparent
                        //                  select m).ToList();

                        childlistquery = (from List in StaticLists.AllMenueList.ToList()
                                       join
                                           DBase in ctx.Priv_Menus.ToList() on List.Id equals DBase.Id into output
                                       from Real in output.DefaultIfEmpty()

                                       select new Priv_Menus
                                       {
                                           Id = List.Id,
                                           NameE = List.NameE,
                                           NameA = List.NameA,
                                           ParentId = List.ParentId,
                                           Expanded = List.Expanded,
                                           HasChildren = List.HasChildren,
                                           Sold = (Real == null ? false : Real.Sold),
                                           UrlLink = List.UrlLink,
                                           ImageUrlLink = List.ImageUrlLink,
                                           Target = List.Target,
                                           Notes = List.Notes,
                                           OnlyForProgrammer = List.OnlyForProgrammer,
                                       }

                  ).Where(List => List.ParentId == currentparent).ToList<Priv_Menus>(); 


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

                    lastchild = (from List in StaticLists.AllMenueList.ToList()
                                      join
                                          DBase in ctx.Priv_Menus.ToList() on List.Id equals DBase.Id into output
                                      from Real in output.DefaultIfEmpty()

                                      select new Priv_Menus
                                      {
                                          Id = List.Id,
                                          NameE = List.NameE,
                                          NameA = List.NameA,
                                          ParentId = List.ParentId,
                                          Expanded = List.Expanded,
                                          HasChildren = List.HasChildren,
                                          Sold = (Real == null ? false : Real.Sold),
                                          UrlLink = List.UrlLink,
                                          ImageUrlLink = List.ImageUrlLink,
                                          Target = List.Target,
                                          Notes = List.Notes,
                                          OnlyForProgrammer = List.OnlyForProgrammer,
                                      }

               ).Where(List => List.ParentId == item.ParentId).ToList<Priv_Menus>(); 


                    List<Priv_Menus> lastparent = new List<Priv_Menus>();

                    //lastparent = (from m in ctx.Priv_Menus
                    //              where m.ParentId == null
                    //              select m).ToList();

                    lastparent = (from List in StaticLists.AllMenueList.ToList()
                                 join
                                     DBase in ctx.Priv_Menus.ToList() on List.Id equals DBase.Id into output
                                 from Real in output.DefaultIfEmpty()

                                 select new Priv_Menus
                                 {
                                     Id = List.Id,
                                     NameE = List.NameE,
                                     NameA = List.NameA,
                                     ParentId = List.ParentId,
                                     Expanded = List.Expanded,
                                     HasChildren = List.HasChildren,
                                     Sold = (Real == null ? false : Real.Sold),
                                     UrlLink = List.UrlLink,
                                     ImageUrlLink = List.ImageUrlLink,
                                     Target = List.Target,
                                     Notes = List.Notes,
                                     OnlyForProgrammer = List.OnlyForProgrammer,
                                 }

             ).Where(List => List.ParentId == null).ToList<Priv_Menus>(); 


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
   
        

    }
}
