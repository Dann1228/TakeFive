using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TabkeFiveWebApplication.Models.Cart;
using TabkeFiveWebApplication.DAL;
using Microsoft.AspNet.Identity;
using TabkeFiveWebApplication.Models.ViewModels;
using TFDBLibrary;

namespace TabkeFiveWebApplication.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {

        public ActionResult FinishCancelOrder()
        {
            return View();
        }

        public ActionResult CancelOrderFailure()
        {
            return View();
        }



        [HttpPost]
        public ActionResult CanelOrder(string TextBox_CancelReason,string HiddenInput_PID)
        {

            PaymentEntity pe = new PaymentEntity();
            Boolean isUpdated;
            isUpdated=pe.CancelOrder( HiddenInput_PID, TextBox_CancelReason);

            if (isUpdated == true)
            {

                return RedirectToAction("FinishCancelOrder");

            }
            else
            {

                return RedirectToAction("CancelOrderFailure");

            }
            
            //return Content("Cancel Reason is===>" + TextBox_CancelReason + ",Pid===>" + HiddenInput_PID);


        }

        public ActionResult CanelOrder(int Id)
        {

            BuyItemDetailEntify be = new BuyItemDetailEntify();
            ViewBag.Pid = Id;
            return View(be.Query(Id).ToList());
            
        }


        // GET: Order        
        public ActionResult Index()
        {

            return View();
          
        }

        public ActionResult MyOrderDetail(int Id)
        {

            BuyItemDetailEntify be = new BuyItemDetailEntify();
            return View(be.Query(Id).ToList());
            
        }

        public ActionResult MyOrder()
        {

            //取得目前登入使用者Id
            var UserId = HttpContext.User.Identity.GetUserId();


            PaymentEntity pe = new PaymentEntity();
            return View(pe.Query(UserId).ToList());

        }

            public ActionResult FinishOrdered()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(OrderViewModel orderView)
        {
            if (ModelState.IsValid)
            {

                var userId = HttpContext.User.Identity.GetUserId();

                var currentCart = Models.Cart.Operation.GetCurrentCart();
                List<CartItem> items = currentCart.GetCartItems();

                Decimal totalPrice;
                totalPrice = currentCart.TotalAmount;

                //0 現金
                //1 信用卡
                PaymentEntity pe = new PaymentEntity();
                int payId;
                payId= pe.Insert(orderView,userId , totalPrice, 1);

                BuyItemDetailEntify be = new BuyItemDetailEntify();
                Boolean isInserted;
                isInserted=be.Insert(items, userId, payId);

                TFDBLibrary.TakeFiveDBEntities db = new TFDBLibrary.TakeFiveDBEntities();
           
                foreach (var item in items)
                {
                    db.producttbl.Find(item.Id).state += 1;
                }
                db.SaveChanges();

                //移除購物車Session資料
                HttpContext.Session.Remove("Cart");

                //轉至訂單完成頁面
                return RedirectToAction("FinishOrdered");

                //string msg;
                //msg=PrintOrderViewContent(orderView);
                //return Content(msg);


            }
            // todo error page
            return Content("Error!");
           //return View(); 
        }


        private string PrintOrderViewContent(OrderViewModel orderView)
        {

            StringBuilder msg = new StringBuilder();
            msg.Append("Name==>" + orderView.MName + "<br>");
            msg.Append("Phone==>" + orderView.MPhone + "<br>");
            msg.Append("CreditNo1==>" + orderView.CreditNumber1 + "<br>");
            msg.Append("CreditNo2==>" + orderView.CreditNumber2 + "<br>");
            msg.Append("CreditNo3==>" + orderView.CreditNumber3 + "<br>");
            msg.Append("CreditNo4==>" + orderView.CreditNumber4 + "<br>");
            msg.Append("VaildMonth==>" + orderView.VaildMonth + "<br>");
            msg.Append("VaildYear==>" + orderView.VaildYear + "<br>");
            msg.Append("VaildMemo==>" + orderView.Memo + "<br>");
            
            return msg.ToString();

        }


        private string PrintContent()
        {

            //取得目前購物車資料
            var currentCart = Models.Cart.Operation.GetCurrentCart();

            IEnumerator enumerator = currentCart.GetEnumerator();


            StringBuilder msg = new StringBuilder();



            while (enumerator.MoveNext())
            {
                CartItem item = (CartItem)enumerator.Current;
                msg.Append("item id:" + item.Id + "----" + "item Name:" + item.Name + "----" + "item Price:" + item.Price + "----" + "item Price:" + item.Quantity + "<br>");
            }

            return msg.ToString();


        }


    }
}