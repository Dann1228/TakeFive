using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TabkeFiveWebApplication.Models.Cart
{
    public class Orders
    {

        public Orders()
        {
            this.OrderDetails = new HashSet<OrderDetails>();
        }

        public int Id { get; set; }
        public string AspNetUser_Id { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public System.DateTime BuyDT { get; set; }
        public string Memo { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }

    }
}