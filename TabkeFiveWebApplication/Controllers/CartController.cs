using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TabkeFiveWebApplication.Models.Cart;
using TabkeFiveWebApplication.Models.ViewModels;

namespace TabkeFiveWebApplication.Controllers
{
    public class CartController : Controller
    {

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        //取得目前購物車頁面
        public ActionResult GetCart()
        {
            return PartialView("_CartPartial");
        }

        /// <summary>
        /// 將商品加入購物車
        /// </summary>
        /// <param name="Id">商品編號(Product Id)</param>
        /// <returns></returns>
        public ActionResult AddToCart(int? Id)
        {
            var currentCart = Operation.GetCurrentCart();
            currentCart.AddProduct(Id);
            return PartialView("_CartPartial");
        }

        /// <summary>
        /// 將商品移除購物車
        /// </summary>
        /// <param name="Id">商品編號(Product Id)</param>
        /// <returns></returns>
        public ActionResult RemoveFromCart(int? Id)
        {
            var currentCart = Operation.GetCurrentCart();
            currentCart.RemoveProduct(Id);
            return PartialView("_CartPartial");
        }

        /// <summary>
        /// 清空購物車
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearCart()
        {
            var currentCart = Operation.GetCurrentCart();
            currentCart.ClearCart();
            return PartialView("_CartPartial");
        }
        [HttpGet]
        public string ClearCartID()
        {
            string ids="";
            var currentCart = Operation.GetCurrentCart();
            foreach (var cart in currentCart)
            {
                ids = ids + cart.Id+'/';
            }
            return ids;
        }
        public ActionResult CurrentCart(int? id)
        {
            var currentCart = Operation.GetCurrentCart();
            var userid = HttpContext.User.Identity.GetUserId();
            ProductViewModels product = new ProductViewModels();
            product.PId =Convert.ToInt32(id);
            TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
            var query = from c in db.buyitemdetailtbl
                        where c.mid == userid && c.pid == id
                        select c;
            if (query.Count()>0)
            {
                return PartialView("_btnBuy");
            }
            foreach (var cartitem in currentCart)
            {
                if (cartitem.Id == id)
                {
                    return PartialView("_btnAdded",product);
                }
            }
            return PartialView("_btnAddCart",product);
        }
    }
}
