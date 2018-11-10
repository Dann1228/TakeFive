using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TabkeFiveWebApplication.Models.Common
{
    public class Programming
    {       
            public int selectedType { get; set; }
            public int selectedLanguange { get; set; }

            public System.Web.Mvc.SelectList types;
            public System.Web.Mvc.SelectList languanges;       
    }
}