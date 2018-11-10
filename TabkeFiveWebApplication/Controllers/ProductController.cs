using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TabkeFiveWebApplication.Models.Common;
using TabkeFiveWebApplication.Models.ViewModels;
using System.IO;
using TabkeFiveWebApplication.DAL;
using System.Net;
using Microsoft.AspNet.Identity;
using TabkeFiveWebApplication.Models.Cart;
using TFDBLibrary;

namespace TabkeFiveWebApplication.Controllers
{
    
    public class ProductController : Controller
    {
        public ActionResult UserProduct()
        {
            return View();
        }

        public string GetProductName(int pid)
        {
            TFDBLibrary.TakeFiveDBEntities db = new TakeFiveDBEntities();

            var query = from c in db.producttbl
                        where c.pid == pid
                        select new { c.name };
            return query.First().name;
        }
        //public string Time(DateTime time)
        //{

        //}
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetUserAllProduct()
        {
            TFDBLibrary.TakeFiveDBEntities db = new TakeFiveDBEntities();
            string userid = HttpContext.User.Identity.GetUserId();

            var query = from c in db.buyitemdetailtbl.AsEnumerable()
                        where c.mid == userid
                        select new { c.mid, productname = GetProductName(c.pid), c.state, c.pprice,time = Convert.ToString( c.btime) ,c.pid};

            return Json(new { data = query.ToList() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductGrade(int pid)
        {
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
            var query = from c in db.productgrade
                        where c.pid == pid
                        select c;
            int membernum = 0;
            int totalgrade = 0;
            foreach (var v in query)
            {
                membernum++;
                totalgrade += Convert.ToInt32(v.grade);
            }

            if (membernum == 0)
            {
                ViewBag.membernum = membernum;
                ViewBag.grade = 0;
            }
            else
            {
                ViewBag.membernum = membernum;
                ViewBag.grade = (float)(totalgrade / membernum);
            }

            return PartialView("_Grade");
        }

        public ActionResult SendProductGrade(int pid, int? grade)
        {
            string userid = HttpContext.User.Identity.GetUserId();
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
            productgrade product = new productgrade();
            product.pid = pid;
            product.grade = Convert.ToInt32(grade);
            product.mid = userid;

            db.productgrade.Add(product);
            db.SaveChanges();

            var query = from c in db.productgrade
                        where c.pid == pid
                        select c;

            int membernum = 0;
            int totalgrade = 0;
            foreach (var v in query)
            {
                membernum++;
                totalgrade += Convert.ToInt32(v.grade);
            }

            if (membernum == 0)
            {
                ViewBag.membernum = membernum;
                ViewBag.grade = 0;               
            }
            else
            {
                ViewBag.membernum = membernum;
                ViewBag.grade = totalgrade / membernum;
                db.producttbl.Find(pid).score = totalgrade / membernum;
                db.SaveChanges();
            }
            return PartialView("_Grade");
        }



        public ActionResult GetProductGrade(int pid)
        {
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
            var query = from c in db.producttbl
                        where c.pid == pid
                        select c;

            return PartialView("_ProductGrade", query);
        }

        public ActionResult SearchProduct()
        {
           
            //TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
            //var query = from c in db.producttbl
            //            where c.intro.Contains($"{keyword}") || c.name.Contains($"{keyword}")
            //            select c;
            ViewBag.keyword = Request.QueryString["keyword"];
            return View("Search");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetSearchProduct(string keyword)
        {


            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
            var query = from c in db.producttbl.AsEnumerable()
                        where c.intro.Contains($"{keyword}") || c.name.Contains($"{keyword}")
                        select new {
                            imgsrc = c.name,
                            c.pid,
                            c.name,
                         picture = Convert.ToBase64String(c.picture),
                           kind = c.kind==1?"課程":"影片",
                           category = c.category==1?"英文":c.category==2?"日文":"韓文",
                            c.intro,
                            c.price,
                            c.state,
                            c.score
                        };
     
            return Json(new { data = query.ToList() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductDetail(int? id)
        {
            IEnumerable<ProductViewModels> vModelList;
            ProductEntity pe = new ProductEntity();
            vModelList = pe.QueryByProductId(id.ToString());
            if (vModelList == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ProductDetail",vModelList);
        }
        //顯示照片，利用 Pid 到資料庫捉出圖檔
        public string ShowPhoto(int id)
        {
            ProductEntity pe = new ProductEntity();
            byte[] data = pe.GetPicture(id.ToString());

            string imgSrc = null;
            if (data != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(data, 0, data.Length);
                    string imgBase64 = Convert.ToBase64String(ms.ToArray());
                    imgSrc = string.Format("data:image/jpg;base64,{0}", imgBase64);
                }
            }
            return imgSrc;
           // return File(content, "image/jpg");
            
        }


        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IEnumerable<ProductViewModels> vModelList;
            ProductEntity pe = new ProductEntity();
            vModelList = pe.QueryByProductId(id.ToString());
            if (vModelList == null)
            {
                return HttpNotFound();
            }

            ProductViewModels vModel = vModelList.ToList()[0];
            //Kinds dropdownlist
            vModel.Kinds = ProductEnumLists.GetInitKinds();
            //Category dropdownlist
            vModel.Categories = ProductEnumLists.GetCategories(vModel.SelectedKinds.ToString());

            //取得目前登入使用者Id
            var UserId = HttpContext.User.Identity.GetUserId();
            vModel.MId = UserId;

            //這邊為了方便起見，先只回傳一筆，理論上應該是要直接回傳單一個 ViewModel
            return View(vModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModels vModel)
        {

            if (ModelState.IsValid)
            {
                
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

                
                ProductEntity pe = new ProductEntity();
                Boolean isSucced;

                //取得目前登入使用者Id
                var UserId = HttpContext.User.Identity.GetUserId();
                vModel.MId = UserId;

                isSucced = pe.Update(vModel);

                

                return RedirectToAction("Index");

                //return Content("Update state==>" + isSucced.ToString());
                //string total = ShowInfo(vModel);
                //return Content(total);


            }

            //return Content("<root>check failure</root>", "application/xml");

            return View(vModel);


        }


        // GET: Product
        public ActionResult Index()
        {

            //取得目前登入使用者Id
            var UserId = HttpContext.User.Identity.GetUserId();
                        

            //return Content("Login User ID==>" + UserId + "Login User Name==>" + UserName);

            ProductEntity pe = new ProductEntity();
            return View(pe.QueryByMemberId(UserId).ToList());

        }


        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            IEnumerable<ProductViewModels> vModelList;
            ProductEntity pe = new ProductEntity();
            vModelList=pe.QueryByProductId(id.ToString());
            if (vModelList == null)
            {
                return HttpNotFound();
            }

            //這邊為了方便起見，先只回傳一筆，理論上應該是要直接回傳單一個 ViewModel
            ProductViewModels vModel = vModelList.ToList()[0];
            return View(vModel);

        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            ProductEntity pe = new ProductEntity();
            Boolean isSucced;
            isSucced = pe.Delete(id);

            //return Content("Insert state==>" + isSucced.ToString());

            return RedirectToAction("Index");
        }



        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IEnumerable<ProductViewModels> vModelList;
            ProductEntity pe = new ProductEntity();
            vModelList = pe.QueryByProductId(id.ToString());
            if (vModelList == null)
            {
                return HttpNotFound();
            }

            ProductViewModels vModel = vModelList.ToList()[0];
            //Kinds dropdownlist
            vModel.Kinds = ProductEnumLists.GetInitKinds();
            //Category dropdownlist
            vModel.Categories = ProductEnumLists.GetCategories(vModel.SelectedKinds.ToString());


            //這邊為了方便起見，先只回傳一筆，理論上應該是要直接回傳單一個 ViewModel
            return View(vModel);

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public Action UploadVideo(IEnumerable<HttpPostedFileBase>files)
        //{
        //    if (files.First()!=null)
        //    {
        //        foreach (var file in files)
        //        {
        //            string SourceFileName = Path.GetFileName(file.FileName);

        //        }
        //    }
        //    return View();
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
        public ActionResult Create(HttpPostedFileBase video,ProductViewModels vModel)
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
                    vModel.VidioUrl =VideoUrl;
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
                "pid-->" + pid + ",mid-->" + mid + ",selectKind-->" +  selectkind +
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


}
