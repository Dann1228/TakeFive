using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TabkeFiveWebApplication.Models.ViewModels;
using TFDBLibrary;

namespace TabkeFiveWebApplication.Controllers
{
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetUserNote()
        {
            string id = HttpContext.User.Identity.GetUserId();
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();

            var query = from c in db.note.AsEnumerable()
                        where c.mid == id
                        select new {c.n_id, c.p_name,c.p_id,c.create_time,c.n_title,c.n_grade,c.mid,videotime= ChangeTime(Convert.ToInt32( c.videotime)),c.n_content};
            return Json(new { data = query.ToList() },JsonRequestBehavior.AllowGet);
        }

        public string ChangeTime(int time)
        {
            string min = Convert.ToString(time / 60);
            string sec = Convert.ToString(time % 60);

            return min + ":" + sec;
        }
        [HttpPost]
        [ValidateInput(false)]
        public int ChangeUserNote(int nid,string title, string content)
        {
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
            var note=db.note.Find(nid);
            note.n_title = title;
            note.n_content = content;
            db.SaveChanges();

            var query = from c in db.note
                        where c.n_id == nid
                        select new { c.p_id};
            int pid = Convert.ToInt32(query.First().p_id);
            return pid;
        }

        [HttpPost]
        [ValidateInput(false)]
        public int SendtoNote(string title, double? videotime, string content, int pid)
        {
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
            var query = from c in db.producttbl
                        where c.pid == pid
                        select new { c.name };
            string productname =query.Count()>0 ? query.First().name:string.Empty;

            note model = new note();
            model.n_title = title;
            model.n_content =Convert.ToString( content);
            model.p_id = pid;
            model.videotime = videotime;
            model.create_time = DateTime.Now.ToShortDateString();
            model.mid = HttpContext.User.Identity.GetUserId();
            model.p_name = productname;
            db.note.Add(model);
            db.SaveChanges();

            return pid;

        }



        [HttpGet]
        public ActionResult GetAllNoteBook(int pid,string starttime,string endtime,int? skip)
        {
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();

            var total = from c in db.note.AsEnumerable()
                        where c.p_id == pid && c.videotime < float.Parse(endtime) &&
                        c.videotime >= float.Parse(starttime)
                        select c;

            var query = from c in db.note.AsEnumerable()
                        where c.p_id==pid && c.videotime < float.Parse(endtime) &&
                        c.videotime >= float.Parse(starttime)
                        select c;
            if (query.Count() <= 0) {
                return PartialView("_ZeroNoteBook");
            }


            if ((skip + 1) * 9 >= total.Count() && skip == 0)
            {
                ViewBag.right = false;
                ViewBag.left = false;
            }
            else if ((skip+1)*9 >= total.Count())
            {
                ViewBag.right = false;
                ViewBag.left = true;
            }
            else if(skip == 0)
            {
                ViewBag.right = true;
                ViewBag.left = false;
            }
            else
            {
                ViewBag.right = true;
                ViewBag.left = true;
            }          
            return PartialView("_NoteTag",query.Skip(Convert.ToInt32(skip) * 9).Take(9));
        }


        [HttpGet]
        public ActionResult GetNoteByID(int nid)
        {
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();

            var query = from c in db.note
                        where c.n_id == nid
                        select c;
            string userid = HttpContext.User.Identity.GetUserId();

            if (query.Count()>0)
            {
                ViewBag.edit = query.First().mid==userid ? true:false;
                return PartialView("_NoteBook",query);
            }
            else
            {
               return HttpNotFound();
            }
        }


        //[HttpGet]
        //public ActionResult GetNoteByTime(int nid)
        //{
        //    TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
        //    var query = from c in db.note
        //                where c.n_id == nid
        //                select c;
        //    var product = from c in db.producttbl
        //                  where c.pid == query.FirstOrDefault().p_id
        //                  select new ProductViewModels { VidioUrl = c.vidiourl, PId = c.pid,
        //                      SelectedCategories = Convert.ToInt32(c.category),
        //                      SelectedKinds = Convert.ToInt32(c.kind),
        //                      MId = c.mid,
        //                      Picture = c.picture ,
        //                       Intro = c.intro,
        //                        Name = c.name,
        //                         Price = Convert.ToInt32(c.price),
        //                          Score = Convert.ToInt32(c.score),
        //                          State = Convert.ToInt32(c.state)
        //                  };
        //    ViewBag.Time = query.FirstOrDefault().videotime;
        //    ViewBag.Buy = true;
        //    return View("View",product);
        //}

    }
}