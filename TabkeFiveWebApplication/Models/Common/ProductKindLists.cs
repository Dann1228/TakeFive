using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TabkeFiveWebApplication.Models.Common
{
    public class ProductKindLists
    {

                 
        public static List<SelectListItem>  GetProductKindLists()
        {
            
            List<SelectListItem>  SelectItemList = new List<SelectListItem>();

            SelectItemList.Add(new SelectListItem() {
                Text = "課程",
                Value = "1"                
            });

            SelectItemList.Add(new SelectListItem() {
                Text = "影片",
                Value = "2"                
            });

            SelectItemList.Add(new SelectListItem() {
                Text = "二手書",
                Value = "3",                
            });

            return SelectItemList;


          }


    }
}