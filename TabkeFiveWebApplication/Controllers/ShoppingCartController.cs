using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TabkeFiveWebApplication.Models.ViewModels;
using TabkeFiveWebApplication.DAL;
using PagedList;
using Microsoft.AspNet.Identity;

namespace TabkeFiveWebApplication.Controllers
{
    // GET: 網路下單
    public class ShoppingCartController : Controller
    {
        int pageSize = 9;
        public ActionResult View(int? id)
        {
            if (id != null)
            {
                ProductEntity pe = new ProductEntity();
                var query = pe.QueryByProductId(id.ToString());
                ViewBag.keyword = query.FirstOrDefault().Categories;

                TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
                string userid = HttpContext.User.Identity.GetUserId();
                if( userid != null )
                {
                    var buyquery = from c in db.buyitemdetailtbl
                            where c.mid == userid && c.pid == id
                            select c;

                    if (buyquery.Count()>0)
                    {
                        ViewBag.Buy = true;
                    }
                    else
                    {
                        ViewBag.Buy =false;
                    }
                }
                else
                {
                    ViewBag.Buy = false;
                }
           
                return View(query);
            }
             
            return View();
        }

        public ActionResult GetLikeProduct(int category)
        {
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();

            var userid = HttpContext.User.Identity.GetUserId();
            if (userid != null)
            {
                var buyquery = from c in db.buyitemdetailtbl
                               where c.mid == userid 
                               select c.pid;
                List<string> buiedlist = new List<string>();
                foreach (var item in buyquery)
                {
                    buiedlist.Add(Convert.ToString(item));
                }

                var query = (from c in db.producttbl
                            where c.category==category 
                            orderby c.score descending
                            select c).ToList().SkipWhile(x=> buiedlist.Contains(Convert.ToString(x.pid)));

                return PartialView("_Shopping", query.Take(3));
            }
            else
            {
                var query = from c in db.producttbl
                where c.category == category
                          orderby c.score descending
                          select c;

                return PartialView("_Shopping",query.Take(3));
            }     
     
        }

        //顯示照片，利用 Pid 到資料庫捉出圖檔
        public FileResult ShowPhoto(int id)
        {

            ProductEntity pe = new ProductEntity();
            byte[] content = pe.GetPicture(id.ToString());
            return File(content, "image/jepg");

        }


        // GET: ShoppingCart        (int? kindId,int? lang,int page)
        public ActionResult Index(int? kindId, int? lang, int page=1)
        {
            int currentPage = page < 1 ? 1 : page;
            
             ProductEntity pe = new ProductEntity();
             IEnumerable<ProductViewModels> vModelList=null;
            //vModelList = pe.QueryByAllKind();

            if (kindId == null)
            {
                vModelList = pe.QueryByAllKind();

            }
            else
            {

                switch (kindId)
                {
                    case 0:
                        //全部
                        vModelList = pe.QueryByAllKind();
                        break;

                    case 1:
                        //課程
                        if (lang == 0)
                        {
                            vModelList = pe.QueryByKindId(kindId, lang);
                        }
                        else
                        {
                            vModelList = pe.QueryByKindId(kindId, lang);
                        }

                        break;

                    case 2:
                        //影片

                        vModelList = pe.QueryByKindId(kindId);
                        break;

                }

                //return Content("the kind id==>" + kindId + "category id " + lang);

            }


            var result= vModelList.ToPagedList(currentPage,pageSize);

            return View(result);
        }


        public ActionResult SelectIndex(int? kindId, int? lang, int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;

            ProductEntity pe = new ProductEntity();
            IEnumerable<ProductViewModels> vModelList = null;
            //vModelList = pe.QueryByAllKind();

            if (kindId == null)
            {
                vModelList = pe.QueryByAllKind();

            }
            else
            {

                switch (kindId)
                {
                    case 0:
                        //全部
                        if (lang == null)
                        {
                            vModelList = pe.QueryByAllKind();
                        }
                        else
                        {
                            vModelList = pe.QueryByCatgoryId(lang);
                        }
                        
                        break;

                    case 1:
                        //課程
                        if (lang ==null)
                        {
                            vModelList = pe.QueryByKindId(kindId);
                        }
                        else
                        {
                            vModelList = pe.QueryByKindId(kindId, lang);
                        }

                        break;

                    case 2:
                        //影片
                        if (lang == null)
                        {
                            vModelList = pe.QueryByKindId(kindId);
                        }
                        else
                        {
                            vModelList = pe.QueryByKindId(kindId, lang);
                        }
                     
                        break;

                }

                //return Content("the kind id==>" + kindId + "category id " + lang);

            }


            var result = vModelList.ToPagedList(currentPage, pageSize);
            return PartialView("Index", result);
        }
        //public ActionResult Index1(int page)
        //{
        //    int currentPage = page < 1 ? 1 : page;            
        //    ProductEntity pe = new ProductEntity();

        //    IEnumerable<ProductViewModels> vModelList = null;
        //    vModelList = pe.QueryByAllKind();
        //    var result = vModelList.ToPagedList(currentPage, pageSize);
        //    return View(result);
        //}

    }
}
