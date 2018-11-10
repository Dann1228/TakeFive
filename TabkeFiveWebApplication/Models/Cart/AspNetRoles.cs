using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TabkeFiveWebApplication.Models.Cart
{
    public class AspNetRoles
    {
        public AspNetRoles()
        {
            this.AspNetUsers = new HashSet<AspNetUsers>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }

    }
}