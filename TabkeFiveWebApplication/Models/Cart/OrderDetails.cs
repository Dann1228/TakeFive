using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TabkeFiveWebApplication.Models.Cart
{
    public class OrderDetails
    {

        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Order_Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity { get; set; }

        public virtual Orders Orders { get; set; }
        public virtual Products Products { get; set; }

    }
}