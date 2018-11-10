using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TabkeFiveWebApplication.DAL;

namespace TabkeFiveWebApplication.Models.Cart
{

    public class Cart : IEnumerable<CartItem>
    {

        //商品個別項目 List
        private List<CartItem> cartItems;
        public List<CartItem>  GetCartItems()
        {
            return cartItems;
        }
                          

        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }

        //購物車內商品總數量
        public int Count
        {
            get
            {
                return this.cartItems.Count();
            }
        }

        //購物車內商品總價
        public decimal TotalAmount
        {
            get
            {
                decimal totalAmount = 0.0m;
                foreach (var cartItem in this.cartItems)
                {
                    totalAmount = totalAmount + cartItem.Amount;
                }
                return totalAmount;
            }
        }


        /// <summary>
        /// 新增商品至購物車：
        /// 先檢查此商品是否已存在購物車，
        /// 若是則數量加1，
        /// 若否則新增product至購物車內。
        /// </summary>
        /// <param name="Id">商品編號(Product Id)</param>
        public void AddProduct(int? Id)
        {
            var tmpItem = this.cartItems
                            .Where(items => items.Id == Id)
                            .Select(items => items)
                            .FirstOrDefault();

            if (tmpItem == default(CartItem))
            {


                ProductEntity pe = new ProductEntity();
                CartItem dbitem = pe.QueryByProductid(Id);

                if (dbitem != default(CartItem))
                {
                        this.AddCarItem(dbitem);
                }

                    ////若在購物車，沒有同樣的商品
                    //using (WelcomeBooksEntities db = new WelcomeBooksEntities())
                    //{
                    //    var product = (from prod in db.Products
                    //                   where prod.Id == Id
                    //                   select prod).FirstOrDefault();


                    //    if (product != default(Products))
                    //    {
                    //        this.AddProduct(product);
                    //    }

                    //}


            }
            else
            {
                //若在購物車，已有同樣的商品，則數量加1 
                tmpItem.Quantity += 1;
            }

        }

        private void AddCarItem(CartItem item)
        {
            
            //加入購物車內
            this.cartItems.Add(item);
        }

        private void AddProduct(Products product)
        {
            var cartItem = new CartItem()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = 1,
                PhotoUrl = product.PhotoUrl
            };

            //加入購物車內
            this.cartItems.Add(cartItem);
        }

        /// <summary>
        /// 從購物車移除商品：
        /// 判斷此商品是否有在購物車內，若有則移除。
        /// </summary>
        /// <param name="Id">商品編號(Product Id)</param>
        public void RemoveProduct(int? Id)
        {
            var tmpItem = this.cartItems
                            .Where(items => items.Id == Id)
                            .Select(items => items)
                            .FirstOrDefault();

            //存在購物車內，刪除此Id商品
            if (tmpItem != default(CartItem))
            {
                this.cartItems.Remove(tmpItem);
            }
        }

        //清空購物車
        public void ClearCart()
        {
            this.cartItems.Clear();
        }

        public List<OrderDetails> ToOrderDetailList(int orderId)
        {
            var result = new List<OrderDetails>();
            foreach (var cartItem in this.cartItems)
            {
                result.Add(new OrderDetails()
                {
                    Order_Id = orderId,
                    Price = cartItem.Price,
                    Product_Id = cartItem.Id,
                    Quantity = cartItem.Quantity
                });
            }
            return result;
        }

        #region IEnumerator
        public IEnumerator<CartItem> GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }
        #endregion

    }


}