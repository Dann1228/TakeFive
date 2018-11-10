using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TabkeFiveWebApplication.Models.Common
{
    public class ProductCategory
    {

        public static List<SelectListItem> GetClassLists()
        {

            List<SelectListItem> ClassLists = new List<SelectListItem>();

            ClassLists.Add(new SelectListItem()
            {                
                Text = "英文",
                Value = "1"
            });

            ClassLists.Add(new SelectListItem()
            {
                Text = "日文",
                Value = "2"
            });

            ClassLists.Add(new SelectListItem()
            {
                Text = "韓文",
                Value = "3",
            });

            return ClassLists;


        }



        public static List<SelectListItem> GetViedoLists()
        {

            List<SelectListItem> ClassLists = new List<SelectListItem>();

            ClassLists.Add(new SelectListItem()
            {
                Text = "英文影片",
                Value = "1"
            });

            ClassLists.Add(new SelectListItem()
            {
                Text = "日文影片",
                Value = "2"
            });

            ClassLists.Add(new SelectListItem()
            {
                Text = "韓文影片",
                Value = "3",
            });

            return ClassLists;


        }


    }
}