using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVC_ERP.Models;
using Kendo.Mvc.Extensions;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Globalization;
using MVC_ERP.Controllers.General;

namespace MVC_ERP.Controllers

    
{
    public class HomeController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-EG");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-EG");

            int UserId = Convert.ToInt32(User.Identity.GetUserId());
            string UserName=User.Identity.GetUserName();
            if (UserId == -698245 && UserName == "Programmers")
            {
                ViewData["TVDS"] = TreeviewProgrammers(0, "", 0, 0);
            }
            else if (UserId == -698246 && UserName == "System admin")
            {
                ViewData["TVDS"] = TreeviewAdmin(0, "", 0, 0);
            }
            else
            {
                ViewData["TVDS"] = Treeview(0, "", 0, 0, UserId);

            }
            return View();
        }

         public ActionResult Default()
        {
            return View();
        }
         public ActionResult ChangeLanguage(string Src)
         {
             if (Request.Cookies["Language"].Value == Language.English)
             {
                 Response.Cookies.Set(new HttpCookie("Language", Language.Arabic));
                 
             }
             else if (Request.Cookies["Language"].Value == Language.Arabic)
             {
                 Response.Cookies.Set(new HttpCookie("Language", Language.English));

             }

             TempData["IframeSRC"] = Src;
             //reload last requested page with new culture 
             return RedirectToAction("Index");
         }


        int mainNode = 0;
        int childquantity = 0;
        int myflag;

        public string Treeview(int itemID, string mystr, int j, int flag, int UserId)
        {


            List<Priv_Menus> querylist = new List<Priv_Menus>();
            var ctx = new MSDBContext();

            var GroupIds = ctx.Priv_Users_Groups.Where(c => c.UserId == UserId).Select(c => c.GroupId);
            var ParentMenuIds = (from List in ctx.Priv_Menus
                                 join DBase in ctx.Priv_Groups_Rights on List.Id equals DBase.MenuId
                                 where DBase.Open == true && GroupIds.Contains(DBase.GroupId)
                                 select List).GroupBy(x => x.ParentId).Select(x => x.FirstOrDefault()).Select(x => x.ParentId);
      

            if (flag == 0)
            {

                //Join static list menus with real menus in database
                //var ids= new[] { 1, 2, 3, 4 };
                    
                querylist = (from List in ctx.Priv_Menus.Where(List=>List.Sold==true && List.OnlyForProgrammer !=true ).ToList()
                             join
                             DBase in ctx.Priv_Groups_Rights.Where(d => GroupIds.Contains(d.GroupId) && d.Open == true) on List.Id equals DBase.MenuId
                             select List)
                             //Add Parent like( Menue,System.General Ledger) because it not open in Priv_Groups_Rights we dont open parent because showing in treeview 
                             //will not be correct if open in parent true if parent true treeview make all children true even if it false in database
                            .Union(ctx.Priv_Menus.Where(c => ParentMenuIds.Contains(c.Id) || c.Id==1000)
                            )
                            .GroupBy(x=>x.Id).Select(y=>y.FirstOrDefault()).Where(List => List.ParentId == null).ToList<Priv_Menus>();



                mainNode = querylist.Count;

                mystr += "[";
            }
            if (flag == 1)
            {


                querylist = (from List in ctx.Priv_Menus.Where(List => List.Sold == true && List.OnlyForProgrammer != true).ToList()
                             join
                             DBase in ctx.Priv_Groups_Rights.Where(d => GroupIds.Contains(d.GroupId) && d.Open == true) on List.Id equals DBase.MenuId
                             select List)
                            //Add Parent like( Menue,System.General Ledger) because it not open in Priv_Groups_Rights we dont open parent because showing in treeview 
                            //will not be correct if open in parent true if parent true treeview make all children true even if it false in database
                            .Union(ctx.Priv_Menus.Where(c => ParentMenuIds.Contains(c.Id) || c.Id == 1000)
                            )
                            .GroupBy(x => x.Id).Select(y => y.FirstOrDefault()).Where(List => List.ParentId == itemID).ToList<Priv_Menus>();
                    
                
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
                mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + item.NameA + "\",NameE:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",checked:" + (item.HasChildren ? "false" : (item.Sold ? "true" : "false")) + ",expanded:" + (item.Expanded ? "true" : "false") + "" + ",HasChildren:" + (item.HasChildren ? "true" : "false") + ",text:\"" + (Request.Cookies["Language"].Value == Language.English ? item.NameE : item.NameA) + "\",UrlLink:\"" + item.UrlLink + "\",ImageUrlLink:\"" + item.ImageUrlLink + "\",Target:\"" + item.Target + "\",HtmlAttributes:\"" + item.HtmlAttributes + "\",OnlyForProgrammer:" + (item.OnlyForProgrammer ? "true" : "false") + ",Notes:\"" + item.Notes + "\"";

                List<Priv_Menus> querylistParent = new List<Priv_Menus>();
                //Check this parent has child or not , if yes how many?



                querylistParent = (from List in ctx.Priv_Menus.Where(List => List.Sold == true && List.OnlyForProgrammer != true).ToList()
                                   join
                                   DBase in ctx.Priv_Groups_Rights.Where(d => GroupIds.Contains(d.GroupId) && d.Open == true) on List.Id equals DBase.MenuId
                                   select List)
                                  //Add Parent like( Menue,System.General Ledger) because it not open in Priv_Groups_Rights we dont open parent because showing in treeview 
                                   //will not be correct if open in parent true if parent true treeview make all children true even if it false in database
                                  .Union(ctx.Priv_Menus.Where(c => ParentMenuIds.Contains(c.Id) || c.Id == 1000)
                                   )
                                  .GroupBy(x => x.Id).Select(y => y.FirstOrDefault()).Where(List => List.ParentId == item.Id).ToList<Priv_Menus>();

                  

                childquantity = querylistParent.Count;
                //If Parent Has Child again call Treeview with new parameter
                if (childquantity > 0)
                {
                    mystr = Treeview(item.Id, mystr, i, 1,UserId);

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



                        parentquery = (from List in ctx.Priv_Menus.Where(List => List.Sold == true && List.OnlyForProgrammer != true).ToList()
                                       join
                                       DBase in ctx.Priv_Groups_Rights.Where(d => GroupIds.Contains(d.GroupId) && d.Open == true) on List.Id equals DBase.MenuId
                                       select List)
                                       //Add Parent like( Menue,System.General Ledger) because it not open in Priv_Groups_Rights we dont open parent because showing in treeview 
                                       //will not be correct if open in parent true if parent true treeview make all children true even if it false in database
                                      .Union(ctx.Priv_Menus.Where(c => ParentMenuIds.Contains(c.Id) || c.Id == 1000)
                                       )
                                      .GroupBy(x => x.Id).Select(y => y.FirstOrDefault()).Where(List => List.Id == currentparent).ToList<Priv_Menus>();

               


                        var rep2 = (from h in parentquery select new { h.ParentId }).First();

                        //put {[ up to end

                        //list of child
                        childlistquery = new List<Priv_Menus>();



                        childlistquery = (from List in ctx.Priv_Menus.Where(List => List.Sold == true && List.OnlyForProgrammer != true).ToList()
                                          join
                                          DBase in ctx.Priv_Groups_Rights.Where(d => GroupIds.Contains(d.GroupId) && d.Open == true) on List.Id equals DBase.MenuId
                                          select List)
                                          //Add Parent like( Menue,System.General Ledger) because it not open in Priv_Groups_Rights we dont open parent because showing in treeview 
                                          //will not be correct if open in parent true if parent true treeview make all children true even if it false in database
                                         .Union(ctx.Priv_Menus.Where(c => ParentMenuIds.Contains(c.Id) || c.Id == 1000)
                                         )
                                         .GroupBy(x => x.Id).Select(y => y.FirstOrDefault()).Where(List => List.ParentId == currentparent).ToList<Priv_Menus>();
 
                            
                         


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



                    lastchild = (from List in ctx.Priv_Menus.Where(List => List.Sold == true && List.OnlyForProgrammer != true).ToList()
                                 join
                                 DBase in ctx.Priv_Groups_Rights.Where(d => GroupIds.Contains(d.GroupId) && d.Open == true) on List.Id equals DBase.MenuId
                                 select List)
                                //Add Parent like( Menue,System.General Ledger) because it not open in Priv_Groups_Rights we dont open parent because showing in treeview 
                                //will not be correct if open in parent true if parent true treeview make all children true even if it false in database
                                .Union(ctx.Priv_Menus.Where(c => ParentMenuIds.Contains(c.Id) || c.Id == 1000)
                                 )
                                 .GroupBy(x => x.Id).Select(y => y.FirstOrDefault()).Where(List => List.ParentId == item.ParentId).ToList<Priv_Menus>();


                    List<Priv_Menus> lastparent = new List<Priv_Menus>();

                

                    lastparent = (from List in ctx.Priv_Menus.Where(List => List.Sold == true && List.OnlyForProgrammer != true).ToList()
                                  join
                                  DBase in ctx.Priv_Groups_Rights.Where(d => GroupIds.Contains(d.GroupId) && d.Open == true) on List.Id equals DBase.MenuId
                                  select List)
                                 //Add Parent like( Menue,System.General Ledger) because it not open in Priv_Groups_Rights we dont open parent because showing in treeview 
                                 //will not be correct if open in parent true if parent true treeview make all children true even if it false in database
                                 .Union(ctx.Priv_Menus.Where(c => ParentMenuIds.Contains(c.Id) || c.Id == 1000)
                                 )
                                 .GroupBy(x => x.Id).Select(y => y.FirstOrDefault()).Where(List => List.ParentId == null).ToList<Priv_Menus>();


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
        public string TreeviewProgrammers(int itemID, string mystr, int j, int flag)
        {


            List<Priv_Menus> querylist = new List<Priv_Menus>();
            var ctx = new MSDBContext();

            if (flag == 0)
            {

                querylist = (from List in StaticLists.AllMenueList select List).ToList().Where(List => List.ParentId == null).ToList<Priv_Menus>(); 
                           

                mainNode = querylist.Count;

                mystr += "[";
            }
            if (flag == 1)
            {

                querylist =  (from List in StaticLists.AllMenueList select List).ToList().Where(List => List.ParentId == itemID).ToList<Priv_Menus>();
                           

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
                mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + item.NameA + "\",NameE:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",checked:" + (item.HasChildren ? "false" : (item.Sold ? "true" : "false")) + ",expanded:" + (item.Expanded ? "true" : "false") + "" + ",HasChildren:" + (item.HasChildren ? "true" : "false") + ",text:\"" + (Request.Cookies["Language"].Value == Language.English? item.NameE: item.NameA) + "\",UrlLink:\"" + item.UrlLink + "\",ImageUrlLink:\"" + item.ImageUrlLink + "\",Target:\"" + item.Target + "\",HtmlAttributes:\"" + item.HtmlAttributes + "\",OnlyForProgrammer:" + (item.OnlyForProgrammer ? "true" : "false") + ",Notes:\"" + item.Notes + "\"";

                List<Priv_Menus> querylistParent = new List<Priv_Menus>();
                //Check this parent has child or not , if yes how many?

                querylistParent =(from List in StaticLists.AllMenueList select List).ToList().Where(List => List.ParentId == item.Id).ToList<Priv_Menus>();
                                

                childquantity = querylistParent.Count;
                //If Parent Has Child again call Treeview with new parameter
                if (childquantity > 0)
                {
                    mystr = TreeviewProgrammers(item.Id, mystr, i, 1);

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

                        parentquery = (from List in StaticLists.AllMenueList select List).ToList().Where(List => List.Id == currentparent).ToList<Priv_Menus>();
                                      


                        var rep2 = (from h in parentquery select new { h.ParentId }).First();

                        //put {[ up to end

                        //list of child
                        childlistquery = new List<Priv_Menus>();

                        childlistquery =(from List in StaticLists.AllMenueList select List).ToList().Where(List => List.ParentId == currentparent).ToList<Priv_Menus>();
                                       


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


                    lastchild =(from List in StaticLists.AllMenueList select List).ToList().Where(List => List.ParentId == item.ParentId).ToList<Priv_Menus>();
                                


                    List<Priv_Menus> lastparent = new List<Priv_Menus>();


                    lastparent =(from List in StaticLists.AllMenueList select List).ToList().Where(List => List.ParentId == null).ToList<Priv_Menus>();
                                


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
        public string TreeviewAdmin (int itemID, string mystr, int j, int flag)
        {

            List<Priv_Menus> querylist = new List<Priv_Menus>();
            var ctx = new MSDBContext();

            if (flag == 0)
            {

                querylist = ctx.Priv_Menus.Where(List => List.ParentId == null && List.Sold == true && List.OnlyForProgrammer != true).ToList(); 
                           

                mainNode = querylist.Count;

                mystr += "[";
            }
            if (flag == 1)
            {

                querylist = ctx.Priv_Menus.Where(List => List.ParentId == itemID && List.Sold == true && List.OnlyForProgrammer != true).ToList(); 
                           

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
                mystr += "{id:\"" + item.Id + "\",Id:\"" + item.Id + "\",NameA:\"" + item.NameA + "\",NameE:\"" + item.NameE + "\",ParentId:\"" + item.ParentId + "\",checked:" + (item.HasChildren ? "false" : (item.Sold ? "true" : "false")) + ",expanded:" + (item.Expanded ? "true" : "false") + "" + ",HasChildren:" + (item.HasChildren ? "true" : "false") + ",text:\"" + (Request.Cookies["Language"].Value == Language.English ? item.NameE : item.NameA) + "\",UrlLink:\"" + item.UrlLink + "\",ImageUrlLink:\"" + item.ImageUrlLink + "\",Target:\"" + item.Target + "\",HtmlAttributes:\"" + item.HtmlAttributes + "\",OnlyForProgrammer:" + (item.OnlyForProgrammer ? "true" : "false") + ",Notes:\"" + item.Notes + "\"";

                List<Priv_Menus> querylistParent = new List<Priv_Menus>();
                //Check this parent has child or not , if yes how many?

                querylistParent = ctx.Priv_Menus.Where(List => List.ParentId == item.Id && List.Sold == true && List.OnlyForProgrammer != true).ToList(); 
                                

                childquantity = querylistParent.Count;
                //If Parent Has Child again call Treeview with new parameter
                if (childquantity > 0)
                {
                    mystr = TreeviewAdmin(item.Id, mystr, i, 1);

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

                        parentquery = ctx.Priv_Menus.Where(List => List.Id == currentparent && List.Sold == true && List.OnlyForProgrammer!= true).ToList(); 
                                      


                        var rep2 = (from h in parentquery select new { h.ParentId }).First();

                        //put {[ up to end

                        //list of child
                        childlistquery = new List<Priv_Menus>();

                        childlistquery = ctx.Priv_Menus.Where(List => List.ParentId == currentparent && List.Sold == true && List.OnlyForProgrammer != true).ToList();
                                       


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


                    lastchild = ctx.Priv_Menus.Where(List => List.ParentId == item.ParentId && List.Sold == true && List.OnlyForProgrammer != true).ToList();
                                


                    List<Priv_Menus> lastparent = new List<Priv_Menus>();


                    lastparent = ctx.Priv_Menus.Where(List => List.ParentId == null && List.Sold == true && List.OnlyForProgrammer != true).ToList();
                                


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