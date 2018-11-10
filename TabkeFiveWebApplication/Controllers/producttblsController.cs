using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TabkeFiveWebApplication.DAL;
using TabkeFiveWebApplication.Models;
using TabkeFiveWebApplication.Models.Common;
using TabkeFiveWebApplication.Models.ViewModels;
using TFDBLibrary;

namespace TabkeFiveWebApplication.Controllers
{
    public class producttblsController : Controller
    {
        private TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();

        // GET: producttbls
        public ActionResult Index()
        {
            return View(db.producttbl.ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetVideo()
        {
            var list2 = db.producttbl.Select(x => x).ToList();
            var list1 = list2.Select(x => new producttbl2
            {
                kind = x.kind,
                category = x.category,
                mid = x.mid,
                name = x.name,
                intro = x.intro,
                price = x.price,
                state = x.state,
                score = x.score,
                picture = x.pid,
                vidiourl = x.vidiourl
            }).ToList();
            return Json(new { data = list1 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Itemphoto(int id)
        {
            var x = db.producttbl.Find(id).picture;
            return File(x, "image/jpeg");
        }
        // GET: producttbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TFDBLibrary.producttbl producttbl = db.producttbl.Find(id);
            if (producttbl == null)
            {
                return HttpNotFound();
            }
            return View(producttbl);
        }

        //// GET: producttbls/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: producttbls/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "pid,kind,category,mid,name,intro,price,state,score,vidiourl")] Models.Partials.producttbl producttbl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Request.Files["f_image"] != null)
        //        {
        //            byte[] ImageContent = null;
        //            using (BinaryReader br = new BinaryReader(Request.Files["f_image"].InputStream))
        //            {
        //                ImageContent = br.ReadBytes(
        //                Request.Files["f_image"].ContentLength);
        //            }
        //            producttbl.picture = ImageContent;
        //        }
        //        db.producttbl.Add(producttbl);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(producttbl);
        //}

        // GET: producttbls/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Models.Partials.producttbl producttbl = db.producttbl.Find(id);
        //    if (producttbl == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(producttbl);
        //}

        // POST: producttbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "pid,kind,category,mid,name,intro,price,state,score,picture,vidiourl")] producttbl producttbl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(producttbl).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(producttbl);
        //}

        // GET: producttbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TFDBLibrary.producttbl producttbl = db.producttbl.Find(id);
            if (producttbl == null)
            {
                return HttpNotFound();
            }
            return View(producttbl);
        }

        // POST: producttbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TFDBLibrary.producttbl producttbl = db.producttbl.Find(id);
            db.producttbl.Remove(producttbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult VideoEdit(int id)
        {
            var model = db.producttbl.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult VideoEdit(producttbl model)
        {
            ModelState.AddModelError("", "儲存成功。");
            if (Request.Files[0] != null)
            {
                byte[] ImageContent = null;
                using (BinaryReader br = new BinaryReader(Request.Files[0].InputStream))
                {

                    ImageContent = br.ReadBytes(
                        Request.Files[0].ContentLength);
                }

                model.picture = ImageContent;

            }

            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();

            return View(model);
        }

        //[HttpPost]
        //public ActionResult VideoCreate(producttbl model)
        //{
        //    ModelState.AddModelError("", "儲存成功。");
        //    if (Request.Files[0] != null)
        //    {
        //        byte[] ImageContent = null;
        //        using (BinaryReader br = new BinaryReader(Request.Files[0].InputStream))
        //        {
        //            ImageContent = br.ReadBytes(
        //            Request.Files[0].ContentLength);
        //        }
        //        model.picture = ImageContent;
        //    }
        //    db.Entry(model).State = EntityState.Detached;
        //    db.SaveChanges();
        //    return View(model);
        //}


        // GET: Product/Create
        public ActionResult Create()
        {

            ProductViewModels vModel = new ProductViewModels();
            //Kinds dropdownlist
            vModel.Kinds = ProductEnumLists.GetInitKinds();
            //Category dropdownlist
            vModel.Categories = ProductEnumLists.GetInitCategories();

            //取得目前登入使用者Id
            var UserId = HttpContext.User.Identity.GetUserId();
            vModel.MId = UserId;

            return View(vModel);

        }


        //public string SaveFiles(string targetFileName , HttpPostedFileBase file)
        //{
        //    string SourceFileName = Path.GetFileName(file.FileName);
        //    string Url = Path.Combine($"~/{targetFileName}", SourceFileName);
        //    string TargetFileName = Path.Combine(Server.MapPath($"~/{targetFileName}"), SourceFileName);
        //    file.SaveAs(TargetFileName);
        //    return Url;
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase video, ProductViewModels vModel)
        {

            if (ModelState.IsValid)
            {
                //ToDo  upload video 
                if (video != null)
                {

                    string SourceFileName = Path.GetFileName(video.FileName);
                    string VideoUrl = Path.Combine("~/Videos", SourceFileName);
                    string TargetFileName = Path.Combine(Server.MapPath("~/Videos"), SourceFileName);
                    video.SaveAs(TargetFileName);

                    //SourceFileName = Path.GetFileName(File1.FileName);
                    //string PicUrl = Path.Combine("~/Pic", SourceFileName);
                    //TargetFileName = Path.Combine(Server.MapPath("~/Pic"), SourceFileName);
                    //file.SaveAs(TargetFileName);

                    if (Request.Files["File1"] != null)
                    {

                        byte[] ImageContent = null;
                        using (BinaryReader br = new BinaryReader(Request.Files["File1"].InputStream))
                        {

                            ImageContent = br.ReadBytes(
                                Request.Files["File1"].ContentLength);

                        }

                        vModel.Picture = ImageContent;

                    }

                    //return RedirectToAction("Create");
                    ProductEntity pe = new ProductEntity();
                    Boolean isSucced;

                    //取得目前登入使用者Id
                    var UserId = HttpContext.User.Identity.GetUserId();
                    vModel.MId = UserId;
                    vModel.VidioUrl = VideoUrl;
                    isSucced = pe.Insert(vModel);
                    return RedirectToAction("Index");
                    //return Content("Insert state==>" + isSucced.ToString());
                    //string total = ShowInfo(vModel);
                    //return Content(total);
                }

            }

            //return Content("<root>check failure</root>", "application/xml");
            return View(vModel);
        }


        //顯示 ProductViewModels 內容
        private string ShowInfo(ProductViewModels vModel)
        {
            String pid = vModel.PId.ToString();
            string mid = vModel.MId.ToString();
            string selectkind = vModel.SelectedKinds.ToString();
            string selectCategories = vModel.SelectedCategories.ToString();
            string intro = vModel.Intro;
            string price = vModel.Price.ToString();
            string state = vModel.State.ToString();
            string score = vModel.Score.ToString();
            string videoUrl = vModel.VidioUrl;

            string total =
                "pid-->" + pid + ",mid-->" + mid + ",selectKind-->" + selectkind +
                ",selectCategories-->" + selectCategories + ",intro-->" + intro +
                ",price-->" + price + ",state-->" + state +
                ",score-->" + score + ",videoUrl" + videoUrl;

            return total;


        }

        //在 Create Page ，dropdownlist 使用
        //It is necessary to set string type in parameter or else you will get Internal Server error 500.
        public JsonResult GetProductCategories(string typeSelected)
        {

            ProductViewModels mView = new ProductViewModels();
            mView.Categories = ProductEnumLists.GetCategories(typeSelected);
            return Json(new { mView.Categories });

        }
    }




    internal class producttbl2
    {
        public int? kind { get; set; }
        public int? category { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public string intro { get; set; }
        public int? price { get; set; }
        public int? state { get; set; }
        public int? score { get; set; }
        public int picture { get; set; }
        public string vidiourl { get; set; }
    }
}