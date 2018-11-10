using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TabkeFiveWebApplication.Models.Cart
{
    public class Products
    {

        public Products()
        {
            this.OrderDetails = new HashSet<OrderDetails>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Category_Id { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public System.DateTime PublishDT { get; set; }
        public bool IsPublic { get; set; }

        public virtual Categories Categories { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

    }
}