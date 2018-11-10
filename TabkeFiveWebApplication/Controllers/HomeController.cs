using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TFDBLibrary;

namespace TabkeFiveWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TakeFiveDBEntities db = new TakeFiveDBEntities();
            var aquery = (from x in db.producttbl.AsEnumerable()
                         where x.kind == 2
                         select x).Take(6);
            ViewBag.kindt = aquery;
            var query = (from x in db.producttbl.AsEnumerable()
                        where x.category == 1
                        select x).Take(6);
            ViewBag.kind = query;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult FindEng()
        {

            TakeFiveDBEntities db = new TakeFiveDBEntities();


            var aquery = from x in db.producttbl.AsEnumerable()
                         where x.category == 1
                         select x;
            ViewBag.kindt = aquery;

            return PartialView("_FindPartial", aquery);



        }
        public ActionResult FindJap()
        {

            TakeFiveDBEntities db = new TakeFiveDBEntities();

            var aquery = from x in db.producttbl.AsEnumerable()
                         where x.category == 2
                         select x;
            ViewBag.kindt = aquery;

            return PartialView("_FindjapPartial", aquery);



        }
        public ActionResult FindKor()
        {

            TakeFiveDBEntities db = new TakeFiveDBEntities();
            var query = from x in db.producttbl.AsEnumerable()
                        where x.category == 3
                        select x;
            ViewBag.kindt = query;


            return PartialView("_FindkorPartial", query);



        }
        public FileResult photo(int id)
        {
            TakeFiveDBEntities db = new TakeFiveDBEntities();
            var x = db.producttbl.Find(id).picture;
            return File(x, "image/jpeg");
        }
    }
}