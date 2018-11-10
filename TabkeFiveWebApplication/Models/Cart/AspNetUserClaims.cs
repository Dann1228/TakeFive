using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TabkeFiveWebApplication.Models.Cart
{
    public class AspNetUserClaims
    {

        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }


    }
}