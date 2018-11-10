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
    public class DiscussController : Controller
    {
        // GET: Discuss
        [HttpGet]
        public ActionResult Index(int id)
        {
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
            var query = from c in db.discuss
                        where c.p_id == id
                        orderby c.d_id descending
                        select new DiscussViewModel {DId=c.d_id,PId=c.p_id,MId=c.Id,
                        DContent=c.d_content,DCrateDate=c.d_crateDate,
                            DGreat =c.d_great,DModifyDate=c.d_modifyDate
                        };
            if (query.Count() > 0 )
            {
                return PartialView("_Discuss", query);
            }
            else
            { 
                DiscussViewModel non = new DiscussViewModel();
                non.PId = id;
                return PartialView("_NonDiscuss",non);
            }
          
        }

        public IEnumerable<DiscussViewModel> Msg(int id)
        {
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
            var query = from c in db.discuss
                        where c.p_id == id
                        orderby c.d_crateDate descending
                        select new DiscussViewModel
                        {
                            DId = c.d_id,
                            PId = c.p_id,
                            MId = c.Id,
                            DContent = c.d_content,
                            DCrateDate = c.d_crateDate,
                            DGreat = c.d_great,
                            DModifyDate = c.d_modifyDate
                        };          
            return query;
        }

        [HttpGet]
        public ActionResult SendMsg(int PId)
        {
            discuss ok = new discuss();
            TFDBLibrary.discuss discuss = new discuss();
            string userId = HttpContext.User.Identity.GetUserId();
            if (userId != null)
            {
                ViewBag.user = true;
            }
            else
            {
                ViewBag.user = false;
            }
            discuss.p_id = PId;
            return PartialView("_SendMsg",discuss);
        }
        public string GetUserName(string id )
        {
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
            var query = from c in db.AspNetUsers
                        where c.Id == id
                        select new { c.UserName };
            return query.FirstOrDefault().UserName;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SendMsg(string id)
        {
            string[] msg = id.Split('/');
            discuss model = new discuss();
            model.p_id = Convert.ToInt32(msg[0]);
            model.d_content = msg[1];
            var ID = HttpContext.User.Identity.GetUserId();
            
            if (ID==null)
            {
                return RedirectToAction("Login","Account");
            }
            int pid =Convert.ToInt32( model.p_id);
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
            model.Id = ID;
          //  model.d_crateDate =Convert.ToString( DateTime.Now.ToLocalTime());
            model.d_crateDate =DateTime.Now.ToShortDateString();

            db.discuss.Add(model);
            db.SaveChanges();

            //var query = from c in db.producttbl.AsEnumerable()
            //            where c.pid ==pid 
            //            select new TabkeFiveWebApplication.Models.ViewModels.ProductViewModels
            //            {
            //                PId = c.pid,
            //                SelectedKinds =Convert.ToInt32( c.kind),
            //                SelectedCategories = Convert.ToInt32(c.category),
            //                MId = c.mid,
            //                Intro = c.intro,
            //                Price = Convert.ToInt32 (c.price),
            //                State = Convert.ToInt32(c.state),
            //                Score = Convert.ToInt32(c.score)
            //            }; 

            //return RedirectToAction("View", "ShoppingCart", new { id = pid });
            //var query = from c in db.discuss
            //            where c.p_id == model.p_id
            //            select c;
            //return PartialView("_Discuss",query);
             return Content(msg[0]);
        }
    }
}